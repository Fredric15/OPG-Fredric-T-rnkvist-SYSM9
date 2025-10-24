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
        public bool success {  get; set; }
        public RegisterWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            
            this.Loaded += RegisterWindow_Loaded;
            

            
            //this.DataContextChanged += OnDataContextChanged;
            
            /*if (DataContext is RegisterViewModel rw)
            {

                rw.RequestClose += () => this.Close();
            }*/
        }

       

        private void RegisterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is RegisterViewModel vm)
            {
                vm.CloseRegister += Vm_CloseRegister;
                vm.ConfirmMessage += Vm_ConfirmMessage;
            }
            
        }

        private void Vm_CloseRegister(object? sender, EventArgs e)
        {
            Close();
        }

        private void Vm_ConfirmMessage(object? sender, EventArgs e)
        {
            Close();
            MessageBox.Show("Användare har lagts till.");
            
        }

        /*private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue is RegisterViewModel vm)
            {

                vm.RequestClose += () => this.Close();

                vm.RequestMessage += msg => MessageBox.Show(msg, "Information");
            }

        }*/
    }
}
