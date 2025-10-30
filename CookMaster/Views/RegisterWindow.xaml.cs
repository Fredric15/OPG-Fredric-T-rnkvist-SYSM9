using CookMaster.Managers;
using CookMaster.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        

        public RegisterWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            this.DataContext = new RegisterViewModel(userManager);
            this.Loaded += RegisterWindow_Loaded;
            

        }

       private void RegisterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is RegisterViewModel vm)
            {
                vm.RequestClose += Vm_RequestClose;
                vm.ConfirmNewUser += Vm_ConfirmNewUser;
            }
            
        }

        private void Vm_ConfirmNewUser(string obj)
        {
            Close();
            MessageBox.Show("Användare har lagts till.");
        }

        private void Vm_RequestClose()
        {
            Close();
        }

    }
}
