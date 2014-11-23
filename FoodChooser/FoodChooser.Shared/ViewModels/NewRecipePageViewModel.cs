using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
namespace FoodChooser.ViewModels
{
    public class NewRecipePageViewModel : ViewModelBase
    {
        private RecipeViewModel recipeViewModel;
        private int numberOfProducts;
        private bool progressbarVisability;

        public bool ProgressbarVisability
        {
            get
            {
                return this.progressbarVisability;
            }
            set
            {
                this.progressbarVisability = value;
                this.RaisePropertyChanged(() => this.ProgressbarVisability);
            }
        }

        public RecipeViewModel Recipe
        {
            get
            {
                return this.recipeViewModel;
            }
            set
            {
                this.recipeViewModel = value;
                this.RaisePropertyChanged(() => this.Recipe);
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

                var products = new List<ProductViewModel>();
                for (int i = 0; i < numberOfProducts; i++)
                {
                    products.Add(new ProductViewModel() { Name = string.Empty });
                }

                this.Recipe.Products = products;
                this.RaisePropertyChanged(() => this.Recipe.Products);
            }
        }
    }
}
