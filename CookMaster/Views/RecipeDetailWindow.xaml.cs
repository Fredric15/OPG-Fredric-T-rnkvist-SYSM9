using CookMaster.Managers;
using CookMaster.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CookMaster.Views
{
    /// <summary>
    /// Interaction logic for RecipeDetailWindow.xaml
    /// </summary>
    public partial class RecipeDetailWindow : Window
    {
        public RecipeDetailWindow()
        {
            InitializeComponent();
            var recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            this.DataContext = new RecipeDetailViewModel(recipeManager);

            this.Loaded += RecipeDetailWindow_Loaded;
        }

        private void RecipeDetailWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is RecipeDetailViewModel rvm)
            {
                rvm.RecipeSaved += msg => MessageBox.Show(msg,"Information");
                rvm.RequestClose += Rvm_RequestClose;
                rvm.RequestCopy += Rvm_RequestCopy;
            
            }
        }

        private void Rvm_RequestCopy()
        {
            //När användare väljer att kopiera ett recept så injekterar jag det valda receptet
            //Alla egenskaperna kan jag då sätta i konstruktorn så allt är ifyllt när fönstret AddRecipe öppnas
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            AddRecipeWindow window = new AddRecipeWindow();
            window.DataContext = new AddRecipeViewModel(userManager, recipeManager, recipeManager.SelectedRecipe);
            window.Show();
            this.Close();
        }

        private void Rvm_RequestClose()
        {
            RecipeListWindow window = new RecipeListWindow();
            window.Show();
            this.Close();
        }
    }
}
