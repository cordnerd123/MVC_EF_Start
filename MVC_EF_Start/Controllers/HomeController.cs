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
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            Person myperson = new Person();
            return View(myperson);
        }

        [HttpPost]
        public ViewResult SignUp(Person myperson)
        {
            if (myperson.fname == null) return View();
            if (myperson.lname == null) return View();
            if (myperson.email == null) return View();

            Person persearch = dbContext.People
                .Where(c => c.personID == myperson.personID)
                .FirstOrDefault();
            
            if (persearch == null)
            {
                dbContext.People.Add(myperson);
                dbContext.SaveChanges();
                ViewBag.Message = "You were successfully added to the database!";
            }
            else if (myperson.personID > 0)
            {
                dbContext.Entry(persearch).CurrentValues.SetValues(myperson);
                dbContext.SaveChanges();
                ViewBag.Message = "You successfully updated your info!";
            }
            else
                ViewBag.Message = "You are already signed up!";

            ModelState.Clear();
            return View(myperson);
        }

        public ViewResult ModPerson(Person myperson)
        {
            if (myperson.email == null) return View();

            Person persearch = dbContext.People
                .Where(c => c.email == myperson.email)
                .FirstOrDefault();

            if (persearch != null)
            {
                return View("Signup",persearch);
            }
            else
            {
                ViewBag.Message = "There Was no person found with email you entered";
                Person myperson1 = new Person();
                return View("SignUp",myperson1);
            }        
        }

        [HttpPost]
        public ViewResult DelPerson(Person myperson)
        {
            if (myperson.personID == 0)
                ViewBag.Message = "You need to look up your info before you delete.";
            else
            {
                try
                {
                    dbContext.Remove(dbContext.People.Single(c => c.email == myperson.email));

                    var resultsen = dbContext.FavSentries
                        .Where(c => c.Person.personID == myperson.personID)
                        .FirstOrDefault();

                    var resultfire = dbContext.FavFireballs
                        .Where(c => c.Person.personID == myperson.personID)
                        .FirstOrDefault();

                    if (resultsen != null)
                        dbContext.Remove(dbContext.FavFireballs.Single(c => c.Person.personID == myperson.personID));
                    if (resultfire != null)
                        dbContext.Remove(dbContext.FavSentries.Single(c => c.Person.personID == myperson.personID));

                    dbContext.SaveChanges();
                    ViewBag.Message = "You were removed from the database";
                }
                catch
                {
                    ViewBag.Message = "You were not in the database";
                }
            }

            Person newperson = new Person();
            return View("SignUp",newperson);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
