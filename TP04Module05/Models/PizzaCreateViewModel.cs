using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP04Module05.Models
{
    public class PizzaCreateViewModel
    {
        public Pizza Pizza {get;set;}
        public List<Ingredient> Ingredients { get; } = Pizza.IngredientsDisponibles;
        public List<int> IdIngredients { get; set; }
        public List<Pate> Pates { get; } = Pizza.PatesDisponibles;
        public int IdPate { get; set; }

    }
}