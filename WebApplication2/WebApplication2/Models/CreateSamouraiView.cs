using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Data;

namespace WebApplication2.Models
{
    public class CreateSamouraiView
    {
        public Samourai Samourai { get; set; }
        public List<Arme> ListeArmes
        {
            get; set;
        }
        public int? IdArmes { get; set; }
    }
}