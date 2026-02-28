using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Xml.Linq;
using CookieCookbook.IngredientsRepo;

namespace CookieCookbook.RecipesRepo
{
    public class Recipe
    {
        public Ingredients AviableIngredients { get; set; }
        public List<int> RecipeIDs { get; }

        public Recipe(Ingredients aviableIngredients)
        {
            AviableIngredients = aviableIngredients;
            RecipeIDs = new List<int>();
        }
        public int CountIngredients()
        {
            return RecipeIDs.Count;
        }
        public bool AddToRecipe(int id)
        {
            if (AviableIngredients.ContainsIngredient(id))
            {
                RecipeIDs.Add(id);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string result = string.Empty;
            foreach (int id in RecipeIDs)
            {
                result += AviableIngredients.GetIngredientByID(id).Name + ". ";
                result += AviableIngredients.GetIngredientByID(id).Description + "\n";
            }
            return result;
        }
    }
}
