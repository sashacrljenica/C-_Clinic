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
using WCFClinic;
using WPFClinic.ViewModels;

namespace WPFClinic.View
{
    /// <summary>
    /// Interaction logic for MedicalStaffAddEdit.xaml
    /// </summary>
    public partial class MedicalStaffAddEdit : Window
    {
        public MedicalStaffAddEdit()
        {
            InitializeComponent();
            this.DataContext = new MedicalStaffAddEditViewModel(this);
        }
    }
}
