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
    class AddTakeMedicationViewModel : ViewModelBase
    {
        AddTakeMedication addTakeMedication;

        #region Constructor
        public AddTakeMedicationViewModel(AddTakeMedication addTakeMedicationOpen)
        {
            takeMedication = new vwTakeMedication();
            addTakeMedication = addTakeMedicationOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmacyList = wcf.GetAllPharmacy().ToList();
                MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                PatientsList = wcf.GetAllPatients().ToList();
            }


        }

        public AddTakeMedicationViewModel(AddTakeMedication addTakeMedicationOpen, vwTakeMedication takeMedicationToEdit)
        {
            takeMedication = takeMedicationToEdit;
            addTakeMedication = addTakeMedicationOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmacyList = wcf.GetAllPharmacy().ToList();
                MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                PatientsList = wcf.GetAllPatients().ToList();
            }
        }
        #endregion

        #region Properties
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
            get
            {
                return pharmacyList;
            }

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
                    if (takeMedication.TakeMedicationID == 0)
                    {
                        int quantityPharmacy = Convert.ToInt32(Pharmacy.QuantityOnCondition);
                        int quantityTakeMedication = Convert.ToInt32(addTakeMedication.txtQuantity.Text);
                        int newQuantity = quantityPharmacy - quantityTakeMedication;
                        //string newQuantityString = newQuantity.ToString();

                        pharmacy.QuantityOnCondition = newQuantity;
                        takeMedication.QuantityOfTheDrug = quantityTakeMedication;
                        wcf.AddPharmacy(Pharmacy);
                    }

                    if (takeMedication.TakeMedicationID != 0)
                    {
                        int quantityOldTakeMedication = 0;

                        // Iz baze uzimamo podatak iz tabele TakeMedication, samo jedan red, koji odgovara ID-u takeMedication.TakeMedicationID
                        List<vwTakeMedication> takeMedicationDetailList = wcf.GetTakeMedicationDetail(takeMedication.TakeMedicationID).ToList();

                        // Iz liste takeMedicationDetailList uzimamo podatak vrednost o kolicini lekova QuantityOFTheDrugs
                        foreach (vwTakeMedication item in takeMedicationDetailList)
                        {
                            quantityOldTakeMedication = item.QuantityOfTheDrug.Value;
                        }

                        int quantityNewTakeMedication = Convert.ToInt32(addTakeMedication.txtQuantity.Text);

                        // Ako je Nova kolicina uzetih lekova veca od uzete stare kolicine lekova
                        if (quantityOldTakeMedication < quantityNewTakeMedication)
                        {
                            int newQuantity = Convert.ToInt32(pharmacy.QuantityOnCondition) - (quantityNewTakeMedication - quantityOldTakeMedication);
                            pharmacy.QuantityOnCondition = newQuantity;
                            takeMedication.QuantityOfTheDrug = quantityNewTakeMedication;
                        }

                        // Ako je Nova kolicina uzetih lekova manja od stare kolicine izdatih lekova
                        if (quantityOldTakeMedication > quantityNewTakeMedication)
                        {
                            int newQuantity = Convert.ToInt32(pharmacy.QuantityOnCondition) + (quantityOldTakeMedication - quantityNewTakeMedication);

                            pharmacy.QuantityOnCondition = newQuantity;

                            takeMedication.QuantityOfTheDrug = quantityNewTakeMedication;
                        }
                        // Dodavanje u tabelu tblPharmacy objekta Pharmacy, sa svim svojim podacima
                        wcf.AddPharmacy(Pharmacy);
                    }

                    // Ovde u objekat TakeMedication, dodajemo karakteristicne strane kljuceve iz povezanih tabela
                    takeMedication.PharmacyID = pharmacy.PharmacyID;
                    takeMedication.MedicalStaffID = medicalStaff.MedicalStaffID;
                    takeMedication.PatientID = patients.PatientID;

                    // Dodavanje u tabelu tblTakeMedication objekta TakeMedication, sa svim svojim podacima
                    wcf.AddTakeMedication(TakeMedication);

                    MessageBox.Show("You are successfully added Take Medication!");
                    addTakeMedication.Close();
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
            if (takeMedication.DateOfTaking == null || takeMedication.QuantityOfTheDrug == null ||
                takeMedication.Expr4 == null || takeMedication.NameAndSurname == null)
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
                addTakeMedication.Close();
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
