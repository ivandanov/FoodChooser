using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodChooser.Models
{
    [ParseClassName("Product")]
    public class ProductModel : ParseObject
    {
        [ParseFieldName("name")]
        public string Name
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("quantity")]
        public int Quantity
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }
    }
}
