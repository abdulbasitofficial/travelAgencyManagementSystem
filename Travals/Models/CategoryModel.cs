using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travals.Models
{
    public class CategoryModel
    {
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public byte[] Image { get; set; }
        public string Type { get; set; }

        TravelsEntities ctx = new TravelsEntities();
        public void insert_Category(CategoryModel c, HttpPostedFileBase PostedFile)
        {
            byte[] bytes;
            BinaryReader br = new BinaryReader(PostedFile.InputStream);
            bytes = br.ReadBytes(PostedFile.ContentLength);

            Category cat = new Category();
            cat.NameCategory = c.Category_Name;
            cat.Image = bytes;
            cat.Type = PostedFile.ContentType;
            ctx.Categories.Add(cat);
            ctx.SaveChanges();

        }
        public List<CategoryModel> dispaly_Category()
        {
            List<CategoryModel> files = null;

            files = ctx.Categories.Select(c => new CategoryModel()
            {
                Category_ID = c.ID,
                Category_Name = c.NameCategory,
                Image = c.Image,
                Type = c.Type
            }).ToList<CategoryModel>();

            return files;
        }
        public void delete_Category(int id)
        {

            var u = ctx.Categories.Where(c => c.ID == id).FirstOrDefault();
            ctx.Entry(u).State = System.Data.Entity.EntityState.Deleted;
            ctx.SaveChanges();

        }
        public CategoryModel upadate_Category(int id)
        {
            CategoryModel files = null;

            files = ctx.Categories.Where(c => c.ID == id).Select(c => new CategoryModel()
            {
                Category_ID = c.ID,
                Category_Name = c.NameCategory,
                Image = c.Image,
                Type = c.Type
            }).SingleOrDefault();

            return files;
        }
        public void upadate_Category(CategoryModel c)
        {

            var result = ctx.Categories.SingleOrDefault(b => b.ID == c.Category_ID);
            if (result != null)
            {
                result.NameCategory = c.Category_Name;
                result.Type = c.Type;
                result.Image = c.Image;
                ctx.SaveChanges();
            }

        }
        public List<SelectListItem> CategoryDropDownList()
        {
            List<CategoryModel> files = dispaly_Category();
            List<SelectListItem> category = new List<SelectListItem>();
            foreach (var item in files)
            {
                category.Add(new SelectListItem { Value = item.Category_ID.ToString(), Text = item.Category_Name.ToString() });
            }

            return category;
        }
    }
}