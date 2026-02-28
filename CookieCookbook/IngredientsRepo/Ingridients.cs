using System;
using System.Collections.Generic;

namespace CookieCookbook.IngredientsRepo
{
    public class Ingredients
    {
        public List<Ingredient> IngredientsList { get; }
        public Ingredients()
        {
            IngredientsList = new List<Ingredient>();
        }
        public void AddIngredient(Ingredient ingredient)
        {
            IngredientsList.Add(ingredient);
        }
        public bool ContainsIngredient(int id)
        {
            foreach (Ingredient ingredient in IngredientsList)
            {
                if (ingredient.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        public Ingredient GetIngredientByID(int id)
        {
            foreach (Ingredient ingredient in IngredientsList)
            {
                if (ingredient.ID == id)
                {
                    return ingredient;
                }
            }
            return null;
        }
        public override string ToString()
        {
            string result = string.Empty;
            if (IngredientsList.Count == 0 || IngredientsList is null)
            {
                result += "Ingredients list is empty.\n";
            }
            else
            {
                for (int i = 0; i < IngredientsList.Count; i++)
                {
                    result += $"{i + 1}. {IngredientsList[i].Name}\n";
                }
            }
            return result;
        }
    }
}
