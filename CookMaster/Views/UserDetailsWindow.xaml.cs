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
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            this.DataContext = new UserDetailsViewModel(userManager);

            this.Loaded += UserDetailsWindow_Loaded;
        }

        private void UserDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UserDetailsViewModel vm)
            {
                vm.CloseWindow += Vm_CloseWindow;
                
            }
        }

        private void Vm_CloseWindow()
        {
            RecipeListWindow window = new RecipeListWindow();
            Close();
            window.Show();
        }
    }
}
