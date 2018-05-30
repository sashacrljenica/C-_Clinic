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
    class DoctorAppointmentViewModel : ViewModelBase
    {
        DoctorAppointment doctorAppointment;

        #region Constructor
        public DoctorAppointmentViewModel(DoctorAppointment doctorAppointmentOpen)
        {
            loginCurrent = new vwLoginCurrent();
            appointment = new vwDoctorAppointment();
            doctorAppointment = doctorAppointmentOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PatientsList = wcf.GetAllPatients().ToList();
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
            }
        }
        #endregion

        #region Properties
        private vwLoginCurrent loginCurrent;
        public vwLoginCurrent LoginCurrent
        {
            get
            {
                return loginCurrent;
            }

            set
            {
                loginCurrent = value;
                OnPropertyChanged("LoginCurrent");
            }
        }


        private vwDoctorAppointment appointment;
        public vwDoctorAppointment Appointment
        {
            get
            {
                return appointment;
            }

            set
            {
                appointment = value;
                OnPropertyChanged("Appointment");
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

        #region ICommand Save

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
                    if (Appointment.DoctorAppointmentID == 0)
                    {

                        //int currentPatient=wcf.

                        int currentPatient = wcf.maxCurrentPatientID();

                        //List<vwPatients> currentPatientList = wcf.GetPatientsDetail(currentPatient).ToList();

                        //foreach (vwPatients item in currentPatientList)
                        //{
                        //    int patientID = item.PatientID.Value;
                        //}

                        Appointment.MedicalDepartmentID = MedicalDepartments.MedicalDepartmentID;
                        Appointment.PatientID = currentPatient;
                    }
                    wcf.AddDoctorAppointment(Appointment);

                    MessageBox.Show("You have successfully scheduled a medical examination!");
                    doctorAppointment.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. You must choose again!");
                MessageBox.Show(ex.ToString());
                doctorAppointment.dpDate.Text = "";
                //doctorAppointment.cbName.Text = "";
                doctorAppointment.cbMedicalDepartment.Text = "";
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(doctorAppointment.dpDate.Text) ||
               //String.IsNullOrEmpty(doctorAppointment.cbName.Text) ||
               String.IsNullOrEmpty(doctorAppointment.cbMedicalDepartment.Text)
               )
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        #endregion

    }
}
