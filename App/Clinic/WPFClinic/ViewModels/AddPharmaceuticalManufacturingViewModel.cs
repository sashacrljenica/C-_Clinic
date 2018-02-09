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
    class AddPharmaceuticalManufacturingViewModel : ViewModelBase
    {
        AddPharmaceuticalManufacturing addPharmaceuticalManufacturing;

        #region Constructor
        public AddPharmaceuticalManufacturingViewModel(AddPharmaceuticalManufacturing addPharmaceuticalManufacturingOpen)
        {
            pharmaceuticalManufacturing = new vwPharmaceuticalManufacturing();
            addPharmaceuticalManufacturing = addPharmaceuticalManufacturingOpen;
        }

        public AddPharmaceuticalManufacturingViewModel(AddPharmaceuticalManufacturing addPharmaceuticalManufacturingOpen, vwPharmaceuticalManufacturing pharmaceuticalManufacturingToEdit)
        {
            pharmaceuticalManufacturing = pharmaceuticalManufacturingToEdit;
            addPharmaceuticalManufacturing = addPharmaceuticalManufacturingOpen;
        }
        #endregion

        #region Properties
        private vwPharmaceuticalManufacturing pharmaceuticalManufacturing;

        public vwPharmaceuticalManufacturing PharmaceuticalManufacturing
        {
            get
            {
                return pharmaceuticalManufacturing;
            }

            set
            {
                pharmaceuticalManufacturing = value;
                OnPropertyChanged("PharmaceuticalManufacturing");
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

                    wcf.AddPharmaceuticalManufacturing(PharmaceuticalManufacturing);
                    MessageBox.Show("You are successfully added Pharmaceutical Manufacturing!");
                    addPharmaceuticalManufacturing.Close();
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
            if (pharmaceuticalManufacturing.ManufacturerName == null ||
                pharmaceuticalManufacturing.AddressOfManufacturer == null ||
                pharmaceuticalManufacturing.PhoneNumberOfManufacturer == null ||
                pharmaceuticalManufacturing.EmailOfManufacturer == null ||
                pharmaceuticalManufacturing.Note == null)
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
                addPharmaceuticalManufacturing.Close();
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
