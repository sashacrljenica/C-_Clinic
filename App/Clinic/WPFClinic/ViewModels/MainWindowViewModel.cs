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
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;

        #region Constructors

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        #endregion

        #region Command LoginPatients

        private ICommand loginPatients;
        public ICommand LoginPatients
        {
            get
            {
                if (loginPatients == null)
                {
                    loginPatients = new RelayCommand(param => LoginPatientsExecute(), param => CanLoginPatientsExecute());
                }
                return loginPatients;
            }
        }

        private void LoginPatientsExecute()
        {
            try
            {
                LoginRegistrationPatients loginRegistrationPatient = new LoginRegistrationPatients();
                loginRegistrationPatient.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginPatientsExecute()
        {
            return true;
        }
        #endregion

        #region Command LoginMedicalStaff

        private ICommand loginMedicalStaff;
        public ICommand LoginMedicalStaff
        {
            get
            {
                if (loginMedicalStaff == null)
                {
                    loginMedicalStaff = new RelayCommand(param => LoginMedicalStaffExecute(), param => CanLoginMedicalStaffExecute());
                }
                return loginMedicalStaff;
            }
        }

        private void LoginMedicalStaffExecute()
        {
            try
            {
                LoginRegistrationMedicalStaff loginMedicalStaff = new LoginRegistrationMedicalStaff();
                loginMedicalStaff.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginMedicalStaffExecute()
        {
            return true;
        }
        #endregion

        #region Command LoginManagement

        private ICommand loginManagement;
        public ICommand LoginManagement
        {
            get
            {
                if (loginManagement == null)
                {
                    loginManagement = new RelayCommand(param => LoginManagementExecute(), param => CanLoginManagementExecute());
                }
                return loginManagement;
            }
        }

        private void LoginManagementExecute()
        {
            try
            {
                LoginManagement addLogin = new LoginManagement();
                addLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginManagementExecute()
        {
            return true;
        }
        #endregion
    }
}
