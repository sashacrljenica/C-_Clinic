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
    class AddDoctorAppointmentViewModel : ViewModelBase
    {
        AddDoctorAppointment addDoctorAppointment;

        #region Constructor
        public AddDoctorAppointmentViewModel(AddDoctorAppointment addDoctorAppointmentOpen)
        {
            doctorAppointment = new vwDoctorAppointment();
            addDoctorAppointment = addDoctorAppointmentOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PatientsList = wcf.GetAllPatients().ToList();
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
            }
        }

        public AddDoctorAppointmentViewModel(AddDoctorAppointment addDoctorAppointmentOpen, vwDoctorAppointment doctorAppointmentToEdit)
        {
            doctorAppointment = doctorAppointmentToEdit;
            addDoctorAppointment = addDoctorAppointmentOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PatientsList = wcf.GetAllPatients().ToList();
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
            }
        }
        #endregion

        #region Properties
        private vwDoctorAppointment doctorAppointment;
        public vwDoctorAppointment DoctorAppointment
        {
            get
            {
                return doctorAppointment;
            }

            set
            {
                doctorAppointment = value;
                OnPropertyChanged("DoctorAppointment");
            }
        }

        private vwPatients patients;
        public vwPatients Patients
        {
            get
            {
                return patients;
            }

            set
            {
                patients = value;
                OnPropertyChanged("Patients");
            }
        }

        private List<vwPatients> patientsList;
        public List<vwPatients> PatientsList
        {
            get
            {
                return patientsList;
            }

            set
            {
                patientsList = value;
                OnPropertyChanged("PatientsList");
            }
        }

        private vwMedicalDepartments medicalDepartments;
        public vwMedicalDepartments MedicalDepartments
        {
            get
            {
                return medicalDepartments;
            }

            set
            {
                medicalDepartments = value;
                OnPropertyChanged("MedicalDepartments");
            }
        }

        private List<vwMedicalDepartments> medicalDepartmentsList;
        public List<vwMedicalDepartments> MedicalDepartmentsList
        {
            get
            {
                return medicalDepartmentsList;
            }

            set
            {
                medicalDepartmentsList = value;
                OnPropertyChanged("MedicalDepartmentsList");
            }
        }
        #endregion

        #region Command Save
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                using (Service1Client wcf = new Service1Client())
                {
                    doctorAppointment.MedicalDepartmentID = medicalDepartments.MedicalDepartmentID;
                    doctorAppointment.PatientID = patients.PatientID;

                    wcf.AddDoctorAppointment(DoctorAppointment);

                    MessageBox.Show("You are successfully added Doctor Appointment!");
                    addDoctorAppointment.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!");
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSaveExecute()
        {
            if (doctorAppointment.NameAndSurname == null || doctorAppointment.DateOfScheduling == null ||
                doctorAppointment.NameOfDepartment == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command Close
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            try
            {
                addDoctorAppointment.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }
}
