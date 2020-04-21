using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP04Module05.Models;
using TP04Module05.Utils;

namespace TP04Module05.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDbPizza.Instance.Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza != null)
            {
                return View(pizza);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaViewModel vm = new PizzaViewModel();
            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaViewModel vm)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (FakeDbPizza.Instance.Pizzas.Any(p=>p.Nom.ToUpper()==vm.Pizza.Nom.ToUpper()))
                    {
                        ModelState.AddModelError("","Il existe déjà une pizza avec ce nom");
                        return View();
                    }

                    if (vm.Pizza.Ingredients.Count < 2 || vm.Pizza.Ingredients.Count > 5)
                    {
                        ModelState.AddModelError("", "Le nombre d'ingrédients doit être compris entre 2 et 5");
                        return View();
                    }

                    bool ingredientExist=false;
                    foreach (var pizza in FakeDbPizza.Instance.Pizzas)
                    {
                        if (pizza.Ingredients==vm.Pizza.Ingredients)
                        {
                            ingredientExist = true;
                        }
                    }

                    if (ingredientExist)
                    {
                        ModelState.AddModelError("", "Une liste d'ingrédients identique éxiste déjà");
                        return View();
                    }

                    FakeDbPizza.Instance.Pizzas.Add(vm.Pizza);

                    return RedirectToAction("Index");
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(x => x.Id == id);

            if (pizza != null)
            {
                PizzaViewModel vm = new PizzaViewModel();
                 
                vm.Pizza.Nom = pizza.Nom;
                vm.Pizza.Pate = pizza.Pate;
                vm.Pizza.Ingredients = pizza.Ingredients;
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PizzaViewModel pizza)
        {
            try
            {
                Pizza pizzaDb = FakeDbPizza.Instance.Pizzas.FirstOrDefault(p => p.Id == id);
                pizzaDb.Nom = pizza.Pizza.Nom;
                pizzaDb.Pate = pizza.Pizza.Pate;
                pizzaDb.Ingredients = pizza.Pizza.Ingredients;
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
            Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
            if (pizza != null)
            {
                return View(pizza);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDbPizza.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
                FakeDbPizza.Instance.Pizzas.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
