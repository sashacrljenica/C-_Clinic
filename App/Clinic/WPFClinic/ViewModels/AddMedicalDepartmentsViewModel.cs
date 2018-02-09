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
    class AddMedicalDepartmentsViewModel : ViewModelBase
    {
        AddMedicalDepartments addMedicalDepartments;

        #region Constructor
        public AddMedicalDepartmentsViewModel(AddMedicalDepartments addMedicalDepartmentsOpen)
        {
            medicalDepartments = new vwMedicalDepartments();
            addMedicalDepartments = addMedicalDepartmentsOpen;
        }

        public AddMedicalDepartmentsViewModel(AddMedicalDepartments addMedicalDepartmentsOpen, vwMedicalDepartments medicalDepartmentsToEdit)
        {
            medicalDepartments = medicalDepartmentsToEdit;
            addMedicalDepartments = addMedicalDepartmentsOpen;
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

                    wcf.AddMedicalDepartments(MedicalDepartments);
                    MessageBox.Show("You are successfully added Medical Department!");
                    addMedicalDepartments.Close();
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
            if (medicalDepartments.NameOfDepartment == null ||
                medicalDepartments.NumberOfFloor == null ||
                medicalDepartments.ContactPhoneOfDepartment == null)
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
                addMedicalDepartments.Close();
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
