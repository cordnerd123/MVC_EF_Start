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

        public IActionResult Fireball(int fnum)
        {
            LoadFireball();
            if (fnum == 0) fnum= 1;
            FireballTable(fnum);
            return View();
        }

        public IActionResult Details(int id)
        {
            ViewBag.Message = TempData["message"];
            FavFire firedetails = new FavFire();

            firedetails.FireObj = dbContext.FireballEntries
                .Where(c => c.fnum == id)
                .FirstOrDefault();
            
            return PartialView(firedetails);
        }

        [HttpPost]
        public ActionResult FireFav(FavFire myobj)
        {
            string email = myobj.Person.email;

            Person myperson = dbContext.People
                .Where(c => c.email == email)
                .FirstOrDefault();

            string fobjname = myobj.FireObj.objectName;

            Fireball myfire = dbContext.FireballEntries
                .Where(c => c.objectName == fobjname)
                .FirstOrDefault();

            if (myperson == null)
            {
                TempData["message"] = "You do not have an account. You need to Sign up first";
                return RedirectToAction("Details/" + myfire.fnum);
            }

            FavFire myfav = new FavFire();
            myfav.FireObj = myfire;
            myfav.Person = myperson;
            myfav.FavFireID = myfav.Person.personID + myfav.FireObj.objectName;

            dbContext.FavFireballs.Add(myfav);
            dbContext.SaveChanges();
            ModelState.Clear();
            TempData["message"] = "You successfully favorited this object!";
            return RedirectToAction("Details/" + myfire.fnum);
        }

        public PartialViewResult FireballTable(int id)
        {
            Fireball myfireball = new Fireball();
            Fireball[] mytable = new Fireball[10];

            int max = dbContext.FireballEntries.Count();

            if (id == -1) id = max - 9;
            else if (id > max) id = 1;

            for (int x = 0; x < 10; x++)
            {
                myfireball = dbContext.FireballEntries
                    .Where(c => c.fnum == id)
                    .FirstOrDefault();
                mytable[x] = myfireball;
                id++;
            }
            return PartialView(mytable);
        }

        public void LoadFireball()
        {
            FireballHandler webHandler = new FireballHandler();
            FireballObject fireballData = webHandler.GetFireballObjects();

            int x = 1;
            foreach (Fireball s in fireballData.data)
            {
                IQueryable<Fireball> firetest = dbContext.FireballEntries
                        .Where(c => c.objectName == s.objectName);

                if (firetest == s | firetest.Count() == 0)
                {
                    s.fnum = x;
                    x++;
                    dbContext.FireballEntries.Add(s);
                }
                else
                {
                    s.fnum = x;
                    x++;
                    dbContext.FireballEntries.Update(s);
                }
            }
            dbContext.SaveChanges();
        }
    }
}

