using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using CookieCookbook.IngredientsRepo;

namespace CookieCookbook.RecipesRepo
{
    public interface IRecipeEditor
    {
        Ingredients AviableIngredients { get; set; }

        void SaveRecipeToFile(Recipe recipe, string fileName);
        List<Recipe> LoadRecipeFromFile(string fileName);
    }
    public class RecipeEditorJSON : IRecipeEditor
    {
        public RecipeEditorJSON(Ingredients aviableIngredients)
        {
            AviableIngredients = aviableIngredients;
        }
        public Ingredients AviableIngredients { get; set; }
        public void SaveRecipeToFile(Recipe recipe, string fileName)
        {
            string fullFilePath = fileName + ".json";
            string fileOutput = string.Join(",", recipe.RecipeIDs);

            if (File.Exists(fullFilePath))
            {
                List<string> fileContent = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(fullFilePath));
                fileContent.Insert(0, fileOutput);
                File.WriteAllText(fullFilePath, JsonSerializer.Serialize(fileContent));
            }
            else
            {
                File.WriteAllText(fullFilePath, JsonSerializer.Serialize(new List<string> { fileOutput }));
            }
        }
        public List<Recipe> LoadRecipeFromFile(string fileName)
        {
            string fullFilePath = fileName + ".json";
            if (!File.Exists(fullFilePath))
            {
                return null;
            }

            string fileContent = File.ReadAllText(fullFilePath);
            List<string> allRecipes = JsonSerializer.Deserialize<List<string>>(fileContent);
            List<Recipe> resultRecipes = new List<Recipe>();

            foreach (var recipeString in allRecipes)
            {
                Recipe tempRecipe = new Recipe(AviableIngredients);
                foreach (string id in recipeString.Split(',').ToList())
                {
                    if (int.TryParse(id, out int idInt))
                    {
                        tempRecipe.AddToRecipe(idInt);
                    }
                }
                resultRecipes.Add(tempRecipe);
            }
            return resultRecipes;
        }
    }
    public class RecipeEditorTXT : IRecipeEditor
    {
        public RecipeEditorTXT(Ingredients aviableIngredients)
        {
            AviableIngredients = aviableIngredients;
        }
        public Ingredients AviableIngredients { get; set; }
        public void SaveRecipeToFile(Recipe recipe, string fileName)
        {
            string fullFilePath = fileName + ".txt";
            string fileOutput = string.Join(",", recipe.RecipeIDs);

            if (File.Exists(fullFilePath))
            {
                string fileContent = File.ReadAllText(fullFilePath);
                File.WriteAllText(fullFilePath, fileOutput + "\n" + fileContent);
            }
            else
            {
                File.WriteAllText(fullFilePath, fileOutput);
            }
        }
        public List<Recipe> LoadRecipeFromFile(string fileName)
        {
            string fullFilePath = fileName + ".txt";
            if (!File.Exists(fullFilePath))
            {
                return null;
            }

            string fileContent = File.ReadAllText(fullFilePath);
            List<string> allRecipes = fileContent.Split('\n').ToList();
            List<Recipe> resultRecipes = new List<Recipe>();

            foreach (var recipeString in allRecipes)
            {
                Recipe tempRecipe = new Recipe(AviableIngredients);
                foreach (string id in recipeString.Split(',').ToList())
                {
                    if (int.TryParse(id, out int idInt))
                    {
                        tempRecipe.AddToRecipe(idInt);
                    }
                }
                resultRecipes.Add(tempRecipe);
            }
            return resultRecipes;
        }
    }
}
