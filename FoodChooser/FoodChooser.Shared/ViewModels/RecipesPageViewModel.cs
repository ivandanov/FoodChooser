using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodChooser.ViewModels
{
    public class RecipesPageViewModel : ViewModelBase
    {
        private ObservableCollection<RecipeViewModel> recipes;

        public IEnumerable<RecipeViewModel> Recipes
        {
            get
            {
                if (this.recipes == null)
                {
                    this.recipes = new ObservableCollection<RecipeViewModel>();
                }
                return this.recipes;
            }
            set
            {
                if (this.recipes == null)
                {
                    this.recipes = new ObservableCollection<RecipeViewModel>();
                }
                this.recipes.Clear();
                foreach (var item in value)
                {
                    this.recipes.Add(item);
                }
            }
        }


    }
}
