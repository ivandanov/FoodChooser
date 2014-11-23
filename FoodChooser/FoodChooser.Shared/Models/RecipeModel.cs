using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodChooser.Models
{
    [ParseClassName("Recipe")]
    public class RecipeModel : ParseObject
    {
        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("description")]
        public string Description
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("products")]
        public IEnumerable<ProductModel> Products
        {
            get { return GetProperty<IEnumerable<ProductModel>>(); }
            set { SetProperty<IEnumerable<ProductModel>>(value); }
        }

        //[ParseFieldName("image")]
        public byte[] Image { get; set; }
    }
}
