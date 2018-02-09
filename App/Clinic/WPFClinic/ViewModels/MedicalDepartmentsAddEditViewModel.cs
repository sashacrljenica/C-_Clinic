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
    class MedicalDepartmentsAddEditViewModel : ViewModelBase
    {
        MedicalDepartmentsAddEdit medicalDepartmentsAddEdit;

        #region Constructor
        public MedicalDepartmentsAddEditViewModel(MedicalDepartmentsAddEdit medicalDepartmentsAddEditOpen)
        {
            medicalDepartmentsAddEdit = medicalDepartmentsAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
            }
        }
        #endregion

        #region Properties
        private vwMedicalDepartments medicalDepartments;
        public vwMedicalDepartments MedicalDepartments
        {
            get
            {
                return medicalDepartments;
            }

            set
            {
                medicalDepartments = value;
                OnPropertyChanged("MedicalDepartments");
            }
        }

        private List<vwMedicalDepartments> medicalDepartmentsList;
        public List<vwMedicalDepartments> MedicalDepartmentsList
        {
            get
            {
                return medicalDepartmentsList;
            }

            set
            {
                medicalDepartmentsList = value;
                OnPropertyChanged("MedicalDepartmentsList");
            }
        }
        #endregion

        #region Command AddNewMedicalDepartment
        private ICommand addNewMedicalDepartment;
        public ICommand AddNewMedicalDepartment
        {
            get
            {
                if (addNewMedicalDepartment == null)
                {
                    addNewMedicalDepartment = new RelayCommand(param => AddNewMedicalDepartmentExecute(), param => CanAddNewMedicalDepartmentExecute());
                }
                return addNewMedicalDepartment;
            }
        }

        private void AddNewMedicalDepartmentExecute()
        {
            try
            {
                AddMedicalDepartments addMedicalDepartment = new AddMedicalDepartments();
                addMedicalDepartment.ShowDialog();
                using (Service1Client wcf = new Service1Client())
                {
                    MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewMedicalDepartmentExecute()
        {
            return true;
        }
        #endregion

        #region Command EditMedicalDepartment
        private ICommand editMedicalDepartment;
        public ICommand EditMedicalDepartment
        {
            get
            {
                if (editMedicalDepartment == null)
                {
                    editMedicalDepartment = new RelayCommand(param => EditMedicalDepartmentExecute(), param => CanEditMedicalDepartmentExecute());
                }

                return editMedicalDepartment;
            }
        }

        private void EditMedicalDepartmentExecute()
        {
            try
            {
                if (editMedicalDepartment != null)
                {
                    AddMedicalDepartments addMedicalDepartments = new AddMedicalDepartments(MedicalDepartments);
                    addMedicalDepartments.ShowDialog();
                    using (Service1Client wcf = new Service1Client())
                    {
                        MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditMedicalDepartmentExecute()
        {
            if (MedicalDepartments == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeleteMedicalDepartment
        private ICommand deleteMedicalDepartment;
        public ICommand DeleteMedicalDepartment
        {
            get
            {
                if (deleteMedicalDepartment == null)
                {
                    deleteMedicalDepartment = new RelayCommand(param => DeleteMedicalDepartmentExecute(), param => CanDeleteMedicalDepartmentExecute());
                }

                return deleteMedicalDepartment;
            }
        }

        private void DeleteMedicalDepartmentExecute()
        {
            try
            {
                if (MedicalDepartments != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int medicalDepartmentID = medicalDepartments.MedicalDepartmentID;
                        bool isMedicalDepartment = wcf.isMedicalDepartmentID(medicalDepartmentID);
                        if (isMedicalDepartment == true)
                        {
                            wcf.DeleteMedicalDepartment(medicalDepartmentID);
                            MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
                            MessageBox.Show("You are successfully deleted selected medical department!");
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
        private bool CanDeleteMedicalDepartmentExecute()
        {
            if (MedicalDepartments == null)
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
                medicalDepartmentsAddEdit.Close();
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
