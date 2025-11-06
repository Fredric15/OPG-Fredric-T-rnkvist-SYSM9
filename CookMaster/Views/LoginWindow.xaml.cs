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

            RecoverPwWindow recover = new RecoverPwWindow();
            recover.ShowDialog();
        }

        private void Vm_OpenRegister(object? sender, EventArgs e)
        {

            RegisterWindow window = new RegisterWindow();
            window.ShowDialog();
        }

        private void Vm_OnLoginSuccess(object? sender, string msg)
        {
            //En messagebox poppar upp med den sexsiffriga koden som genereras när en användare försöker logga in
            MessageBox.Show($"Verifikationskod: {msg}","You got mail!");
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            
            TwoFactorAuthWindow window = new TwoFactorAuthWindow();
            
            //Använder en Show.Dialog som returnerar true om koden matchar användarens input
            //Om true så öppnas RecipeListWindow
            //Annars tillbaka till LoginWindow
            var success = window.ShowDialog();
            if (success == true)
            {
                MessageBox.Show("Verification success!");
                
                RecipeListWindow recipeList = new RecipeListWindow();
                recipeList.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Verification failed.");
            }
                
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = PwdBox.Password;
            }
        }
    }
}