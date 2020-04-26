using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MVC_EF_Start.APIHandlerManager;
using Microsoft.AspNetCore.Mvc;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;


namespace MVC_EF_Start.Controllers
{
    public class ImpactDataController : Controller
    {
        public ApplicationDbContext dbContext;

        public ImpactDataController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult ImpactData(int id)
        {
            if (id == 0) id = 1;
            else if (id == -9)
            {
                Sentry();
                id = 1;
                ViewBag.Message = "Sentry Data was refreshed as of " + DateTime.Now;
            }

            if (dbContext.SentryEntries.Count() < 2) Sentry();
            SentryTable(id);
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewBag.Message = TempData["message"];
            FavSentry sendetails = new FavSentry();

            sendetails.SentryObj = dbContext.SentryEntries
                .Where(c => c.num == id )
                .FirstOrDefault();

            return View(sendetails);
        }

        [HttpPost]
        public ActionResult SenFav(FavSentry myobj)
        {
            string email = myobj.Person.email;

            Person myperson = dbContext.People
                .Where(c => c.email == email)
                .FirstOrDefault();

            string id = myobj.SentryObj.id;

            Sentry mysentry = dbContext.SentryEntries
                .Where(c => c.id == id)
                .FirstOrDefault();

            if (myperson == null)
            {
                TempData["message"] = "You do not have an account. You need to Sign up first";
                return RedirectToAction("Details/" + mysentry.num);
            }

            FavSentry myfav = new FavSentry();
            myfav.SentryObj = mysentry;
            myfav.Person = myperson;                                        
            myfav.FavSentryID = myfav.Person.personID + myfav.SentryObj.id;   //set key

            dbContext.FavSentries.Add(myfav);
            dbContext.SaveChanges();
            ModelState.Clear();
            TempData["message"] = "You successfully favorited this object!";
            return RedirectToAction("Details/" + mysentry.num);
        }

        public PartialViewResult SentryTable(int id)
        {
            Sentry[] mytable = new Sentry[10];
            ChartModel mychart = new ChartModel();
            string[] labels = new string[10];
            int[] data = new int[10];

            int max = dbContext.SentryEntries.Count();

            if (id == -1) id = max - 9;
            else if (id > max) id = 1;

            for (int x = 0; x < 10; x++)
            {
                Sentry mysentry = dbContext.SentryEntries
                    .Where(c => c.num == id)
                    .FirstOrDefault();

                mytable[x] = mysentry;
                labels[x] = mysentry.des;
                data[x] =  Convert.ToInt32(mysentry.n_imp);

                id++;
            }

            mychart.SenObj = mytable;
            mychart.Labels = String.Join(",", labels.Select(d=>"'" + d + "'"));
            mychart.Data = String.Join(",", data.Select(d => d));
            mychart.ChartType = "bar";
            mychart.Title = "Number of Potential Impacts per Designation";


            return PartialView(mychart);
        }

        public void Sentry()
        {
            APIHandler webHandler = new APIHandler();
            SentryObject sentryData = webHandler.GetSentryObjects();

            int x = 1;
            foreach (Sentry s in sentryData.data)
            {
                IQueryable<Sentry> sentest = dbContext.SentryEntries
                        .Where(c => c.id == s.id);

                if (sentest == s | sentest.Count() == 0)
                {
                    s.num = x;
                    x++;
                    dbContext.SentryEntries.Add(s);
                }
                else
                {
                    s.num = x;
                    x++;
                    dbContext.SentryEntries.Update(s);
                }
            }
            dbContext.SaveChanges();
        }
    
    
    
    
    }
}
