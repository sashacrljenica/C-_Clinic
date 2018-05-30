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
    class AddPatientsViewModel : ViewModelBase
    {
        AddPatients addPatients;

        #region Constructor
        public AddPatientsViewModel(AddPatients addPatientsOpen)
        {
            patients = new vwPatients();
            addPatients = addPatientsOpen;
        }

        public AddPatientsViewModel(AddPatients addPatientsOpen, vwPatients patientToEdit)
        {
            patients = patientToEdit;
            addPatients = addPatientsOpen;
        }
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

        private List<string> bloodTypeList = new List<string>()
        {
            "A+","A-","B+","B-","O+","O-","AB+","AB-"
        };
        public List<string> BloodTypeList
        {
            get
            {
                return bloodTypeList;
            }
            set
            {
                bloodTypeList = value;
                OnPropertyChanged("BloodTypeList");
            }
        }

        public bool rbMale
        {
            get
            {
                if (patients != null && patients.MaleOrFemale == "M")
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    patients.MaleOrFemale = "M";
                }
            }
        }

        public bool rbFemale
        {
            get
            {
                if (patients != null && patients.MaleOrFemale == "F")
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    patients.MaleOrFemale = "F";
                }
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

                    wcf.AddPatient(Patients);
                    MessageBox.Show("You are successfully added Patients!");
                    addPatients.Close();
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
            if (patients.NameAndSurname == null || patients.Address == null || patients.BloodType == null ||
                patients.DateOfBirth == null || patients.MaleOrFemale == null || patients.NumberOfIDCard == null ||
                patients.PhoneNumberContactPerson == null || patients.PhoneNumberHome == null || patients.PhoneNumberMobile == null)
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
                addPatients.Close();
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
