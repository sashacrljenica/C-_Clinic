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
    /// Interaction logic for TakeMedication.xaml
    /// </summary>
    public partial class TakeMedication : Window
    {
        public TakeMedication()
        {
            InitializeComponent();
            this.DataContext = new TakeMedicationViewModel(this);
        }
    }
}
