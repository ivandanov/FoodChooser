using System;
using System.Collections.Generic;
using System.Text;

namespace FoodChooser.Models
{
    public class RecipeModelSQLite
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Products { get; set; }

        public byte[] Image { get; set; }
    }
}
