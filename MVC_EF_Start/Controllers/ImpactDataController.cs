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
            //Sentry(); 
            if (id == 0) id = 1;
            SentryTable(id);
            return View();
        }
        public PartialViewResult Details(int id)
        {
            Sentry[] sendetails = new Sentry[1];

            sendetails[0] = dbContext.SentryEntries
                .Where(c => c.num == id)
                .FirstOrDefault();

            return PartialView(sendetails);
        }//Viewbag.whatever
        //also above updates when? below updates when?
        
        [HttpPost]
        public ViewResult Details()
        {
            Sentry[] sendetails = new Sentry[1];
            int id = 1;

            sendetails[0] = dbContext.SentryEntries
                .Where(c => c.num == id)
                .FirstOrDefault();

            ViewBag.Details = sendetails[0];

            return View("Details");
        }
        public PartialViewResult SentryTable(int id)
        {
            Sentry mysentry = new Sentry();
            Sentry[] mytable = new Sentry[10];

            int max = dbContext.SentryEntries.Count();

            if (id == -1) id = max - 9;
            else if (id > max) id = 1;

            for (int x = 0; x < 10; x++)
            {
                mysentry = dbContext.SentryEntries
                    .Where(c => c.num == id)
                    .FirstOrDefault();
                mytable[x] = mysentry;
                id++;
            }

            return PartialView(mytable);
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
            }
            dbContext.SaveChanges();
        }
    
    
    
    
    }
}
