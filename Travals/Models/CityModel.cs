using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travals.Models
{
    public class CityModel
    {
        public int City_ID { get; set; }
        public string City_Name { get; set; }
        public byte[] Image { get; set; }
        public string Type { get; set; }

        TravelsEntities ctx = new TravelsEntities();
        public void insert_City(CityModel c, HttpPostedFileBase PostedFile)
        {
            byte[] bytes;
            BinaryReader br = new BinaryReader(PostedFile.InputStream);
            bytes = br.ReadBytes(PostedFile.ContentLength);

            City cat = new City();
            cat.NameCity = c.City_Name;
            cat.Image = bytes;
            cat.Type = PostedFile.ContentType;
            ctx.Cities.Add(cat);
            ctx.SaveChanges();

        }
        public List<CityModel> dispaly_City()
        {
            List<CityModel> files = null;

            files = ctx.Cities.Select(c => new CityModel()
            {
                City_ID = c.ID,
                City_Name = c.NameCity,
                Image = c.Image,
                Type = c.Type
            }).ToList<CityModel>();

            return files;
        }
        public List<CityModel> Search_City(string Name)
        {
            List<CityModel> files = null;
            files = ctx.Cities.Select(c => new CityModel()
            {
                City_ID = c.ID,
                City_Name = c.NameCity,
                Image = c.Image,
                Type = c.Type
            }).Where(w => w.City_Name.Contains(Name)).ToList<CityModel>();
            return files;
        }
        public void delete_City(int id)
        {

            var u = ctx.Cities.Where(c => c.ID == id).FirstOrDefault();
            ctx.Entry(u).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();

        }
        public CityModel upadate_City(int id)
        {
            CityModel files = null;

            files = ctx.Cities.Where(c => c.ID == id).Select(c => new CityModel()
            {
                City_ID = c.ID,
                City_Name = c.NameCity,
                Image = c.Image,
                Type = c.Type
            }).SingleOrDefault();

            return files;
        }
        public void upadate_City(CityModel c)
        {

            var result = ctx.Cities.SingleOrDefault(b => b.ID == c.City_ID);
            if (result != null)
            {
                result.NameCity = c.City_Name;
                result.Type = c.Type;
                result.Image = c.Image;
                ctx.SaveChanges();
            }

        }
        public List<SelectListItem> CityDropDownList()
        {
            List<CityModel> files = dispaly_City();
            List<SelectListItem> category = new List<SelectListItem>();
            foreach (var item in files)
            {
                category.Add(new SelectListItem { Value = item.City_ID.ToString(), Text = item.City_Name.ToString() });
            }

            return category;
        }
    }
}