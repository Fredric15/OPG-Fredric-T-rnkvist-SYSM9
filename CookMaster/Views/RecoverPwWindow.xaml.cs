using CookMaster.Managers;
using CookMaster.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
    /// Interaction logic for RecoverPw.xaml
    /// </summary>
    public partial class RecoverPwWindow : Window
    {
        public RecoverPwWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            this.DataContext = new RecoverPwViewModel(userManager);
            //this.DataContextChanged += OnDataContextChanged;
            this.Loaded += RecoverPwWindow_Loaded;
        }

        private void RecoverPwWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is RecoverPwViewModel vm)
            {
                vm.RequestClose += () => this.Close();

                vm.RequestMessage += msg => MessageBox.Show(msg, "Information");


            }
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue is RecoverPwViewModel rvm)
            {

                rvm.RequestClose += () => this.Close();

                rvm.RequestMessage += msg => MessageBox.Show(msg, "Information");
            }

        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RecoverPwViewModel vm)
            {
                vm.NewPwd = PwdBox.Password;
                vm.ConfirmPwd = ConfirmPwd.Password;
            }

        }
    }
}
