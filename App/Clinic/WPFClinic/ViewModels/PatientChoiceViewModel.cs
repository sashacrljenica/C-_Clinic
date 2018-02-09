using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFClinic.Commands;
using WPFClinic.View;

namespace WPFClinic.ViewModels
{
    class PatientChoiceViewModel : ViewModelBase
    {
        PatientChoice patientChoice;

        #region Constructor
        public PatientChoiceViewModel(PatientChoice patientChoiceOpen)
        {
            patientChoice = patientChoiceOpen;
        }
        #endregion

        #region Command MedicalDepartment

        private ICommand medicalDepartment;
        public ICommand MedicalDepartment
        {
            get
            {
                if (medicalDepartment == null)
                {
                    medicalDepartment = new RelayCommand(param => MedicalDepartmentExecute(), param => CanMedicalDepartmentExecute());
                }
                return medicalDepartment;
            }
        }

        private void MedicalDepartmentExecute()
        {
            try
            {
                MedicalDeparmentsViewByPatients view = new MedicalDeparmentsViewByPatients();
                patientChoice.Close();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanMedicalDepartmentExecute()
        {
            return true;
        }

        #endregion

        #region Command doctorAppointment

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
                DoctorAppointment doctorAppointmentWindow = new DoctorAppointment();
                patientChoice.Close();
                doctorAppointmentWindow.ShowDialog();
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
