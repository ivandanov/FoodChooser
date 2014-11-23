using FoodChooser.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace FoodChooser.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        private string name;
        private IEnumerable<ProductViewModel> products;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Description { get; set; }

        public IEnumerable<ProductViewModel> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
                RaisePropertyChanged(() => this.Products);
            }
        }

        public byte[] Image { get; set; }


        public static RecipeModel FromVMToModel(RecipeViewModel recipe)
        {
            return new RecipeModel()
            {
                Description = recipe.Description,
                Name = recipe.name,
                Products = recipe.Products.Select(product => new ProductModel() { Name = product.Name, Quantity = product.Quantity }).ToList(),
                Image = recipe.Image
            };
        }

        public static Expression<Func<RecipeModel, RecipeViewModel>> FromModel
        {
            get
            {
                return recipe => new RecipeViewModel()
                {
                    
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Image = recipe.Image,
                    Products = recipe.Get<IList<ProductModel>>("products").ToList()
                                        .Select(product => new ProductViewModel() { Name = product.Name, Quantity = product.Quantity }).ToList(),
                };
            }
        }
    }
}
