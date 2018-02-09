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
    class TakeMedicationAddEditViewModel : ViewModelBase
    {
        TakeMedicationAddEdit takeMedicationAddEdit;

        #region Constructor
        public TakeMedicationAddEditViewModel(TakeMedicationAddEdit takeMedicationAddEditOpen)
        {
            takeMedicationAddEdit = takeMedicationAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                TakeMedicationList = wcf.GetAllTakeMedication().ToList();
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

        private List<vwTakeMedication> takeMedicationList;
        public List<vwTakeMedication> TakeMedicationList
        {
            get
            {
                return takeMedicationList;
            }

            set
            {
                takeMedicationList = value;
                OnPropertyChanged("TakeMedicationList");
            }
        }
        #endregion

        #region Command AddNewTakeMedication
        private ICommand addNewTakeMedication;
        public ICommand AddNewTakeMedication
        {
            get
            {
                if (addNewTakeMedication == null)
                {
                    addNewTakeMedication = new RelayCommand(param => AddNewTakeMedicationExecute(), param => CanAddNewTakeMedicationExecute());
                }
                return addNewTakeMedication;
            }
        }

        private void AddNewTakeMedicationExecute()
        {
            try
            {
                AddTakeMedication addTakeMedication = new AddTakeMedication();
                addTakeMedication.ShowDialog();
                using (Service1Client wcf = new Service1Client())
                {
                    TakeMedicationList = wcf.GetAllTakeMedication().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewTakeMedicationExecute()
        {
            return true;
        }
        #endregion

        #region Command EditTakeMedication
        private ICommand editTakeMedication;
        public ICommand EditTakeMedication
        {
            get
            {
                if (editTakeMedication == null)
                {
                    editTakeMedication = new RelayCommand(param => EditTakeMedicationExecute(), param => CanEditTakeMedicationExecute());
                }

                return editTakeMedication;
            }
        }

        private void EditTakeMedicationExecute()
        {
            try
            {
                if (editTakeMedication != null)
                {
                    AddTakeMedication addTakeMedication = new AddTakeMedication(TakeMedication);
                    addTakeMedication.ShowDialog();
                    using (Service1Client wcf = new Service1Client())
                    {
                        TakeMedicationList = wcf.GetAllTakeMedication().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditTakeMedicationExecute()
        {
            if (TakeMedication == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeleteTakeMedication
        private ICommand deleteTakeMedication;
        public ICommand DeleteTakeMedication
        {
            get
            {
                if (deleteTakeMedication == null)
                {
                    deleteTakeMedication = new RelayCommand(param => DeleteTakeMedicationExecute(), param => CanDeleteTakeMedicationExecute());
                }

                return deleteTakeMedication;
            }
        }

        private void DeleteTakeMedicationExecute()
        {
            try
            {
                if (TakeMedication != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int takeMedicationID = takeMedication.TakeMedicationID;
                        bool isTakeMedication = wcf.isTakeMedicationID(takeMedicationID);
                        if (isTakeMedication == true)
                        {
                            wcf.DeleteTakeMedication(takeMedicationID);
                            TakeMedicationList = wcf.GetAllTakeMedication().ToList();
                            MessageBox.Show("You are successfully deleted selected Take Medication!");
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

        private bool CanDeleteTakeMedicationExecute()
        {
            if (TakeMedication == null)
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
                takeMedicationAddEdit.Close();
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
