﻿using System;
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

        public IActionResult Details(int id)
        {
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

            FavSentry myfav = new FavSentry();
            myfav.SentryObj = mysentry;
            myfav.Person = myperson;                                        
            myfav.FavSentryID = myfav.Person.personID + myfav.SentryObj.id;   //set key

            dbContext.FavSentries.Add(myfav);
            dbContext.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("ImpactData");
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
