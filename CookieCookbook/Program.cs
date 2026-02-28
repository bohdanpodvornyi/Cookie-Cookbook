using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using CookieCookbook.IngredientsRepo;
using CookieCookbook.RecipesRepo;

namespace CookieCookbook
{
    internal partial class Program
    {
        enum FileFormat
        {
            Txt,
            Json,
        }

        static void Main(string[] args)
        {
            const string fileName = "recipes";
            const FileFormat format = FileFormat.Json;

            Ingredients aviableIngredients = ReturnPrecodedIngredients();
            Recipe recipe = new Recipe(aviableIngredients);

            IRecipeEditor editor;
            switch (format)
            {
                case FileFormat.Txt:
                    editor = new RecipeEditorTXT(aviableIngredients);
                    break;
                case FileFormat.Json:
                    editor = new RecipeEditorJSON(aviableIngredients);
                    break;
            }
            List<Recipe> loadedRecipes = editor.LoadRecipeFromFile(fileName);
            if (loadedRecipes != null)
            {
                for (int i = 0; i < loadedRecipes.Count; i++)
                {
                    Console.WriteLine($"\t\t****{i + 1}****");
                    Console.WriteLine(loadedRecipes[i]);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Create a new cookie recipe! Available ingredients are: ");
            Console.WriteLine(aviableIngredients);
            while (true)
            {
                Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");
                if (!int.TryParse(Console.ReadLine(), out int ingredientId))
                {
                    if (recipe.CountIngredients() > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Add at least one ingredient. Recipe can't be empty!");
                    }
                }
                else if (!recipe.AddToRecipe(ingredientId))
                {
                    Console.WriteLine("Wrong ingredient ID!");
                }
            }

            Console.WriteLine("Recipe added:");
            Console.WriteLine(recipe);

            editor.SaveRecipeToFile(recipe, fileName);
        }

        static public Ingredients ReturnPrecodedIngredients()
        {
            Ingredients ingredientsAll = new Ingredients();
            ingredientsAll.AddIngredient(new WheatFlour(1, "Wheat flour", "Sieve. Add to other ingredients."));
            ingredientsAll.AddIngredient(new CoconutFlour(2, "Coconut flour", "Sieve. Add to other ingredients."));
            ingredientsAll.AddIngredient(new Butter(3, "Butter", "Melt on low heat. Add to other ingredients."));
            ingredientsAll.AddIngredient(new Chocolate(4, "Chocolate", "Melt in a water bath. Add to other ingredients."));
            ingredientsAll.AddIngredient(new Sugar(5, "Sugar", "Add to other ingredients."));
            ingredientsAll.AddIngredient(new Cardamom(6, "Cardamom", "Take half a teaspoon. Add to other ingredients."));
            ingredientsAll.AddIngredient(new Cinnamon(7, "Cinnamon", "Take half a teaspoon. Add to other ingredients."));
            ingredientsAll.AddIngredient(new CocoaPowder(8,"Cocoa powder", "Add to other ingredients."));
            return ingredientsAll;
        }
    }
}
