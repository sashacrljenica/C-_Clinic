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
    /// Interaction logic for AddMedicalDepartments.xaml
    /// </summary>
    public partial class AddMedicalDepartments : Window
    {
        public AddMedicalDepartments()
        {
            InitializeComponent();
            this.DataContext = new AddMedicalDepartmentsViewModel(this);
        }

        public AddMedicalDepartments(vwMedicalDepartments medicalDepartmentsToEdit)
        {
            InitializeComponent();
            this.DataContext = new AddMedicalDepartmentsViewModel(this, medicalDepartmentsToEdit);
        }
    }
}
