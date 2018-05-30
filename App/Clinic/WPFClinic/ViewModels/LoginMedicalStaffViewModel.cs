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
    /*
     * Ovo je primer upisa u dve tabele, tblLogin i tblMedicalStaff
     * i iscitavanja podataka iz tabele tblMedicalDepartments
     */
    class LoginMedicalStaffViewModel : ViewModelBase
    {
        LoginMedicalStaff loginMedicalStaff;

        #region Constructor

        public LoginMedicalStaffViewModel(LoginMedicalStaff loginMedicalStaffOpen)
        {
            login = new vwLogin();
            medicalStaff = new vwMedicalStaff();
            // kada hocemo vec postojece podatke da dodelimo nekoj drugoj tabeli onda je ovo visak!
            // Samo ako hocemo nesto novo da kreiramo onda ovo pisemo u konstruktor!
            // Medical Departments iscitavamo iz vec postojece tabele, login i medicalStaff upisujemo!
            // medicalDepartments = new vwMedicalDepartments();
            loginMedicalStaff = loginMedicalStaffOpen;

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

        private vwLogin login;
        public vwLogin Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
                OnPropertyChanged("Login");
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

        private List<string> bloodTypeList = new List<string>()
        {
            "O+","A+","B+","O-","A-","AB+","B-","AB-"
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

        #region Commands

        #region ICommand Save

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
                    if (MedicalStaff.MedicalStaffID == 0)
                    {
                        MedicalStaff.MedicalDepartmentID = MedicalDepartments.MedicalDepartmentID;
                    }
                    wcf.AddMedicalStaff(MedicalStaff);

                    int maxElement = wcf.maxMedicalStaffID();
                    if (Login.LoginID == 0)
                    {
                        Login.UserName = loginMedicalStaff.txtUserName.Text;
                        Login.Password = loginMedicalStaff.txtPassword.Password;
                        Login.MedicalStaffID = maxElement;
                        //login.MedicalStaffID = 1;
                    }
                    wcf.AddLogin(Login);
                    MessageBox.Show("You are successfully registered!");
                    loginMedicalStaff.Close();
                    LoginRegistrationMedicalStaff view = new LoginRegistrationMedicalStaff();
                    view.ShowDialog();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Repeat the entry!");
                MessageBox.Show(ex.ToString());
                loginMedicalStaff.txtUserName.Text = "";
                loginMedicalStaff.txtPassword.Password = "";
                loginMedicalStaff.txtName.Text = "";
                loginMedicalStaff.txtDatePicker.Text = "";
                loginMedicalStaff.txtAddress.Text = "";
                loginMedicalStaff.txtPhoneNumber.Text = "";
                loginMedicalStaff.cmbNameOfJob.Text = "";
                loginMedicalStaff.cmbStatusOfEmployee.Text = "";
                loginMedicalStaff.cmbBloodType.Text = "";
                loginMedicalStaff.cmbMedicalDepartment.Text = "";
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(loginMedicalStaff.txtUserName.Text) ||
               String.IsNullOrEmpty(loginMedicalStaff.txtPassword.Password) ||
               String.IsNullOrEmpty(loginMedicalStaff.txtName.Text) ||
               //!(loginMedicalStaff.rbM.IsEnabled) ||
               //!(loginMedicalStaff.rbF.IsEnabled) ||
               String.IsNullOrEmpty(loginMedicalStaff.txtDatePicker.Text) ||
               String.IsNullOrEmpty(loginMedicalStaff.txtAddress.Text) ||
               String.IsNullOrEmpty(loginMedicalStaff.txtPhoneNumber.Text) ||
               String.IsNullOrEmpty(loginMedicalStaff.cmbNameOfJob.Text) ||
               String.IsNullOrEmpty(loginMedicalStaff.cmbStatusOfEmployee.Text) ||
               String.IsNullOrEmpty(loginMedicalStaff.cmbBloodType.Text)
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

        #region ICommand Close

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
                loginMedicalStaff.Close();
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

        #endregion
    }
}
