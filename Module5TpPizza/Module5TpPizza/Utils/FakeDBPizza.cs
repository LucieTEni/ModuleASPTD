using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Module5TpPizza.Utils
{
    public class FakeDBPizza
    {
        private List<Pizza> listPizza { get; } = new List<Pizza>();
        public List<Pizza> ListDePizza 
        {
            get { return listPizza; } 
        }
        public List<Ingredient> ListeIngredientsDisponibles 
        {
            get { return Pizza.IngredientsDisponibles; }
        }

        public List<Pate> ListeDePatesDisponibles
        {
            get { return Pizza.PatesDisponibles; }
        }
    }
}