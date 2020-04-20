﻿using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start.Models
{
        // Model for parks data. These classes can be obtained by either using 
        // the Visual Studio editor (right-click -> Paste Special -> Paste Json as Classes)
        // or sites such as JsonToCSHarp

    public class SentryObject
    {
        public string count { get; set; }
        public Signature signature { get; set; }
        public Sentry[] data { get; set; }
    }

    public class Signature
    {
        public string source { get; set; }
        public string version { get; set; }
    }

    public class Sentry
    {
        public string ip { get; set; }
        public string range { get; set; }
        public string ps_cum { get; set; }
        public string diameter { get; set; }
        public string ps_max { get; set; }
        public string h { get; set; }
        public string last_obs { get; set; }
        public string v_inf { get; set; }
        public string fullname { get; set; }
        public string n_imp { get; set; }
        public string last_obs_jd { get; set; }
        public string ts_max { get; set; }
        [Key]
        public string id { get; set; }
        public string des { get; set; }
    }

    public class Person
    {
        int personID { get; set; }
        string fname { get; set; }
        string lname { get; set;}
        string email { get; set; }
    }
}
