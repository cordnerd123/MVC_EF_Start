using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace MVC_EF_Start.Models
{
    // Model for parks data. These classes can be obtained by either using 
    // the Visual Studio editor (right-click -> Paste Special -> Paste Json as Classes)
    // or sites such as JsonToCSHarp

    public class FireballObject
    {
        public string count { get; set; }
        public Signaturef signature { get; set; }
        public Fireball[] data { get; set; }
    }

    public class Signaturef
    {
        public string source { get; set; }
        public string version { get; set; }
    }

    public class Fireball
    {
        public int fnum { get; set; }
        public string neo1kmScore { get; set; }
        public string lastRun { get; set; }
        public string uncP1 { get; set; }
        public string dec { get; set; }
        public string neoScore { get; set; }
        public string rating { get; set; }
        public string rate { get; set; }
        public string unc { get; set; }
        public string phaScore { get; set; }
        public string ra { get; set; }
        public string elong { get; set; }
        public string nObs { get; set; }
        public string arc { get; set; }
        public string tEphem { get; set; }
        [Key]
        public string objectName { get; set; }
        public string tisserandScore { get; set; }
        public string caDist { get; set; }
        public string vInf { get; set; }
        public string H { get; set; }
        public string rmsN { get; set; }
        public string ieoScore { get; set; }
        public string geocentricScore { get; set; }
        public string moid { get; set; }
        public string Vmag { get; set; }
    }

    public class FavFire
    {
        public string FavFireID { get; set; }
        public Person Person { get; set; }
        public Fireball FireObj { get; set; }
    }
}