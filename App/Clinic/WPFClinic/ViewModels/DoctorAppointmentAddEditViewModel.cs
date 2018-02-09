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
    class DoctorAppointmentAddEditViewModel : ViewModelBase
    {
        DoctorAppointmentAddEdit doctorAppointmentAddEdit;

        #region Constructor
        public DoctorAppointmentAddEditViewModel(DoctorAppointmentAddEdit doctorAppointmentAddEditOpen)
        {
            doctorAppointmentAddEdit = doctorAppointmentAddEditOpen;
            using (Service1Client wcf = new Service1Client())
            {
                DoctorAppointmentList = wcf.GetAllDoctorAppointment().ToList();
            }
        }
        #endregion

        #region Properties
        private vwDoctorAppointment doctorAppointment;
        public vwDoctorAppointment DoctorAppointment
        {
            get
            {
                return doctorAppointment;
            }

            set
            {
                doctorAppointment = value;
                OnPropertyChanged("DoctorAppointment");
            }
        }

        private List<vwDoctorAppointment> doctorAppointmentList;
        public List<vwDoctorAppointment> DoctorAppointmentList
        {
            get
            {
                return doctorAppointmentList;
            }

            set
            {
                doctorAppointmentList = value;
                OnPropertyChanged("DoctorAppointmentList");
            }
        }
        #endregion

        #region Command AddNewDoctorAppointment
        private ICommand addNewDoctorAppointment;
        public ICommand AddNewDoctorAppointment
        {
            get
            {
                if (addNewDoctorAppointment == null)
                {
                    addNewDoctorAppointment = new RelayCommand(param => AddNewDoctorAppointmentExecute(), param => CanAddNewDoctorAppointmentExecute());
                }
                return addNewDoctorAppointment;
            }
        }

        private void AddNewDoctorAppointmentExecute()
        {
            try
            {
                AddDoctorAppointment addDoctorAppointment = new AddDoctorAppointment();
                addDoctorAppointment.ShowDialog();
                using (Service1Client wcf = new Service1Client())
                {
                    DoctorAppointmentList = wcf.GetAllDoctorAppointment().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanAddNewDoctorAppointmentExecute()
        {
            return true;
        }
        #endregion

        #region Command EditDoctorAppointment
        private ICommand editDoctorAppointment;
        public ICommand EditDoctorAppointment
        {
            get
            {
                if (editDoctorAppointment == null)
                {
                    editDoctorAppointment = new RelayCommand(param => EditDoctorAppointmentExecute(), param => CanEditDoctorAppointmentExecute());
                }

                return editDoctorAppointment;
            }
        }

        private void EditDoctorAppointmentExecute()
        {
            try
            {
                if (editDoctorAppointment != null)
                {
                    AddDoctorAppointment addDoctorAppointment = new AddDoctorAppointment(DoctorAppointment);
                    addDoctorAppointment.ShowDialog();
                    using (Service1Client wcf = new Service1Client())
                    {
                        DoctorAppointmentList = wcf.GetAllDoctorAppointment().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditDoctorAppointmentExecute()
        {
            if (DoctorAppointment == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Command DeleteDoctorAppointment
        private ICommand deleteDoctorAppointment;
        public ICommand DeleteDoctorAppointment
        {
            get
            {
                if (deleteDoctorAppointment == null)
                {
                    deleteDoctorAppointment = new RelayCommand(param => DeleteDoctorAppointmentExecute(), param => CanDeleteDoctorAppointmentExecute());
                }

                return deleteDoctorAppointment;
            }
        }

        private void DeleteDoctorAppointmentExecute()
        {
            try
            {
                if (DoctorAppointment != null)
                {
                    using (Service1Client wcf = new Service1Client())
                    {
                        int doctorAppointmentID = doctorAppointment.DoctorAppointmentID;
                        bool isDoctorAppointment = wcf.isDoctorAppointmentID(doctorAppointmentID);
                        if (isDoctorAppointment == true)
                        {
                            wcf.DeleteDoctorAppointment(doctorAppointmentID);
                            DoctorAppointmentList = wcf.GetAllDoctorAppointment().ToList();
                            MessageBox.Show("You are successfully deleted selected Doctor Appointment!");
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

        private bool CanDeleteDoctorAppointmentExecute()
        {
            if (DoctorAppointment == null)
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
                doctorAppointmentAddEdit.Close();
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
