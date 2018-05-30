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
    class ManagementAddEditViewModel : ViewModelBase
    {
        ManagementAddEdit managementAddEdit;

        #region Constructor
        public ManagementAddEditViewModel(ManagementAddEdit managementAddEditOpen)
        {
            managementAddEdit = managementAddEditOpen;
        }
        #endregion

        #region Command MedicalStaff

        private ICommand medicalStaff;
        public ICommand MedicalStaff
        {
            get
            {
                if (medicalStaff == null)
                {
                    medicalStaff = new RelayCommand(param => MedicalStaffExecute(), param => CanMedicalStaffExecute());
                }
                return medicalStaff;
            }
        }

        private void MedicalStaffExecute()
        {
            try
            {
                MedicalStaffAddEdit view = new MedicalStaffAddEdit();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanMedicalStaffExecute()
        {
            return true;
        }
        #endregion

        #region Command Patients
        private ICommand patients;
        public ICommand Patients
        {
            get
            {
                if (patients == null)
                {
                    patients = new RelayCommand(param => PatientsExecute(), param => CanPatientsExecute());
                }
                return patients;
            }
        }

        private void PatientsExecute()
        {
            try
            {
                PatientsAddEdit view = new PatientsAddEdit();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanPatientsExecute()
        {
            return true;
        }
        #endregion

        #region Command MedicalDepartments
        private ICommand medicalDepartments;
        public ICommand MedicalDepartments
        {
            get
            {
                if (medicalDepartments == null)
                {
                    medicalDepartments = new RelayCommand(param => MedicalDepartmentsExecute(), param => CanMedicalDepartmentsExecute());
                }
                return medicalDepartments;
            }
        }

        private void MedicalDepartmentsExecute()
        {
            try
            {
                MedicalDepartmentsAddEdit view = new MedicalDepartmentsAddEdit();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanMedicalDepartmentsExecute()
        {
            return true;
        }
        #endregion

        #region Command Pharmacy
        private ICommand pharmacy;
        public ICommand Pharmacy
        {
            get
            {
                if (pharmacy == null)
                {
                    pharmacy = new RelayCommand(param => PharmacyExecute(), param => CanPharmacyExecute());
                }
                return pharmacy;
            }
        }

        private void PharmacyExecute()
        {
            try
            {
                PharmacyAddEdit view = new PharmacyAddEdit();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanPharmacyExecute()
        {
            return true;
        }
        #endregion

        #region Command PharmaceuticalManufacturing
        private ICommand pharmaceuticalManufacturing;
        public ICommand PharmaceuticalManufacturing
        {
            get
            {
                if (pharmaceuticalManufacturing == null)
                {
                    pharmaceuticalManufacturing = new RelayCommand(param => PharmaceuticalManufacturingExecute(), param => CanPharmaceuticalManufacturingExecute());
                }
                return pharmaceuticalManufacturing;
            }
        }

        private void PharmaceuticalManufacturingExecute()
        {
            try
            {
                PharmaceuticalManufacturingAddEdit view = new PharmaceuticalManufacturingAddEdit();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanPharmaceuticalManufacturingExecute()
        {
            return true;
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
                DoctorAppointmentAddEdit view = new DoctorAppointmentAddEdit();
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

        #region Command TakeMedication
        private ICommand takeMedication;
        public ICommand TakeMedication
        {
            get
            {
                if (takeMedication == null)
                {
                    takeMedication = new RelayCommand(param => TakeMedicationExecute(), param => CanTakeMedicationExecute());
                }
                return takeMedication;
            }
        }

        private void TakeMedicationExecute()
        {
            try
            {
                TakeMedicationAddEdit view = new TakeMedicationAddEdit();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanTakeMedicationExecute()
        {
            return true;
        }
        #endregion
    }
}
