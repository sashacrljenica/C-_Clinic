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
    class LoginRegistrationPatientsViewModel : ViewModelBase
    {
        LoginRegistrationPatients loginRegistrationPatients;

        public LoginRegistrationPatientsViewModel(LoginRegistrationPatients loginRegistrationPatientsOpen)
        {
            loginCurrent = new vwLoginCurrent();
            loginRegistrationPatients = loginRegistrationPatientsOpen;
        }
        #region Properties
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        //private vwLogin login;
        //public vwLogin Login
        //{
        //    get
        //    {
        //        return login;
        //    }

        //    set
        //    {
        //        login = value;
        //        OnPropertyChanged("Login");
        //    }
        //}

        private vwLoginCurrent loginCurrent;
        public vwLoginCurrent LoginCurrent
        {
            get
            {
                return loginCurrent;
            }

            set
            {
                loginCurrent = value;
                OnPropertyChanged("LoginCurrent");
            }
        }
        #endregion

        #region Command PatientChoice

        private ICommand patientChoice;
        public ICommand PatientChoice
        {
            get
            {
                if (patientChoice == null)
                {
                    patientChoice = new RelayCommand(param => PatientChoiceExecute(), param => CanPatientChoiceExecute());
                }
                return patientChoice;
            }
        }

        private void PatientChoiceExecute()
        {
            try
            {
                using (Service1Client wcf = new Service1Client())
                {
                    if (wcf.LoginUserPatient(UserName, loginRegistrationPatients.txtPass.Password))
                    {
                        int currentPatientID = wcf.currentPatientID(UserName, loginRegistrationPatients.txtPass.Password);

                        List<vwPatients> patientDetail = wcf.GetPatientsDetail(currentPatientID).ToList();
                        string nameOfPatient = null;

                        foreach (vwPatients item in patientDetail)
                        {
                            nameOfPatient = item.NameAndSurname;
                        }

                        MessageBox.Show("Welcome " + nameOfPatient + ", you are successfully loged in!");

                        loginCurrent.PatientID = currentPatientID;
                        wcf.AddLoginCurrent(LoginCurrent);

                        PatientChoice view = new PatientChoice();
                        view.ShowDialog();
                        loginRegistrationPatients.Close();
                    }
                    else
                    {
                        MessageBox.Show("You did not enter valid data\nor need to register first!");
                        loginRegistrationPatients.txtName.Text = "";
                        loginRegistrationPatients.txtPass.Password = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanPatientChoiceExecute()
        {
            if (string.IsNullOrEmpty(loginRegistrationPatients.txtName.Text) && string.IsNullOrEmpty(loginRegistrationPatients.txtPass.Password))
            {

                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command Registration Patients

        private ICommand registrationPatients;
        public ICommand RegistrationPatients
        {
            get
            {
                if (registrationPatients == null)
                {
                    registrationPatients = new RelayCommand(param => RegistrationPatientsExecute(), param => CanRegistrationPatientsExecute());
                }
                return registrationPatients;
            }
        }



        private void RegistrationPatientsExecute()
        {
            try
            {
                LoginPatients view = new LoginPatients();
                loginRegistrationPatients.Close();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanRegistrationPatientsExecute()
        {
            return true;
        }
        #endregion
    }
}
