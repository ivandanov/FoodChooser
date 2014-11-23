using Newtonsoft.Json;
using SQLite;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json.Linq;
using FoodChooser.Models;
using FoodChooser.ViewModels;
using FoodChooser.DeviceAPIs;

namespace FoodChooser.DataStorageHelpers
{
    public class SQLiteHelper
    {
        private const string dbName = "Recipes7.db";

        public async Task Init()
        {
            bool dbExists = await CheckDbAsync(dbName);
            if (!dbExists)
            {
                await CreateDatabaseAsync();
            }

        }

        public async Task<bool> AddRecipe(RecipeModel recipe)
        {
            var dict = new Dictionary<string, int>();
            try
            {
                foreach (var item in recipe.Products)
                {
                    dict.Add(item.Name, item.Quantity);
                }
            }
            catch (Exception)
            {
                Notification.MakeNotification("You typed two or more products with same name");
                return false;
            }
            string products = JsonConvert.SerializeObject(dict);

            var recipeToInsert = new RecipeModelSQLite()
            {
                Description = recipe.Description,
                Name = recipe.Name,
                Image = recipe.Image,
                Products = products
            };

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.InsertAsync(recipeToInsert);
            
            return true;
        }

        public async Task<IEnumerable<RecipeViewModel>> getAllRecipes()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            var query = conn.Table<RecipeModelSQLite>();
            var recipes = await query.ToListAsync();
            var result = new List<RecipeViewModel>();

            foreach (var item in recipes)
            {
                var products = new List<ProductViewModel>();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, int>>(item.Products);
                foreach (var product in dict)
                {
                    products.Add(new ProductViewModel() { Name = product.Key, Quantity = product.Value });
                }
                result.Add(new RecipeViewModel()
                    {
                        Name = item.Name,
                        Description = item.Description,
                        Products = products,
                        Image = item.Image
                    });
            }

            return result;
        }

        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }

        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.CreateTableAsync<RecipeModelSQLite>();
        }
    }
}
