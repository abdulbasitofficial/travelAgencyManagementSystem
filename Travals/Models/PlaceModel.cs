using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travals.Models;

namespace Travals.Models
{
    public class PlaceModel
    {
        public int Place_ID { get; set; }
        public float StarsNumber { get; set; }
        public int TotalUsers { get; set; }
        public decimal Stars { get; set; }
        public int City_ID { get; set; }
        public int Category_ID { get; set; }
        public string Place_Name { get; set; }
        public string Location { get; set; }
        public int Visibile { get; set; }
        public byte[] Image { get; set; }
        public string Type { get; set; }

        public IEnumerable<CategoryModel> category { get; set; }
        public IEnumerable<CityModel> city { get; set; }
        public IEnumerable<CategoryModel> Visibilty { get; set; }

        TravelsEntities ctx = new TravelsEntities();
        public void insert_Place(PlaceModel c, HttpPostedFileBase PostedFile)
        {
            byte[] bytes;
            BinaryReader br = new BinaryReader(PostedFile.InputStream);
            bytes = br.ReadBytes(PostedFile.ContentLength);

            Place cat = new Place();
            cat.NamePlace = c.Place_Name;
            cat.Image = bytes;
            cat.Type = PostedFile.ContentType;
            cat.CategoryID = c.Category_ID;
            cat.Location = c.Location;
            cat.Visibile = c.Visibile;
            cat.CityID = c.City_ID;
            ctx.Places.Add(cat);
            ctx.SaveChanges();

        }
        public List<PlaceModel> dispaly_Place(int UserID)
        {
            List<PlaceModel> files = null;

            files = ctx.Places.Where(w=>w.UserID==UserID).Select(c => new PlaceModel()
            {
                Place_ID = c.ID,
                Place_Name = c.NamePlace,
                Location=c.Location,
                Visibile=(int)c.Visibile,
                Image = c.Image,
                Type = c.Type
            }).ToList<PlaceModel>();

            return files;
        }
        public List<PlaceModel> dispaly_PlaceByCity(int CityID ,int CategoryID)
        {
            List<PlaceModel> files = null;
            files = ctx.Places.Where(w=>w.Visibile==1).Select(c => new PlaceModel()
            {
                Place_ID = c.ID,
                Category_ID =(int) c.CategoryID,
                City_ID = (int)c.CityID,
                Place_Name = c.NamePlace,
                Location = c.Location,
                Image = c.Image,
                Type = c.Type
            }).Where(w => w.City_ID== CityID && w.Category_ID==CategoryID).ToList<PlaceModel>();

            return files;
        }
        public void delete_Place(int id)
        {

            var u = ctx.Places.Where(c => c.ID == id).FirstOrDefault();
            ctx.Entry(u).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();

        }
        public PlaceModel upadate_Place(int id)
        {
            PlaceModel files = null;

            files = ctx.Places.Where(c => c.ID == id).Select(c => new PlaceModel()
            {
                Place_ID = c.ID,
                Place_Name = c.NamePlace,
                Location = c.Location,
                Image = c.Image,
                Type = c.Type
            }).SingleOrDefault();

            return files;
        }
        public void upadate_Place(PlaceModel c)
        {

            var result = ctx.Places.SingleOrDefault(b => b.ID == c.Place_ID);
            if (result != null)
            {
                result.NamePlace = c.Place_Name;
                result.Type = c.Type;
                result.Image = c.Image;
                result.Location = c.Location;
                ctx.SaveChanges();
            }

        }
        public List<CategoryModel> CategoryDropDownList()
        {
            CategoryModel cat = new CategoryModel();
            List<CategoryModel> category = new List<CategoryModel>();
            List<CategoryModel> list = cat.dispaly_Category();
            foreach (var item in list)
            {
                category.Add(new CategoryModel { Category_ID = item.Category_ID, Category_Name = item.Category_Name });
            }
            return category;
        }
        public List<CityModel> CityDropDownList()
        {
            CityModel ct = new CityModel();
            List<CityModel> city = new List<CityModel>();
            List<CityModel> list = ct.dispaly_City();
            foreach (var item in list)
            {
                city.Add(new CityModel { City_ID = item.City_ID, City_Name = item.City_Name });
            }
            return city;
        }
        public List<CategoryModel> VisibiltyDropDownList()
        {
            List<CategoryModel> category = new List<CategoryModel>();
            category.Add(new CategoryModel { Category_ID =0, Category_Name = "Private" });
            category.Add(new CategoryModel { Category_ID =1, Category_Name = "Public" });
            return category;
        }
    }
}