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
    class PharmacyAddEditViewModel : ViewModelBase
    {
        PharmacyAddEdit pharmacyAddEdit;

        #region Constructor
        public PharmacyAddEditViewModel(PharmacyAddEdit pharmacyAddEditOpen)
        {
            pharmacyAddEdit = pharmacyAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmacyList = wcf.GetAllPharmacy().ToList();
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

        //private List<vwPharmaceuticalManufacturing> pharmaceuticalManufacturingList;
        //public List<vwPharmaceuticalManufacturing> PharmaceuticalManufacturingList
        //{
        //    get
        //    {
        //        return pharmaceuticalManufacturingList;
        //    }

        //    set
        //    {
        //        pharmaceuticalManufacturingList = value;
        //        OnPropertyChanged("PharmaceuticalManufacturingList");
        //    }
        //}
        #endregion

        #region Command AddNewPharmacy
        private ICommand addNewPharmacy;
        public ICommand AddNewPharmacy
        {
            get
            {
                if (addNewPharmacy == null)
                {
                    addNewPharmacy = new RelayCommand(param => AddNewPharmacyExecute(), param => CanAddNewPharmacyExecute());
                }
                return addNewPharmacy;
            }
        }

        private void AddNewPharmacyExecute()
        {
            try
            {
                AddPharmacy addPharmacy = new AddPharmacy();
                addPharmacy.ShowDialog();
                using (Service1Client wcf = new Service1Client())
                {
                    PharmacyList = wcf.GetAllPharmacy().ToList();
                    //PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewPharmacyExecute()
        {
            return true;
        }
        #endregion

        #region Command EditPharmacy
        private ICommand editPharmacy;
        public ICommand EditPharmacy
        {
            get
            {
                if (editPharmacy == null)
                {
                    editPharmacy = new RelayCommand(param => EditPharmacyExecute(), param => CanEditPharmacyExecute());
                }

                return editPharmacy;
            }
        }

        private void EditPharmacyExecute()
        {
            try
            {
                if (editPharmacy != null)
                {
                    AddPharmacy addPharmacy = new AddPharmacy(Pharmacy);
                    addPharmacy.ShowDialog();
                    using (Service1Client wcf = new Service1Client())
                    {
                        PharmacyList = wcf.GetAllPharmacy().ToList();
                        //PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditPharmacyExecute()
        {
            if (Pharmacy == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeletePharmacy
        private ICommand deletePharmacy;
        public ICommand DeletePharmacy
        {
            get
            {
                if (deletePharmacy == null)
                {
                    deletePharmacy = new RelayCommand(param => DeletePharmacyExecute(), param => CanDeletePharmacyExecute());
                }

                return deletePharmacy;
            }
        }

        private void DeletePharmacyExecute()
        {
            try
            {
                if (Pharmacy != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int pharmacyID = pharmacy.PharmacyID;
                        bool isPharmacy = wcf.isPharmacyID(pharmacyID);
                        if (isPharmacy == true)
                        {
                            wcf.DeletePharmacy(pharmacyID);
                            PharmacyList = wcf.GetAllPharmacy().ToList();
                            MessageBox.Show("You are successfully deleted selected Pharmacy!");
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

        private bool CanDeletePharmacyExecute()
        {
            if (Pharmacy == null)
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
                pharmacyAddEdit.Close();
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
