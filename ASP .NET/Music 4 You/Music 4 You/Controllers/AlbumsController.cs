using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment8.Models;

namespace Assignment8.Controllers
{
    public class AlbumsController : Controller
    {
        Manager m = new Manager();

        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var o = m.AlbumGetAllDetails(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(o);
            }
        }

        [Authorize(Roles="Clerk")]
        [Route("Albums/{id}/AddTrack")]
        [HttpPost]
        // GET: Albums/Create
        public ActionResult AddTrack(int? id)
        {
            var addedItem = m.AlbumGetById(id.GetValueOrDefault());

            if (addedItem == null)
                return HttpNotFound();
            else
            {
                var o = new TrackAddForm();
                o.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
                o.AlbumId=addedItem.Id;
                o.AlbumName= addedItem.Name;

                return View(o);
            }
        }

        [Route("Albums/{id}/AddTrack")]
        [HttpPost]
        public ActionResult AddTrack(TrackAdd newItem)
        {
            var addedItem = m.TrackAddNew(newItem);

            if (addedItem == null)
                return View(newItem);
            else
                return RedirectToAction("details", "Track", new { id = addedItem.Id });
        }



        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            var o = m.AlbumGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var Form = AutoMapper.Mapper.Map<AlbumEditForm>(o);

                return View(Form);
            }
        }

        // POST: Albums/Edit/5
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

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
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
