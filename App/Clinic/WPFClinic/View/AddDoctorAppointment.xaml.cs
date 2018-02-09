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
    /// Interaction logic for AddDoctorAppointment.xaml
    /// </summary>
    public partial class AddDoctorAppointment : Window
    {
        public AddDoctorAppointment()
        {
            InitializeComponent();
            this.DataContext = new AddDoctorAppointmentViewModel(this);
        }

        public AddDoctorAppointment(vwDoctorAppointment doctorAppointmentToEdit)
        {
            InitializeComponent();
            this.DataContext = new AddDoctorAppointmentViewModel(this, doctorAppointmentToEdit);
        }
    }
}
