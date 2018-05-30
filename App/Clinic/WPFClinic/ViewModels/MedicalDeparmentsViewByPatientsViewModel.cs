using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WCFClinic;
using WPFClinic.Commands;
using WPFClinic.ServiceReference1;
using WPFClinic.View;

namespace WPFClinic.ViewModels
{
    class MedicalDeparmentsViewByPatientsViewModel : ViewModelBase
    {
        MedicalDeparmentsViewByPatients medicalDeparmentsViewByPatient;

        #region Constructor
        public MedicalDeparmentsViewByPatientsViewModel(MedicalDeparmentsViewByPatients medicalDeparmentsViewByPatientOpen)
        {
            medicalDeparmentsViewByPatient = medicalDeparmentsViewByPatientOpen;

            using (Service1Client wcf = new Service1Client())
            {
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
            }
        }
        #endregion

        #region Properties
        private List<vwMedicalDepartments> medicalDepartmentsList;
        public List<vwMedicalDepartments> MedicalDepartmentsList
        {
            get { return medicalDepartmentsList; }
            set
            {
                medicalDepartmentsList = value;
                OnPropertyChanged("MedicalDepartmentsList");
            }
        }
        #endregion

        #region Command DoctorAppointment

        private ICommand doctorAppointment;
        public ICommand DoctorAppointment
        {
            get
            {
                if (doctorAppointment == null)
                {
                    doctorAppointment = new RelayCommand(param => DoctorAppointmentExecute(), param => CanDoctorAppointmentExecute());
                }
                return doctorAppointment;
            }
        }

        private void DoctorAppointmentExecute()
        {
            try
            {
                DoctorAppointment view = new DoctorAppointment();
                medicalDeparmentsViewByPatient.Close();
                view.ShowDialog();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanDoctorAppointmentExecute()
        {
            return true;
        }
        #endregion
    }
}
