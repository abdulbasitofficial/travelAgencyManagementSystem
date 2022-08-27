using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travals.Models;

namespace Travels.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(CityModel c, HttpPostedFileBase PostedFile)
        {
            if (ModelState.IsValid)
            {
                CityModel cat = new CityModel();
                cat.insert_City(c, PostedFile);

                return RedirectToAction("City");
            }
            else
            {
                return View("AddCategory");
            }
        }
        [HttpGet]
        public ActionResult City(int page = 1)
        {
            CityModel cat = new CityModel();
            List<CityModel> files = cat.dispaly_City();
            ViewBag.TotalPages = (files.Count() / 20) + 1;
            files = files.Skip((page - 1) * 20).Take(20).ToList();
            return View(files);
        }
        [HttpGet]
        [AllowAnonymous]

        public ActionResult Delete(int id)
        {
            CityModel cat = new CityModel();
            cat.delete_City(id);
            return RedirectToAction("City");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CityModel cat = new CityModel();
            CityModel c = cat.upadate_City(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(CityModel c)
        {
            CityModel cat = new CityModel();
            cat.upadate_City(c);
            return RedirectToAction("City");
        }
    }
}