using Microsoft.AspNetCore.Mvc;
using MVC_EF_Start.Models;
using MVC_EF_Start.DataAccess;
using System.Linq;
using MVC_EF_Start.APIHandlerManager;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MVC_EF_Start.Controllers
{
    public class FireballController : Controller
    {
        public ApplicationDbContext dbContext;

        public FireballController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Fireball(int moid)
        {
            //Fireball(); 
            if (moid == 0) moid = 1;
            FireballTable(moid);
            return View();
        }
        public PartialViewResult Details(int moid)
        {
            Fireball[] firedetails = new Fireball[1];

            firedetails[0] = dbContext.FireballEntries
                .Where(c => c.chifre == moid)
                .FirstOrDefault();

            return PartialView(firedetails);
        }//Viewbag.whatever
         //also above updates when? below updates when?

        [HttpPost]
        public ViewResult Details()
        {
            Fireball[] firedetails = new Fireball[1];
            int moid = 1;

            firedetails[0] = dbContext.FireballEntries
                .Where(c => c.chifre == moid)
                .FirstOrDefault();

            ViewBag.Details = firedetails[0];

            return View("Details");
        }
        public PartialViewResult FireballTable(int moid)
        {
            Fireball myfireball = new Fireball();
            Fireball[] mytable1 = new Fireball[10];

            int max = dbContext.FireballEntries.Count();

            if (moid == -1) moid = max - 9;
            else if (moid > max) moid = 1;

            for (int x = 0; x < 10; x++)
            {
                myfireball = dbContext.FireballEntries
                    .Where(c => c.chifre == moid)
                    .FirstOrDefault();
                mytable1[x] = myfireball;
                moid++;
            }

            return PartialView(mytable1);
        }

       /* public void Fireball()
        {
            FireballHandler webHandler = new FireballHandler();
            FireballObject fireballData = webHandler.GetFireballObjects();

            int x = 1;
            foreach (Fireball s in fireballData.data)
            {
                IQueryable<Fireball> firetest = dbContext.FireballEntries
                        .Where(c => c.moid == s.moid);

                if (firetest == s | firetest.Count() == 0)
                {
                    s.chifre = x;
                    x++;
                    dbContext.FireballEntries.Add(s);
                }
            }
            dbContext.SaveChanges();
            */
        }




    }

