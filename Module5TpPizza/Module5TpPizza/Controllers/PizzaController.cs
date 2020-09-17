using BO;
using Module5TpPizza.Models;
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
            Pizza Pizza = listpizza.FirstOrDefault(i => i.Id == id);

            if (Pizza == null)
            {
                return RedirectToAction("Index");
            }
            return View(Pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            CreatePizzaModelView viewPizza = new CreatePizzaModelView { 
            ListeIngredient = Pizza.IngredientsDisponibles,
            Pates = Pizza.PatesDisponibles};

            return View(viewPizza);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(CreatePizzaModelView CreatePizza)
        {
            try
            {
                Pizza pizzCreate = CreatePizza.pizza;
                pizzCreate.Pate = pate.FirstOrDefault(pate => pate.Id == CreatePizza.IdPate);
                pizzCreate.Ingredients = ingredients.Where(i => CreatePizza.ListIdIngredients.Contains(i.Id)).ToList();
                pizzCreate.Nom = CreatePizza.pizza.Nom;
                pizzCreate.Id = idPizza;

                listpizza.Add(pizzCreate);
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
            CreatePizzaModelView viewPizza = new CreatePizzaModelView
            {
                ListeIngredient = Pizza.IngredientsDisponibles,
                Pates = Pizza.PatesDisponibles
            };
            viewPizza.pizza = listpizza.FirstOrDefault(i => i.Id == id);

            if (viewPizza.pizza.Pate != null)
            {
                viewPizza.IdPate = viewPizza.pizza.Pate.Id;
            }
            
            if (viewPizza.pizza.Ingredients.Any())
            {
                viewPizza.ListIdIngredients = viewPizza.pizza.Ingredients.Select(i => i.Id).ToList();
            }
            return View(viewPizza);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(CreatePizzaModelView EditPizza)
        {
            try
            {
                Pizza pizzaEdit = listpizza.FirstOrDefault(p => p.Id == EditPizza.pizza.Id);
                pizzaEdit.Nom = EditPizza.pizza.Nom;
                pizzaEdit.Ingredients = ingredients.Where(i => EditPizza.ListIdIngredients.Contains(i.Id)).ToList();
                pizzaEdit.Pate = pate.FirstOrDefault(pate => pate.Id == EditPizza.IdPate);

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
