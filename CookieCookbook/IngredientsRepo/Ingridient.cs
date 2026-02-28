using System.Data;

namespace CookieCookbook.IngredientsRepo
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Ingredient(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }
    }
}
