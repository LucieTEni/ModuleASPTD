using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module5TpPizza.Models
{
    public class CreatePizzaModelView
    {
        public Pizza pizza { get; set; }
        public List<Ingredient> ListeIngredient { get; set; }
        public List<Pate> Pates { get; set; }

        public List<int> ListIdIngredients { get; set; } = new List<int>();
        public int? IdPate { get; set; }
    }
}