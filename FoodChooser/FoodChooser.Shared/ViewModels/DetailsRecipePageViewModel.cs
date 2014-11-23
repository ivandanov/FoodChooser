using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodChooser.ViewModels
{
    public class DetailsRecipePageViewModel : ViewModelBase
    {
        private RecipeViewModel recipeViewModel;

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
    }
}
