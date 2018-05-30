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
    class MedicalStaffAddEditViewModel : ViewModelBase
    {
        MedicalStaffAddEdit medicalStaffAddEdit;

        #region Constructor
        public MedicalStaffAddEditViewModel(MedicalStaffAddEdit medicalStaffAddEditOpen)
        {
            medicalStaffAddEdit = medicalStaffAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
            }
        }
        #endregion

        #region Properties
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
        #endregion

        #region Command AddNewMedicalStaff
        private ICommand addNewMedicalStaff;
        public ICommand AddNewMedicalStaff
        {
            get
            {
                if (addNewMedicalStaff == null)
                {
                    addNewMedicalStaff = new RelayCommand(param => AddNewMedicalStaffExecute(), param => CanAddNewMedicalStaffExecute());
                }
                return addNewMedicalStaff;
            }
        }

        private void AddNewMedicalStaffExecute()
        {
            try
            {
                AddMedicalStaff addMedicalStaff = new AddMedicalStaff();
                addMedicalStaff.ShowDialog();
                    using (Service1Client wcf = new Service1Client())
                    {
                        MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewMedicalStaffExecute()
        {
            return true;
        }
        #endregion

        #region Command EditMedicalStaff
        private ICommand editMedicalStaff;
        public ICommand EditMedicalStaff
        {
            get
            {
                if (editMedicalStaff == null)
                {
                    editMedicalStaff = new RelayCommand(param => EditMedicalStaffExecute(), param => CanEditMedicalStaffExecute());
                }

                return editMedicalStaff;
            }
        }

        private void EditMedicalStaffExecute()
        {
            try
            {
                if (editMedicalStaff != null)
                {
                    AddMedicalStaff addMedicalStaff = new AddMedicalStaff(MedicalStaff);
                    addMedicalStaff.ShowDialog();
                        using (Service1Client wcf = new Service1Client())
                        {
                            MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditMedicalStaffExecute()
        {
            if (MedicalStaff == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeleteMedicalStaff
        private ICommand deleteMedicalStaff;
        public ICommand DeleteMedicalStaff
        {
            get
            {
                if (deleteMedicalStaff == null)
                {
                    deleteMedicalStaff = new RelayCommand(param => DeleteMedicalStaffExecute(), param => CanDeleteMedicalStaffExecute());
                }

                return deleteMedicalStaff;
            }
        }

        private void DeleteMedicalStaffExecute()
        {
            try
            {
                if (MedicalStaff != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int medicalStaffID = medicalStaff.MedicalStaffID;
                        bool isMedicalStaff = wcf.isMedicalStaffID(medicalStaffID);
                        if (isMedicalStaff == true)
                        {
                            wcf.DeleteMedicalStaff(medicalStaffID);
                            MedicalStaffList = wcf.GetAllMedicalStaff().ToList();
                            MessageBox.Show("You are successfully deleted selected Medical Staff!");
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

        private bool CanDeleteMedicalStaffExecute()
        {
            if (MedicalStaff == null)
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
                medicalStaffAddEdit.Close();
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
