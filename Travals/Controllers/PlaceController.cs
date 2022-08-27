using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travals.Models;

namespace Travals.Controllers
{
    public class PlaceController : Controller
    {
        public ActionResult Add()
        {
            PlaceModel cat = new PlaceModel();
            cat.category = cat.CategoryDropDownList();

            cat.city = cat.CityDropDownList();
            cat.Visibilty = cat.VisibiltyDropDownList();

            return View(cat);
        }
        [HttpPost]
        public ActionResult Add(PlaceModel c, HttpPostedFileBase PostedFile)
        {
            
                PlaceModel cat = new PlaceModel();
                cat.insert_Place(c, PostedFile);
                return RedirectToAction("Place");
          
            
        }
        [HttpGet]
        public ActionResult Place(int page = 1)
        {
            int UserID = int.Parse(Session["UserID"].ToString());
            PlaceModel cat = new PlaceModel();
            List<PlaceModel> files = cat.dispaly_Place(UserID);
            ViewBag.TotalPages = (files.Count() / 20) + 1;
            files = files.Skip((page - 1) * 20).Take(20).ToList();
            return View(files);
        }
        [HttpGet]
        [AllowAnonymous]

        public ActionResult Delete(int id)
        {
            PlaceModel cat = new PlaceModel();
            cat.delete_Place(id);
            return RedirectToAction("Place");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            PlaceModel cat = new PlaceModel();
            PlaceModel c = cat.upadate_Place(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(PlaceModel c)
        {
            PlaceModel cat = new PlaceModel();
            cat.upadate_Place(c);
            return RedirectToAction("Place");
        }
    }
}