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
    class PatientsAddEditViewModel : ViewModelBase
    {
        PatientsAddEdit patientsAddEdit;

        #region Constructor
        public PatientsAddEditViewModel(PatientsAddEdit patientsAddEditOpen)
        {
            patientsAddEdit = patientsAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PatientsList = wcf.GetAllPatients().ToList();
            }
        }

        //public PatientsAddEditViewModel(PatientsAddEdit patientsAddEditOpen, vwPatients patientsToEdit)
        //{
        //    patientsAddEdit = patientsAddEditOpen;
        //    using (Service1Client wcf = new Service1Client())
        //    {
        //        PatientsList = wcf.GetAllPatients().ToList();
        //    }

        //}
        #endregion

        #region Properties
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

        //private bool isUpdatePatient;
        //public bool IsUpdatePatient
        //{
        //    get
        //    {
        //        return isUpdatePatient;
        //    }
        //    set
        //    {
        //        isUpdatePatient = value;
        //    }
        //}
        #endregion

        #region Command AddNewPatient
        private ICommand addNewPatient;
        public ICommand AddNewPatient
        {
            get
            {
                if (addNewPatient == null)
                {
                    addNewPatient = new RelayCommand(param => AddNewPatientExecute(), param => CanAddNewPatientExecute());
                }
                return addNewPatient;
            }
        }

        private void AddNewPatientExecute()
        {
            try
            {
                AddPatients addPatients = new AddPatients();
                addPatients.ShowDialog();
                //if ((addPatients.DataContext as AddPatientsViewModel).IsUpdatePatients == true)
                //{
                    using (Service1Client wcf = new Service1Client())
                    {
                        PatientsList = wcf.GetAllPatients().ToList();
                    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewPatientExecute()
        {
            return true;
        }
        #endregion

        #region Command EditPatient
        private ICommand editPatient;
        public ICommand EditPatient
        {
            get
            {
                if (editPatient == null)
                {
                    editPatient = new RelayCommand(param => EditPatientExecute(), param => CanEditPatientExecute());
                }

                return editPatient;
            }
        }

        private void EditPatientExecute()
        {
            try
            {
                if (editPatient != null)
                {
                    AddPatients addPatients = new AddPatients(Patients);
                    addPatients.ShowDialog();
                    //if ((addPatients.DataContext as AddPatientsViewModel).IsUpdatePatients == true)
                    //{
                        using (Service1Client wcf = new Service1Client())
                        {
                            PatientsList = wcf.GetAllPatients().ToList();
                        }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditPatientExecute()
        {
            if (Patients == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeletePatient
        private ICommand deletePatient;
        public ICommand DeletePatient
        {
            get
            {
                if (deletePatient == null)
                {
                    deletePatient = new RelayCommand(param => DeletePatientExecute(), param => CanDeletePatientExecute());
                }

                return deletePatient;
            }
        }

        private void DeletePatientExecute()
        {
            try
            {
                if (Patients != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int patientID = patients.PatientID;
                        bool isPatient = wcf.isPatientID(patientID);
                        if (isPatient == true)
                        {
                            wcf.DeletePatient(patientID);
                            PatientsList = wcf.GetAllPatients().ToList();
                            MessageBox.Show("You are successfully deleted selected patient!");
                        }
                        else
                        {
                            MessageBox.Show("Error!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeletePatientExecute()
        {
            if (Patients == null)
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
                patientsAddEdit.Close();
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
