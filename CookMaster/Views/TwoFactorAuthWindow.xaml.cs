using CookMaster.Managers;
using CookMaster.ViewModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CookMaster.Views
{
    /// <summary>
    /// Interaction logic for TwoFactorAuthWindow.xaml
    /// </summary>
    public partial class TwoFactorAuthWindow : Window
    {
        public TwoFactorAuthWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            this.DataContext = new TwoFactorAuthViewModel(userManager);
            this.Loaded += TwoFactorAuthWindow_Loaded;

        }

        private void TwoFactorAuthWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is TwoFactorAuthViewModel tvm)
            {

                tvm.GenerateCode += msg => MessageBox.Show(msg, "You got mail!");
                tvm.CancelButton += Tvm_CancelButton;
                tvm.Confirm += Tvm_Confirm;
            }
        }

        private void Tvm_Confirm()
        {
            DialogResult = true;
            this.Close();
        }

        private void Tvm_CancelButton()
        {
            this.Close();
        }
    }
}
