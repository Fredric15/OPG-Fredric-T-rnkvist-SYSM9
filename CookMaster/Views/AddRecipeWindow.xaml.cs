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
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            var recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            this.DataContext = new AddRecipeViewModel(userManager,recipeManager);
            this.Loaded += AddRecipeWindow_Loaded;
        }

        private void AddRecipeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is AddRecipeViewModel viewModel)
            {
                viewModel.ConfirmAddRecipe += msg => MessageBox.Show(msg, "Information");
                viewModel.RequestClose += ViewModel_RequestClose;
            
            }
        }

        private void ViewModel_RequestClose()
        {
            RecipeListWindow recipeListWindow = new RecipeListWindow();
            recipeListWindow.Show();
            this.Close();
        }
    }
}
