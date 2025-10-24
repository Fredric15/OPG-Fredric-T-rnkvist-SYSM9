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
    /// Interaction logic for RecoverPw.xaml
    /// </summary>
    public partial class RecoverPwWindow : Window
    {
        public RecoverPwWindow()
        {
            InitializeComponent();
            var userManager = (UserManager)Application.Current.Resources["UserManager"];
            
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (e.NewValue is RecoverPwViewModel rvm)
            {

                rvm.RequestClose += () => this.Close();

                rvm.RequestMessage += msg => MessageBox.Show(msg, "Information");
            }

        }
    }
}
