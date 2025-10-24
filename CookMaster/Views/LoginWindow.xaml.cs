using CookMaster.Managers;
using CookMaster.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CookMaster.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            LoginViewModel vm = new LoginViewModel(userManager);
            DataContext = vm;

            vm.OnLoginSuccess += Vm_OnLoginSuccess;
            vm.OpenRegister += Vm_OpenRegister;
            vm.RecoverPwd += Vm_RecoverPwd;
            
        }

        private void Vm_RecoverPwd(object? sender, EventArgs e)
        {
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            RecoverPwWindow recover = new RecoverPwWindow();
            recover.DataContext = new RecoverPwViewModel(userManager);
            recover.ShowDialog();
        }

        private void Vm_OpenRegister(object? sender, EventArgs e)
        {
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            RegisterWindow window = new RegisterWindow();
            window.DataContext = new RegisterViewModel(userManager);
            window.ShowDialog();
        }

        private void Vm_OnLoginSuccess(object? sender, EventArgs e)
        {
            MessageBox.Show("Hejsan");
        }
    }
}