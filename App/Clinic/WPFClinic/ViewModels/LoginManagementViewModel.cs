using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WCFClinic;
using WPFClinic.Commands;
using WPFClinic.ServiceReference1;
using WPFClinic.View;

namespace WPFClinic.ViewModels
{
    class LoginManagementViewModel : ViewModelBase
    {
        LoginManagement login;

        public LoginManagementViewModel(LoginManagement loginOpen)
        {
            login = loginOpen;
        }

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

        private ICommand addManagement;
        public ICommand AddManagement
        {
            get
            {
                if (addManagement == null)
                {
                    addManagement = new RelayCommand(param => AddManagementExecute(), param => CanAddManagementExecute());
                }
                return addManagement;
            }
        }

        private void AddManagementExecute()
        {
            try
            {
                using (Service1Client wcf = new Service1Client())
                {
                    if (wcf.LoginUserManagement(UserName, login.txtPass.Password))
                    {

                        ManagementAddEdit management = new ManagementAddEdit();
                        management.ShowDialog();
                        login.Close();
                    }
                    else
                    {
                        MessageBox.Show("You did not enter valid data!");
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

        private bool CanAddManagementExecute()
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
    }

}
