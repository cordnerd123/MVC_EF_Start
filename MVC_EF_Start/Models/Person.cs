using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start.Models
{
    public class Person
    {
        public int personID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
    }
    public class FavSentry
    {
        public string FavSentryID { get; set; }
        public Person Person { get; set; }
        public Sentry SentryObj { get; set; }
    }
}
