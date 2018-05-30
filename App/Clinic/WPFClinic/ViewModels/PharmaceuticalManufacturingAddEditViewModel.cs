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
    class PharmaceuticalManufacturingAddEditViewModel : ViewModelBase
    {
        PharmaceuticalManufacturingAddEdit pharmaceuticalManufacturingAddEdit;

        #region Constructor
        public PharmaceuticalManufacturingAddEditViewModel(PharmaceuticalManufacturingAddEdit pharmaceuticalManufacturingAddEditOpen)
        {
            pharmaceuticalManufacturingAddEdit = pharmaceuticalManufacturingAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
            }
        }
        #endregion

        #region Properties
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

        #region Command AddNewPharmaceuticalManufacturing
        private ICommand addNewPharmaceuticalManufacturing;
        public ICommand AddNewPharmaceuticalManufacturing
        {
            get
            {
                if (addNewPharmaceuticalManufacturing == null)
                {
                    addNewPharmaceuticalManufacturing = new RelayCommand(param => AddNewPharmaceuticalManufacturingExecute(), param => CanAddNewPharmaceuticalManufacturingExecute());
                }
                return addNewPharmaceuticalManufacturing;
            }
        }

        private void AddNewPharmaceuticalManufacturingExecute()
        {
            try
            {
                AddPharmaceuticalManufacturing addPharmaceuticalManufacturing = new AddPharmaceuticalManufacturing();
                addPharmaceuticalManufacturing.ShowDialog();
                using (Service1Client wcf = new Service1Client())
                {
                    PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewPharmaceuticalManufacturingExecute()
        {
            return true;
        }
        #endregion

        #region Command EditPharmaceuticalManufacturing
        private ICommand editPharmaceuticalManufacturing;
        public ICommand EditPharmaceuticalManufacturing
        {
            get
            {
                if (editPharmaceuticalManufacturing == null)
                {
                    editPharmaceuticalManufacturing = new RelayCommand(param => EditPharmaceuticalManufacturingExecute(), param => CanEditPharmaceuticalManufacturingExecute());
                }
                return editPharmaceuticalManufacturing;
            }
        }

        private void EditPharmaceuticalManufacturingExecute()
        {
            try
            {
                if (editPharmaceuticalManufacturing != null)
                {
                    AddPharmaceuticalManufacturing addPharmaceuticalManufacturing = new AddPharmaceuticalManufacturing(PharmaceuticalManufacturing);
                    addPharmaceuticalManufacturing.ShowDialog();
                    using (Service1Client wcf = new Service1Client())
                    {
                        PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditPharmaceuticalManufacturingExecute()
        {
            if (PharmaceuticalManufacturing == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeletePharmaceuticalManufacturing
        private ICommand deletePharmaceuticalManufacturing;
        public ICommand DeletePharmaceuticalManufacturing
        {
            get
            {
                if (deletePharmaceuticalManufacturing == null)
                {
                    deletePharmaceuticalManufacturing = new RelayCommand(param => DeletePharmaceuticalManufacturingExecute(), param => CanDeletePharmaceuticalManufacturingExecute());
                }

                return deletePharmaceuticalManufacturing;
            }
        }

        private void DeletePharmaceuticalManufacturingExecute()
        {
            try
            {
                if (PharmaceuticalManufacturing != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int pharmaceuticalManufacturingID = pharmaceuticalManufacturing.ManufacturerID;
                        bool isPharmaceuticalManufacturing = wcf.isManufacturerID(pharmaceuticalManufacturingID);
                        if (isPharmaceuticalManufacturing == true)
                        {
                            wcf.DeletePharmaceuticalManufacturing(pharmaceuticalManufacturingID);
                            PharmaceuticalManufacturingList = wcf.GetAllPharmaceuticalManufacturing().ToList();
                            MessageBox.Show("You are successfully deleted selected Pharmaceutical Manufacturing!");
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
        private bool CanDeletePharmaceuticalManufacturingExecute()
        {
            if (PharmaceuticalManufacturing == null)
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
                pharmaceuticalManufacturingAddEdit.Close();
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
