using FoodChooser.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodChooser.ViewModels
{
    public class WhatToEatPageViewModel : ViewModelBase
    {
        private IEnumerable<ProductViewModel> products;
        private int numberOfProducts;
        private bool buttonVisability;

        public WhatToEatPageViewModel()
        {            
        }

        public bool ButtonVisability
        {
            get
            {
                return this.buttonVisability;
            }
            set
            {
                this.buttonVisability = value;
                this.RaisePropertyChanged(() => this.ButtonVisability);
            }
        }

        public int NumberOfProducts
        {
            get
            {
                return this.numberOfProducts;
            }

            set
            {
                this.numberOfProducts = value;
                this.RaisePropertyChanged(() => this.NumberOfProducts);


                this.ButtonVisability = this.numberOfProducts > 0 ? true : false;
                

                var products = new List<ProductViewModel>();
                for (int i = 0; i < numberOfProducts; i++)
                {
                    products.Add(new ProductViewModel() { Name = string.Empty });
                }
                

                this.Products = products;
                this.RaisePropertyChanged(() => this.Products);
            }
        }

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

        public async Task<IEnumerable<RecipeViewModel>>  GetRecipesAsync()
        {
            var inStockProducts = this.Products;
            
            var queryParse = (await new ParseQuery<RecipeModel>().Include("products").FindAsync());
            
            var result = queryParse.AsQueryable().Select(RecipeViewModel.FromModel)
                .Where(parseRecipe => parseRecipe.Products
                    .Any(pr => inStockProducts
                        .Any(ins => ins.Name == pr.Name)))
                        .ToList();

            return result;
        }




    }
}
