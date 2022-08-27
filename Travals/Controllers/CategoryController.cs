using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travals.Models;

namespace Travals.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(CategoryModel c, HttpPostedFileBase PostedFile)
        {
            if (ModelState.IsValid)
            {
                CategoryModel cat = new CategoryModel();
                cat.insert_Category(c,PostedFile);

                return RedirectToAction("Category");
            }
            else
            {
                return View("AddCategory");
            }
        }
        [HttpGet]
        public ActionResult Category(int page = 1)
        {
            CategoryModel cat = new CategoryModel();
            List<CategoryModel> files = cat.dispaly_Category();
            ViewBag.TotalPages = (files.Count() / 20) + 1;
            files = files.Skip((page - 1) * 20).Take(20).ToList();
            return View(files);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Delete(int id)
        {
            CategoryModel cat = new CategoryModel();
            cat.delete_Category(id);
            return RedirectToAction("Category");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryModel cat = new CategoryModel();
            CategoryModel c = cat.upadate_Category(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(CategoryModel c)
        {
            CategoryModel cat = new CategoryModel();
            cat.upadate_Category(c);
            return RedirectToAction("Category");
        }
    }
}