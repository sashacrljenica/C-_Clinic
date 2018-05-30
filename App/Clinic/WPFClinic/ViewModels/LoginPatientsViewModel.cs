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
    class LoginPatientsViewModel : ViewModelBase
    {
        LoginPatients loginPatients;

        #region Constructor

        public LoginPatientsViewModel(LoginPatients loginPatientsOpen)
        {
            login = new vwLogin();
            patients = new vwPatients();
            loginPatients = loginPatientsOpen;
        }

        #endregion

        #region Properties

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

        #region RadioButton MaleOrFemale

        public bool rbMale
        {
            get
            {
                if (patients != null && patients.MaleOrFemale == "M")
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    patients.MaleOrFemale = "M";
                }
            }
        }

        public bool rbFemale
        {
            get
            {
                if (patients != null && patients.MaleOrFemale == "F")
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value)
                {
                    patients.MaleOrFemale = "F";
                }
            }
        }
        #endregion


        #endregion

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
                    wcf.AddPatient(Patients);
                    int maxElement = wcf.maxPatientID();
                    if (Login.LoginID == 0)
                    {
                        Login.UserName = loginPatients.txtUserName.Text;
                        Login.Password = loginPatients.txtPassword.Password;
                        Login.PatientID = maxElement;
                        //login.MedicalStaffID = 1;
                    }
                    wcf.AddLogin(Login);
                    MessageBox.Show("You are successfully registered!");
                    loginPatients.Close();
                    LoginRegistrationPatients view = new LoginRegistrationPatients();
                    view.ShowDialog();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error. Repeat the entry!");
                MessageBox.Show(ex.ToString());
                loginPatients.txtUserName.Text = "";
                loginPatients.txtPassword.Password = "";
                loginPatients.txtAddress.Text = "";
                loginPatients.cmbBloodType.Text = "";
                loginPatients.txtIDCard.Text = "";
                loginPatients.txtName.Text = "";
                loginPatients.txtPhoneContact.Text = "";
                loginPatients.txtPhoneHome.Text = "";
                loginPatients.txtPhoneMobile.Text = "";
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(loginPatients.txtName.Text) ||
               String.IsNullOrEmpty(loginPatients.txtPassword.Password) ||
               String.IsNullOrEmpty(loginPatients.txtAddress.Text) ||
               String.IsNullOrEmpty(loginPatients.txtName.Text) ||
               String.IsNullOrEmpty(loginPatients.txtDatePicker.Text) ||
               String.IsNullOrEmpty(loginPatients.txtIDCard.Text) ||
               String.IsNullOrEmpty(loginPatients.txtPhoneContact.Text) ||
               String.IsNullOrEmpty(loginPatients.txtPhoneHome.Text) ||
               String.IsNullOrEmpty(loginPatients.txtPhoneMobile.Text) ||
               String.IsNullOrEmpty(loginPatients.cmbBloodType.Text)
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
                loginPatients.Close();
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
