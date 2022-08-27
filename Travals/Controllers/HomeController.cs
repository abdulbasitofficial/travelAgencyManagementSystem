using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travals.Models;

namespace Travels.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CityModel cat = new CityModel();
            List<CityModel> files = cat.dispaly_City();
            return View(files);
        }
        [HttpPost]
        public ActionResult Search(string search)
        {
            CityModel cat = new CityModel();
            List<CityModel> files = cat.Search_City(search);
            return View(files);
        }
        public ActionResult Category(int ID)
        {
            Session["CityID"] = ID;
            CategoryModel cat = new CategoryModel();
            List<CategoryModel> files = cat.dispaly_Category();
            return View(files);
        }
        public ActionResult Place(int ID)
        {
            Session["PlaceID"] = ID;
            int CityID =int.Parse( Session["CityID"].ToString());
            PlaceModel cat = new PlaceModel();
            List<PlaceModel> files = cat.dispaly_PlaceByCity(CityID, ID);
            RatingModel rt = new RatingModel();

            ViewBag.StartAverage = rt .StarsAverage();
            return View(files);
        }
        public ActionResult Rating(int PlaceID,int Rate)
        {
            int UserID = int.Parse(Session["UserID"].ToString());
            RatingModel rt = new RatingModel();
            rt.Rating(PlaceID, Rate, UserID);

            return RedirectToAction("Place",new {ID= int.Parse(Session["PlaceID"].ToString())});
        }
    }
}