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
    class TakeMedicationViewModel : ViewModelBase
    {
        TakeMedication takeMedicationWindow;

        #region Constructor
        public TakeMedicationViewModel(TakeMedication takeMedicationWindowOpen)
        {
            loginCurrent = new vwLoginCurrent();
            medicalStaff = new vwMedicalStaff();
            patients = new vwPatients();
            pharmacy = new vwPharmacy();
            takeMedication = new vwTakeMedication();
            takeMedicationWindow = takeMedicationWindowOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmacyList = wcf.GetAllPharmacy().ToList();
                MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                PatientsList = wcf.GetAllPatients().ToList();
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

        private vwTakeMedication takeMedication;
        public vwTakeMedication TakeMedication
        {
            get
            {
                return takeMedication;
            }

            set
            {
                takeMedication = value;
                OnPropertyChanged("TakeMedication");
            }
        }

        private vwPharmacy pharmacy;
        public vwPharmacy Pharmacy
        {
            get
            {
                return pharmacy;
            }

            set
            {
                pharmacy = value;
                OnPropertyChanged("Pharmacy");
            }
        }

        private List<vwPharmacy> pharmacyList;
        public List<vwPharmacy> PharmacyList
        {
            get { return pharmacyList; }
            set
            {
                pharmacyList = value;
                OnPropertyChanged("PharmacyList");
            }
        }
        private vwMedicalStaff medicalStaff;
        public vwMedicalStaff MedicalStaff
        {
            get
            {
                return medicalStaff;
            }

            set
            {
                medicalStaff = value;
                OnPropertyChanged("MedicalStaff");
            }
        }

        private List<vwMedicalStaff> medicalStaffList;
        public List<vwMedicalStaff> MedicalStaffList
        {
            get
            {
                return medicalStaffList;
            }

            set
            {
                medicalStaffList = value;
                OnPropertyChanged("MedicalStaffList");
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

                    int quantityPharmacy = Convert.ToInt32(Pharmacy.QuantityOnCondition);
                    int quantityTakeMedication = Convert.ToInt32(takeMedicationWindow.txtQuantity.Text);
                    int newQuantity = quantityPharmacy - quantityTakeMedication;
                    //string newQuantityString = newQuantity.ToString();

                    Pharmacy.QuantityOnCondition = newQuantity;

                    wcf.AddPharmacy(Pharmacy);

                    //Pharmacy.QuantityOnCondition = Pharmacy.QuantityOnCondition - takeMedicationWindow.txtQuantity.Text;

                    if (takeMedication.TakeMedicationID == 0)
                    {
                        takeMedication.QuantityOfTheDrug = quantityTakeMedication;
                        takeMedication.PharmacyID = pharmacy.PharmacyID;
                        //takeMedication.MedicalStaffID = MedicalStaff.MedicalStaffID;
                        takeMedication.PatientID = patients.PatientID;

                        int currentMedicalStaff = wcf.maxCurrentMedicalStaffID();
                        takeMedication.MedicalStaffID = currentMedicalStaff;
                    }

                    wcf.AddTakeMedication(TakeMedication);

                    PharmacyList = wcf.GetAllPharmacy().ToList();

                    //isUpdateMedicalStaff = true;
                    MessageBox.Show("You have successfully delivered a cure to the patient!");
                    //takeMedicationWindow.Close();
                    takeMedicationWindow.txtDatePicker.Text = "";
                    takeMedicationWindow.txtQuantity.Text = "";
                    MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                    PatientsList = wcf.GetAllPatients().ToList();
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
            if (takeMedicationWindow.txtDatePicker.Text == "" ||
                takeMedicationWindow.txtQuantity.Text == ""
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
                takeMedicationWindow.Close();
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
