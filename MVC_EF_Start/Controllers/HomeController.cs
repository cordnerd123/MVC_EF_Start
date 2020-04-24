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

        public ViewResult SignUp()
        {
            Person myperson = new Person();
            return View("Index");
        }

        [HttpPost]
        public ViewResult SignUp(Person myperson)
        {
            if (myperson.fname == null) return View("Index");
            if (myperson.lname == null) return View("Index");
            if (myperson.email == null) return View("Index");

            dbContext.People.Add(myperson);
            dbContext.SaveChanges();
            return View("Index");
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
