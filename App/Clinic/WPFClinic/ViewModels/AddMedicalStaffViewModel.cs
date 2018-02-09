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
    class AddMedicalStaffViewModel : ViewModelBase
    {
        AddMedicalStaff addMedicalStaff;

        #region Constructor
        public AddMedicalStaffViewModel(AddMedicalStaff addMedicalStaffOpen)
        {
            medicalStaff = new vwMedicalStaff();
            addMedicalStaff = addMedicalStaffOpen;
            using (Service1Client wcf = new Service1Client())
            {
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
            }
        }

        public AddMedicalStaffViewModel(AddMedicalStaff addMedicalStaffOpen, vwMedicalStaff medicalStaffToEdit)
        {
            medicalStaff = medicalStaffToEdit;
            addMedicalStaff = addMedicalStaffOpen;
            using (Service1Client wcf = new Service1Client())
            {
                MedicalDepartmentsList = wcf.GetAllMedicalDepartments().ToList();
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
                OnPropertyChanged("MedicalDepartmentList");
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

        private List<string> nameOfJobList = new List<string>()
        {
            "Director","Deputy Director","Assistant Director for non-medical activities",
            "Chief Nurse-technician","Public Contact Person - PR","Head of the Department of Internist Medicine",
            "Head of the Service - specialist in internal medicine","Doctor of medicine specialists of internal medicine",
            "Doctor of medicine at specialization","Doctor of medicine","The Main Nurse","Nurse-technician",
            "Auxiliary worker / nurse"
        };
        public List<string> NameOfJobList
        {
            get
            {
                return nameOfJobList;
            }
            set
            {
                nameOfJobList = value;
                OnPropertyChanged("NameOfJobList");
            }
        }

        private List<string> statusOfEmployeeList = new List<string>()
        {
            "Permanently employed","Temporary employees","A person practicing professional practice","Interns"
        };
        public List<string> StatusOfEmployeeList
        {
            get
            {
                return statusOfEmployeeList;
            }
            set
            {
                statusOfEmployeeList = value;
                OnPropertyChanged("StatusOfEmployeeList");
            }
        }

        public bool rbMale
        {
            get
            {
                if (medicalStaff != null && medicalStaff.MaleOrFemale == "M")
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    medicalStaff.MaleOrFemale = "M";
                }
            }
        }

        public bool rbFemale
        {
            get
            {
                if (medicalStaff != null && medicalStaff.MaleOrFemale == "F")
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    medicalStaff.MaleOrFemale = "F";
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
                    MedicalStaff.MedicalDepartmentID = MedicalDepartments.MedicalDepartmentID;

                    wcf.AddMedicalStaff(MedicalStaff);
                    MessageBox.Show("You are successfully added Medical Staff!");
                    addMedicalStaff.Close();
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
            if (medicalStaff.NameAndSurname == null || medicalStaff.Address == null || medicalStaff.BloodType == null ||
                medicalStaff.DateOfBirth == null || medicalStaff.MaleOrFemale == null || medicalStaff.NameOfDepartment == null ||
                medicalStaff.NameOfJob == null || medicalStaff.PhoneNumber == null || medicalStaff.StatusOfEmployee == null)
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
                addMedicalStaff.Close();
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
