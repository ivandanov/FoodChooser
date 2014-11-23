using FoodChooser.Common;
using FoodChooser.Models;
using FoodChooser.ViewModels;
using Newtonsoft.Json;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Capture;
using FoodChooser.DataStorageHelpers;
using FoodChooser.DeviceAPIs;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FoodChooser.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewRecipePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public NewRecipePage()
            : this(new NewRecipePageViewModel())
        {
        }

        public NewRecipePage(NewRecipePageViewModel viewModel)
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.ViewModel = viewModel;


        }

        public NewRecipePageViewModel ViewModel
        {
            get
            {
                return this.DataContext as NewRecipePageViewModel;
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel.Recipe = new RecipeViewModel();

#if WINDOWS_PHONE_APP
            var mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync();
            CameraElement.Source = mediaCapture;
            await mediaCapture.StartPreviewAsync();
#endif

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void OnSaveButonClick(object sender, RoutedEventArgs e)
        {
            if(this.ViewModel.NumberOfProducts == 0)
            {
                Notification.MakeNotification("You should add at least one product");
                return;
            }

            this.ToggleProgressbar();

            //sqlite
            var recipe = RecipeViewModel.FromVMToModel(this.ViewModel.Recipe);
            var sqliteHandler = new SQLiteHelper();
            await sqliteHandler.Init();
            var success = await sqliteHandler.AddRecipe(recipe);

            if(success == false)
            {
                this.ToggleProgressbar();
                return;
            }
            //parse
            var recipeParse = RecipeViewModel.FromVMToModel(this.ViewModel.Recipe);
            await recipeParse.SaveAsync();

            Notification.MakeNotification(string.Format("New recipe add via {0}", Connection.GetConnection()));
            //ToggleProgressbar();           
            Frame.Navigate(typeof(MainPage));
        }

        private void ToggleProgressbar()
        {
            this.ViewModel.ProgressbarVisability = !this.ViewModel.ProgressbarVisability;
            this.ContentRoot.Visibility = this.ContentRoot.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void OnSetPhotoButtonClick(object sender, RoutedEventArgs e)
        {
           var capture = new CameraCapture();
           var photo = await capture.CapturePhoto(this.CameraElement);

           this.ViewModel.Recipe.Image = photo;
        }
    }
}
