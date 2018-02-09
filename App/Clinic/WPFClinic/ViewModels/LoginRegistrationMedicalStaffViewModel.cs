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
    class LoginRegistrationMedicalStaffViewModel : ViewModelBase
    {
        LoginRegistrationMedicalStaff login;

        #region Constructor
        public LoginRegistrationMedicalStaffViewModel(LoginRegistrationMedicalStaff loginOpen)
        {
            loginCurrent = new vwLoginCurrent();
            login = loginOpen;
        }
        #endregion

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

        #region Command TakeMedication

        private ICommand takeMedication;
        public ICommand TakeMedication
        {
            get
            {
                if (takeMedication == null)
                {
                    takeMedication = new RelayCommand(param => TakeMedicationExecute(), param => CanTakeMedicationExecute());
                }
                return takeMedication;
            }
        }

        private void TakeMedicationExecute()
        {
            try
            {
                using (Service1Client wcf = new Service1Client())
                {
                    if (wcf.LoginUserMedicalStaff(UserName, login.txtPass.Password))
                    {

                        int currentMedicalStaffID = wcf.currentMedicalStaffID(UserName, login.txtPass.Password);

                        List<vwMedicalStaff> medicalStaffDetail = wcf.GetMedicalStaffsDetail(currentMedicalStaffID).ToList();
                        string nameOfMedicalStaff = null;

                        foreach (vwMedicalStaff item in medicalStaffDetail)
                        {
                            nameOfMedicalStaff = item.NameAndSurname;
                        }

                        MessageBox.Show("Welcome " + nameOfMedicalStaff + ", you are successfully loged in!");

                        loginCurrent.MedicalStaffID = currentMedicalStaffID;
                        wcf.AddLoginCurrent(LoginCurrent);

                        TakeMedication view = new TakeMedication();
                        view.ShowDialog();
                        login.Close();
                    }
                    else
                    {
                        MessageBox.Show("You did not enter valid data\nor need to register first!");
                        login.txtName.Text = "";
                        login.txtPass.Password = "";
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanTakeMedicationExecute()
        {
            if (string.IsNullOrEmpty(login.txtName.Text) && string.IsNullOrEmpty(login.txtPass.Password))
            {

                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command RegistrationMedicallStaff

        private ICommand registrationMedicallStaff;
        public ICommand RegistrationMedicallStaff
        {
            get
            {
                if (registrationMedicallStaff == null)
                {
                    registrationMedicallStaff = new RelayCommand(param => RegistrationMedicallStaffExecute(), param => CanRegistrationMedicallStaffExecute());
                }
                return registrationMedicallStaff;
            }
        }

        private void RegistrationMedicallStaffExecute()
        {
            try
            {
                LoginMedicalStaff view = new LoginMedicalStaff();
                login.Close();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanRegistrationMedicallStaffExecute()
        {
            return true;
        }
        #endregion
    }
}
