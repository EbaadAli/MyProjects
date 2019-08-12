using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment8.Models;


namespace Assignment8.Controllers
{
    // Protect all actions
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET:
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoadData/Artist
        public ActionResult Artist()
        {
            if (m.LoadArtists())
               ViewBag.Result = "Artist data was loaded";
            else
                ViewBag.Result = "Error";
            
            return View("result");
        }

        // GET: LoadData/Album
        public ActionResult Album()
        {
            if (m.LoadAlbum())
                ViewBag.Result = "Album data was loaded";
            else
                ViewBag.Result = "Error";

            return View("result");
        }

        // GET: LoadData/Track
        public ActionResult Track()
        {
            if (m.LoadTracks())
                ViewBag.Result = "Track data loaded";
            else
                ViewBag.Result = "Error";

            return View("result");
        }

        public ActionResult Genre()
        {
           ViewBag.Result = m.LoadGenre() ? "Genre data loaded" : ("Error");
            return View("result");
        }

        public ActionResult Remove()
        {
            ViewBag.Result = m.RemoveDatabase() ? "Database deleted" : "Error";

            return View("result");
        }

        public ActionResult RemoveData()
        {
            ViewBag.Result = m.RemoveAll() ? "Data deleted" : "Error";


            return View("result");


        }

    }
}