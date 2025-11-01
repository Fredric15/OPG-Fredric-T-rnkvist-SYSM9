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
    /// Interaction logic for RecipeListWindow.xaml
    /// </summary>
    public partial class RecipeListWindow : Window
    {
        public RecipeListWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            var recipeManager = (RecipeManager)Application.Current.Resources["RecipeManager"];
            this.DataContext = new RecipeListViewModel(userManager,recipeManager);
            this.Loaded += RecipeListWindow_Loaded;
            

        }


        private void RecipeListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is RecipeListViewModel rvm)
            {
                rvm.RequestAddRecipe += Rvm_RequestAddRecipe;
                rvm.InfoMessage += msg => MessageBox.Show(msg, "Info");
                rvm.RequestUserDetails += Rvm_RequestUserDetails;
                rvm.LogOut += Rvm_LogOut;
            }
        }

        private void Rvm_RequestAddRecipe()
        {
            AddRecipeWindow addrecipe = new AddRecipeWindow();
            this.Close();
            addrecipe.Show();
        }

        private void Rvm_LogOut()
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void Rvm_RequestUserDetails()
        {
            //var userManager = (UserManager)Application.Current.Resources["UserManager"];
            //UserDetailsWindow userDetails = new UserDetailsWindow();
            //userDetails.DataContext = new UserDetailsViewModel(userManager);

            UserDetailsWindow userDetails = new UserDetailsWindow();
            this.Close();
            userDetails.Show();
        }
    }
}
