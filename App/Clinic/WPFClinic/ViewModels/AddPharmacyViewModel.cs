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
    class AddPharmacyViewModel : ViewModelBase
    {
        AddPharmacy addPharmacy;

        #region Constructor
        public AddPharmacyViewModel(AddPharmacy addPharmacyOpen)
        {
            pharmacy = new vwPharmacy();
            addPharmacy = addPharmacyOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
            }
        }

        public AddPharmacyViewModel(AddPharmacy addPharmacyOpen, vwPharmacy pharmacyToEdit)
        {
            pharmacy = pharmacyToEdit;
            addPharmacy = addPharmacyOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
            }
        }
        #endregion

        #region Properties
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

        private vwPharmaceuticalManufacturing pharmaceuticalManufacturing;
        public vwPharmaceuticalManufacturing PharmaceuticalManufacturing
        {
            get
            {
                return pharmaceuticalManufacturing;
            }

            set
            {
                pharmaceuticalManufacturing = value;
                OnPropertyChanged("PharmaceuticalManufacturing");
            }
        }

        private List<vwPharmaceuticalManufacturing> pharmaceuticalManufacturingList;
        public List<vwPharmaceuticalManufacturing> PharmaceuticalManufacturingList
        {
            get
            {
                return pharmaceuticalManufacturingList;
            }

            set
            {
                pharmaceuticalManufacturingList = value;
                OnPropertyChanged("PharmaceuticalManufacturingList");
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
                    //if (pharmacy.PharmacyID == 0)
                    //{
                        pharmacy.ManufacturerID = pharmaceuticalManufacturing.ManufacturerID;
                    //}
                    wcf.AddPharmacy(Pharmacy);
                    MessageBox.Show("You are successfully added Pharmacy!");
                    addPharmacy.Close();
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
            if (pharmacy.NameOfTheDrug == null || pharmacy.ScopeOfApplication == null || pharmacy.QuantityOnCondition == null ||
                pharmacy.Note == null || pharmacy.ManufacturerName == null)
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
                addPharmacy.Close();
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
