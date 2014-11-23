
namespace FoodChooser.Pages
{
    using System.Linq;
    
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using FoodChooser.Common;
    using FoodChooser.DeviceAPIs;
    using FoodChooser.ViewModels;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class WhatToEatPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public WhatToEatPage()
            : this(new WhatToEatPageViewModel())
        {
        }

        public WhatToEatPage(WhatToEatPageViewModel viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.ViewModel = viewModel;
        }

        public WhatToEatPageViewModel ViewModel
        {
            get
            {
                return this.DataContext as WhatToEatPageViewModel;
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
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void WhatToEatButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
            {
                return;
            }
            if(this.ViewModel.NumberOfProducts == 0)
            {
                return;
            }

            this.ContentRoot.Visibility = Visibility.Collapsed;

            var recipesFromParse = await this.ViewModel.GetRecipesAsync();
            

            if (recipesFromParse.Count() != 0)
            {
                this.Frame.Navigate(typeof(RecipesPage), recipesFromParse);
            }
            else
            {
                Notification.MakeNotification("No Recipes");
                this.ContentRoot.Visibility = Visibility.Visible;
            }
        }

    }
}
