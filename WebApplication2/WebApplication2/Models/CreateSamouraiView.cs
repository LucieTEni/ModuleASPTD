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
        private WebApplication2Context db = new WebApplication2Context();
        public Samourai Samourai { get; set; }
        public List<Arme> ListeArmes
        {
            get
            {
                return db.Armes.ToList();
            }
        }
        public int? IdArmes { get; set; }
    }
}