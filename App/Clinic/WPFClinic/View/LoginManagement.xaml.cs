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
using WPFClinic.ViewModels;

namespace WPFClinic.View
{
    /// <summary>
    /// Interaction logic for LoginManagement.xaml
    /// </summary>
    public partial class LoginManagement : Window
    {
        public LoginManagement()
        {
            InitializeComponent();
            this.DataContext = new LoginManagementViewModel(this);
        }

        private void txtPass_TextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
