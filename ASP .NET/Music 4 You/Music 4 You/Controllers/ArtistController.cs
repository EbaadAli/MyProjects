using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class ArtistsController : Controller
    {
        // Reference to the data manager
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int id)
        {
            return View(m.ArtistGetById(id));
        }

        // GET: Artists/Create
        // Allow a Manager to use this action/method
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var o = new ArtistAddForm();
            o.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(o);
        }

        // GET: Properties/DetailsWithPhotoInfo/5
        public ActionResult DetailsWithMediaItem(int? id)
        {
            // Attempt to get the matching object
            var o = m.ArtistGetByIdWithMediaItemInfo(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Properties/Create

        // POST: Artists/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArtistAdd newItem)
        {

            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistAddNew(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        // Allow an Executive to use this action/method
        [Authorize(Roles = "Coordinator")]
        [Route("Artists/{id}/AddAlbum")]
        public ActionResult AddAlbum(int? id)
        {
            // Attempt to get the associated object
            var a = m.ArtistGetById(id.GetValueOrDefault());

            if (a == null)
            {
                return HttpNotFound();
            }
            else
            {

                var o = new AlbumAddForm();
                o.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                o.ArtistId = a.Id;
                o.ArtistName = a.Name;

                return View(o);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        [Route("Artists/{id}/AddAlbum")]
        public ActionResult AddAlbum(AlbumAdd newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.AlbumAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                // Attention - Must redirect to the Vehicles controller
                return RedirectToAction("details", "Albums", new { id = addedItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("artists/{id}/addmediaitem")]
        public ActionResult AddMediaItem(int? id)
        {
            // Attempt to get the matching object
            var o = m.ArtistGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Create a form
                var form = new MediaItemAddForm();

                // Configure its property values
                form.ArtistId = o.Id;
                form.ArtistName = o.Name;


                // Pass the object to the view
                return View(form);
            }
        }

        // POST: Properties/5/AddPhoto
        [HttpPost]
        [Route("artists/{id}/addmediaitem")]
        public ActionResult AddMediaItem(int? id, MediaItemAdd newItem)
        {
            // Validate the input
            // Two conditions must be checked
            if (!ModelState.IsValid && id.GetValueOrDefault() == newItem.ArtistId)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ArtistMediaItemAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("DetailsWithMediaItem", new { id = addedItem.Id });
            }
        }

        // GET: Artists/Edit/5
        [Authorize(Roles = "Executive")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artists/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artists/Delete/5
        [Authorize(Roles = "Executive")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
