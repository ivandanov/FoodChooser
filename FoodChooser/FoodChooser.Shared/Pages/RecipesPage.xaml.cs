using FoodChooser.Common;
using FoodChooser.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FoodChooser.Pages
{
    public sealed partial class RecipesPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public RecipesPage()
            : this(new RecipesPageViewModel())
        {
        }

        public RecipesPage(RecipesPageViewModel viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.ViewModel = viewModel;
        }

        public RecipesPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as RecipesPageViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }


        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
                
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

       
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var recipes = e.Parameter as IEnumerable<RecipeViewModel>;
            this.ViewModel.Recipes = recipes;

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void OnSelectListItem(object sender, SelectionChangedEventArgs e)
        {
            var listview = (sender as ListView);
            var selectedObject = listview.SelectedItem;

            this.Frame.Navigate(typeof(DetailesRecipePage), selectedObject);
        }
    }
}
