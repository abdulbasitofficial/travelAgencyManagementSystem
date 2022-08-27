using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travals.Models
{
    public class RatingModel
    {
        public int ID { get; set; }
        public int Rate { get; set; }
        public int UserID { get; set; }
        public int Place_ID { get; set; }
        public bool IsActive { get; set; }

        TravelsEntities ctx = new TravelsEntities();
        public void Rating(int PlaceID, int Rate, int UserID)
        {
            var check = ctx.Ratings.Where(w => w.UserID == UserID && w.Place_ID == PlaceID && w.IsActive == true).FirstOrDefault();
            if (check != null)
            {
                check.IsActive = false;
                ctx.SaveChanges();

                Rating rt = new Rating();
                rt.Rate = Rate;
                rt.Place_ID = PlaceID;
                rt.IsActive = true;
                rt.UserID = UserID;
                ctx.Ratings.Add(rt);
                ctx.SaveChanges();
            }
            else
            {
                Rating rt = new Rating();
                rt.Rate = Rate;
                rt.Place_ID = PlaceID;
                rt.IsActive = true;
                rt.UserID = UserID;
                ctx.Ratings.Add(rt);
                ctx.SaveChanges();
            }

        }
        public List<PlaceModel> StarsAverage()
        {
            List<PlaceModel> list = new List<PlaceModel>();

            var ratingCount = from f in ctx.Ratings.Where(w=>w.IsActive==true).GroupBy(f => f.Place_ID)
                              select new
                              {
                                  count = f.Sum(c => c.Rate),
                                  f.FirstOrDefault().Place_ID,
                              };

            var userCount = ctx.Ratings.Where(w => w.IsActive == true).GroupBy(l => l.Place_ID)
              .Select(g => new
              {
                  g.FirstOrDefault().Place_ID,
                  Count = g.Select(l => l.UserID).Distinct().Count()
              });
            foreach (var item in ratingCount)
            {
                foreach (var file in userCount)
                {
                    if (item.Place_ID == file.Place_ID)
                    {
                        decimal st = Math.Floor((decimal)item.count / file.Count);
                        list.Add(new PlaceModel
                        {
                            Place_ID = (int)item.Place_ID,
                            Stars = st,
                            StarsNumber = (float)item.count,
                            TotalUsers = file.Count
                        });
                    }
                }
            }
            return list;
        }
    }
}