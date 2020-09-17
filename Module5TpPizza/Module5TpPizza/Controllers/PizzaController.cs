using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TpPizza.Controllers
{
    public class PizzaController : Controller
    {
        static List<Pizza> listpizza;
        static List<Ingredient> ingredients = Pizza.IngredientsDisponibles;
        static List<Pate> pate = Pizza.PatesDisponibles;
        static int idPizza;
        public PizzaController()
        {
            if(listpizza == null)
            {
                idPizza = 0;
                listpizza = new List<Pizza>();
            }
        }

        // GET: Pizza
        public ActionResult Index()
        {
            if (listpizza == null)
            {
                return RedirectToRoute("Home/Index");
            }
            return View(listpizza);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            var Pizza = listpizza.FirstOrDefault(i => i.Id == id);
            if (Pizza == null)
            {
                return RedirectToAction("Index");
            }
            return View(Pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(Pizza pizza)
        {
            try
            {
                var listeIngreselected = new List<Ingredient>();
                var PateSelected = pate.FirstOrDefault(p => p.Id.ToString().Equals(pizza.Pate.Id));
                foreach(var inge in pizza.Ingredients)
                {
                    listeIngreselected.Add(ingredients.FirstOrDefault(p => p.Id.ToString().Equals(inge.Id)));
                };
                listpizza.Add(
                    new Pizza
                    {
                        Id = idPizza,
                        Nom = pizza.Nom,
                        Pate = PateSelected,
                        Ingredients = listeIngreselected
                    }); ; ;
                
                idPizza++;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            var Pizza = listpizza.FirstOrDefault(i => i.Id == id);
            if (Pizza == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(Pizza);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(Pizza pizza)
        {
            try
            {
                var listeIngreselected = new List<Ingredient>();
                var PateSelected = pate.FirstOrDefault(p => p.Id == pizza.Pate.Id);
                foreach (var inge in pizza.Ingredients)
                {
                    listeIngreselected.Add(ingredients.FirstOrDefault(p => p.Id == inge.Id));
                };
                listpizza.Add(
                    new Pizza
                    {
                        Nom = pizza.Nom,
                        Pate = PateSelected,
                        Ingredients = listeIngreselected
                    }); ; ;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            var Pizza = listpizza.FirstOrDefault(i => i.Id == id);
            return View(Pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                var Pizza = listpizza.FirstOrDefault(i => i.Id == id);
                if(Pizza!= null)
                {
                    listpizza.Remove(Pizza);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
