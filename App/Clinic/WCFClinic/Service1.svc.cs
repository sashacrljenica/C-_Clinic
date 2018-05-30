using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFClinic
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #region Medical Departments
        List<vwMedicalDepartments> IService1.GetAllMedicalDepartments()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwMedicalDepartments> list = new List<vwMedicalDepartments>();
                    list = (from x in context.vwMedicalDepartments select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwMedicalDepartments IService1.AddMedicalDepartments(vwMedicalDepartments medicalDepartment)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (medicalDepartment.MedicalDepartmentID == 0)
                    {
                        tblMedicalDepartments newMedicalDepartment = new tblMedicalDepartments();
                        newMedicalDepartment.NameOfDepartment = medicalDepartment.NameOfDepartment;
                        newMedicalDepartment.NumberOfFloor = medicalDepartment.NumberOfFloor;
                        newMedicalDepartment.ContactPhoneOfDepartment = medicalDepartment.ContactPhoneOfDepartment;

                        context.tblMedicalDepartments.Add(newMedicalDepartment);
                        context.SaveChanges();
                        medicalDepartment.MedicalDepartmentID = newMedicalDepartment.MedicalDepartmentID;
                        return medicalDepartment;
                    }
                    else
                    {
                        tblMedicalDepartments medicalDepartmentToEdit = (from s in context.tblMedicalDepartments
                                                                         where s.MedicalDepartmentID == medicalDepartment.MedicalDepartmentID
                                                                         select s).First();
                        medicalDepartmentToEdit.NameOfDepartment = medicalDepartment.NameOfDepartment;
                        medicalDepartmentToEdit.NumberOfFloor = medicalDepartment.NumberOfFloor;
                        medicalDepartmentToEdit.ContactPhoneOfDepartment = medicalDepartment.ContactPhoneOfDepartment;
                        context.SaveChanges();
                        return medicalDepartment;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeleteMedicalDepartment(int medicalDepartmentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var medical = (from m in context.tblMedicalDepartments
                                   where m.MedicalDepartmentID == medicalDepartmentID
                                   select m).FirstOrDefault();

                    context.tblMedicalDepartments.Remove(medical);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwMedicalDepartments> IService1.GetMedicalDepartmentsDetail(int medicalDepartmentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwMedicalDepartments> list = new List<vwMedicalDepartments>();
                    list = (from x in context.vwMedicalDepartments
                            where x.MedicalDepartmentID == medicalDepartmentID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isMedicalDepartmentID(int medicalDepartmentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwMedicalDepartments
                                  where x.MedicalDepartmentID == medicalDepartmentID
                                  select x.MedicalDepartmentID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }



        #endregion

        #region Medical Staff
        List<vwMedicalStaff> IService1.GetAllMedicalStaff()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwMedicalStaff> list = new List<vwMedicalStaff>();
                    list = (from x in context.vwMedicalStaff select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwMedicalStaff IService1.AddMedicalStaff(vwMedicalStaff medicalStaff)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (medicalStaff.MedicalStaffID == 0)
                    {
                        tblMedicalStaff newMedicalStaff = new tblMedicalStaff();
                        newMedicalStaff.NameAndSurname = medicalStaff.NameAndSurname;
                        newMedicalStaff.MaleOrFemale = medicalStaff.MaleOrFemale;
                        newMedicalStaff.DateOfBirth = medicalStaff.DateOfBirth;
                        newMedicalStaff.Address = medicalStaff.Address;
                        newMedicalStaff.PhoneNumber = medicalStaff.PhoneNumber;
                        newMedicalStaff.NameOfJob = medicalStaff.NameOfJob;
                        newMedicalStaff.StatusOfEmployee = medicalStaff.StatusOfEmployee;
                        newMedicalStaff.BloodType = medicalStaff.BloodType;
                        newMedicalStaff.MedicalDepartmentID = medicalStaff.MedicalDepartmentID;

                        context.tblMedicalStaff.Add(newMedicalStaff);
                        context.SaveChanges();
                        medicalStaff.MedicalStaffID = newMedicalStaff.MedicalStaffID;
                        return medicalStaff;
                    }
                    else
                    {
                        tblMedicalStaff medicalStaffToEdit = (from s in context.tblMedicalStaff
                                                              where s.MedicalStaffID == medicalStaff.MedicalStaffID
                                                              select s).First();
                        medicalStaffToEdit.NameAndSurname = medicalStaff.NameAndSurname;
                        medicalStaffToEdit.MaleOrFemale = medicalStaff.MaleOrFemale;
                        medicalStaffToEdit.DateOfBirth = medicalStaff.DateOfBirth;
                        medicalStaffToEdit.Address = medicalStaff.Address;
                        medicalStaffToEdit.PhoneNumber = medicalStaff.PhoneNumber;
                        medicalStaffToEdit.NameOfJob = medicalStaff.NameOfJob;
                        medicalStaffToEdit.StatusOfEmployee = medicalStaff.StatusOfEmployee;
                        medicalStaffToEdit.BloodType = medicalStaff.BloodType;
                        medicalStaffToEdit.MedicalStaffID = medicalStaff.MedicalStaffID;
                        medicalStaffToEdit.MedicalDepartmentID = medicalStaff.MedicalDepartmentID;
                        context.SaveChanges();
                        return medicalStaff;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeleteMedicalStaff(int medicalStaffID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var medical = (from m in context.tblMedicalStaff
                                   where m.MedicalStaffID == medicalStaffID
                                   select m).FirstOrDefault();

                    context.tblMedicalStaff.Remove(medical);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwMedicalStaff> IService1.GetMedicalStaffsDetail(int medicalStaffID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwMedicalStaff> list = new List<vwMedicalStaff>();
                    list = (from x in context.vwMedicalStaff
                            where x.MedicalStaffID == medicalStaffID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        // Provera da li se kljuc izabranog Medical Staff-a nalazi u tabeli Login
        bool IService1.isMedicalStaffID(int medicalStaffID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwMedicalStaff
                                  where x.MedicalStaffID == medicalStaffID
                                  select x.MedicalStaffID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }

        #endregion

        #region Patients

        List<vwPatients> IService1.GetAllPatients()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwPatients> list = new List<vwPatients>();
                    list = (from x in context.vwPatients select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwPatients IService1.AddPatient(vwPatients patient)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (patient.PatientID == 0)
                    {
                        tblPatients newPatient = new tblPatients();
                        newPatient.NameAndSurname = patient.NameAndSurname;
                        newPatient.MaleOrFemale = patient.MaleOrFemale;
                        newPatient.DateOfBirth = patient.DateOfBirth;
                        newPatient.Address = patient.Address;
                        newPatient.PhoneNumberHome = patient.PhoneNumberHome;
                        newPatient.PhoneNumberMobile = patient.PhoneNumberMobile;
                        newPatient.PhoneNumberContactPerson = patient.PhoneNumberContactPerson;
                        newPatient.NumberOfIDCard = patient.NumberOfIDCard;
                        newPatient.BloodType = patient.BloodType;

                        context.tblPatients.Add(newPatient);
                        context.SaveChanges();
                        patient.PatientID = newPatient.PatientID;
                        return patient;
                    }
                    else
                    {
                        tblPatients patientToEdit = (from s in context.tblPatients
                                                     where s.PatientID == patient.PatientID
                                                     select s).First();
                        patientToEdit.NameAndSurname = patient.NameAndSurname;
                        patientToEdit.MaleOrFemale = patient.MaleOrFemale;
                        patientToEdit.DateOfBirth = patient.DateOfBirth;
                        patientToEdit.Address = patient.Address;
                        patientToEdit.PhoneNumberHome = patient.PhoneNumberHome;
                        patientToEdit.PhoneNumberContactPerson = patient.PhoneNumberContactPerson;
                        patientToEdit.NumberOfIDCard = patient.NumberOfIDCard;
                        patientToEdit.BloodType = patient.BloodType;
                        patientToEdit.PatientID = patient.PatientID;
                        context.SaveChanges();
                        return patient;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeletePatient(int patientID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblPatients
                                  where m.PatientID == patientID
                                  select m).FirstOrDefault();

                    context.tblPatients.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwPatients> IService1.GetPatientsDetail(int patientID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwPatients> list = new List<vwPatients>();
                    list = (from x in context.vwPatients
                            where x.PatientID == patientID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isPatientID(int patientID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwPatients
                                  where x.PatientID == patientID
                                  select x.PatientID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }

        #endregion

        #region Doctor Appointment

        List<vwDoctorAppointment> IService1.GetAllDoctorAppointment()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwDoctorAppointment> list = new List<vwDoctorAppointment>();
                    list = (from x in context.vwDoctorAppointment select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwDoctorAppointment IService1.AddDoctorAppointment(vwDoctorAppointment appointment)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (appointment.DoctorAppointmentID == 0)
                    {
                        tblDoctorAppointment newDoctorAppointment = new tblDoctorAppointment();
                        newDoctorAppointment.DateOfScheduling = appointment.DateOfScheduling;
                        newDoctorAppointment.MedicalDepartmentID = appointment.MedicalDepartmentID;
                        newDoctorAppointment.PatientID = appointment.PatientID;

                        context.tblDoctorAppointment.Add(newDoctorAppointment);
                        context.SaveChanges();
                        appointment.DoctorAppointmentID = newDoctorAppointment.DoctorAppointmentID;
                        return appointment;
                    }
                    else
                    {
                        tblDoctorAppointment appointmentToEdit = (from s in context.tblDoctorAppointment
                                                                  where s.DoctorAppointmentID == appointment.DoctorAppointmentID
                                                                  select s).First();
                        appointmentToEdit.DateOfScheduling = appointment.DateOfScheduling;
                        appointmentToEdit.MedicalDepartmentID = appointment.MedicalDepartmentID;
                        appointmentToEdit.PatientID = appointment.PatientID;
                        appointmentToEdit.DoctorAppointmentID = appointment.DoctorAppointmentID;
                        context.SaveChanges();
                        return appointment;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeleteDoctorAppointment(int doctorAppointmentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblDoctorAppointment
                                  where m.DoctorAppointmentID == doctorAppointmentID
                                  select m).FirstOrDefault();

                    context.tblDoctorAppointment.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwDoctorAppointment> IService1.GetDoctorAppointmentsDetail(int doctorAppointmentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwDoctorAppointment> list = new List<vwDoctorAppointment>();
                    list = (from x in context.vwDoctorAppointment
                            where x.DoctorAppointmentID == doctorAppointmentID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isDoctorAppointmentID(int doctorAppointmentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwDoctorAppointment
                                  where x.DoctorAppointmentID == doctorAppointmentID
                                  select x.DoctorAppointmentID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }

        #endregion

        #region Login

        List<vwLogin> IService1.GetAllLogins()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwLogin> list = new List<vwLogin>();
                    list = (from x in context.vwLogin select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwLogin IService1.AddLogin(vwLogin login)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (login.LoginID == 0)
                    {
                        tblLogin newLogin = new tblLogin();
                        newLogin.UserName = login.UserName;
                        newLogin.Password = login.Password;
                        newLogin.MedicalStaffID = login.MedicalStaffID;
                        newLogin.PatientID = login.PatientID;

                        context.tblLogin.Add(newLogin);
                        context.SaveChanges();
                        login.LoginID = newLogin.LoginID;
                        return login;
                    }
                    else
                    {
                        tblLogin loginToEdit = (from s in context.tblLogin
                                                where s.LoginID == login.LoginID
                                                select s).First();
                        loginToEdit.UserName = login.UserName;
                        loginToEdit.Password = login.Password;
                        loginToEdit.MedicalStaffID = login.MedicalStaffID;
                        loginToEdit.PatientID = login.PatientID;
                        context.SaveChanges();
                        return login;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeleteLogin(int loginID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblLogin
                                  where m.LoginID == loginID
                                  select m).FirstOrDefault();

                    context.tblLogin.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwLogin> IService1.GetLoginsDetail(int loginID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwLogin> list = new List<vwLogin>();
                    list = (from x in context.vwLogin
                            where x.LoginID == loginID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isLoginID(int loginID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwLogin
                                  where x.LoginID == loginID
                                  select x.LoginID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }

        bool IService1.LoginUserManagement(string userName, string password)
        {
            using (ClinicEntities1 context = new ClinicEntities1())
            {
                var q = (from m in context.tblLogin
                         where (m.UserName == userName) && (m.Password == password) && (m.MedicalStaffID == null) && (m.PatientID == null)
                         select m).FirstOrDefault();

                if (q != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        bool IService1.LoginUserPatient(string userName, string password)
        {
            using (ClinicEntities1 context = new ClinicEntities1())
            {
                var q = (from m in context.tblLogin
                         where (m.UserName == userName) && (m.Password == password) && (m.MedicalStaffID == null)
                         select m).FirstOrDefault();

                if (q != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool LoginUserMedicalStaff(string userName, string password)
        {
            using (ClinicEntities1 context = new ClinicEntities1())
            {
                var q = (from m in context.tblLogin
                         where (m.UserName == userName) && (m.Password == password) && (m.PatientID == null)
                         select m).FirstOrDefault();

                if (q != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        int IService1.maxPatientID()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwPatients
                                  orderby x.PatientID descending
                                  select x.PatientID).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return 0;
            }
        }

        int IService1.maxMedicalStaffID()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwMedicalStaff
                                  orderby x.MedicalStaffID descending
                                  select x.MedicalStaffID).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return 0;
            }
        }

        int IService1.currentPatientID(string userName, string password)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var result = (from x in context.tblLogin
                                  where (x.UserName == userName) && (x.Password == password) && (x.MedicalStaffID == null)
                                  select x.PatientID).FirstOrDefault();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return 0;
            }
        }

        int IService1.currentMedicalStaffID(string userName, string password)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var result = (from x in context.tblLogin
                                  where (x.UserName == userName) && (x.Password == password) && (x.PatientID == null)
                                  select x.MedicalStaffID).FirstOrDefault();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return 0;
            }
        }

        #endregion

        #region Pharmacy
        List<vwPharmacy> IService1.GetAllPharmacy()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwPharmacy> list = new List<vwPharmacy>();
                    list = (from x in context.vwPharmacy select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwPharmacy IService1.AddPharmacy(vwPharmacy pharmacy)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (pharmacy.PharmacyID == 0)
                    {
                        tblPharmacy newPharmacy = new tblPharmacy();
                        newPharmacy.NameOfTheDrug = pharmacy.NameOfTheDrug;
                        newPharmacy.ScopeOfApplication = pharmacy.ScopeOfApplication;
                        newPharmacy.QuantityOnCondition = pharmacy.QuantityOnCondition;
                        newPharmacy.Note = pharmacy.Note;
                        newPharmacy.ManufacturerID = pharmacy.ManufacturerID;

                        context.tblPharmacy.Add(newPharmacy);
                        context.SaveChanges();
                        pharmacy.PharmacyID = newPharmacy.PharmacyID;
                        return pharmacy;
                    }
                    else
                    {
                        tblPharmacy pharmacyToEdit = (from s in context.tblPharmacy
                                                      where s.PharmacyID == pharmacy.PharmacyID
                                                      select s).First();
                        pharmacyToEdit.NameOfTheDrug = pharmacy.NameOfTheDrug;
                        pharmacyToEdit.ScopeOfApplication = pharmacy.ScopeOfApplication;
                        pharmacyToEdit.QuantityOnCondition = pharmacy.QuantityOnCondition;
                        pharmacyToEdit.Note = pharmacy.Note;
                        pharmacyToEdit.ManufacturerID = pharmacy.ManufacturerID;
                        context.SaveChanges();
                        return pharmacy;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeletePharmacy(int pharmacyID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblPharmacy
                                  where m.PharmacyID == pharmacyID
                                  select m).FirstOrDefault();

                    context.tblPharmacy.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwPharmacy> IService1.GetPharmacyDetail(int pharmacyID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwPharmacy> list = new List<vwPharmacy>();
                    list = (from x in context.vwPharmacy
                            where x.PharmacyID == pharmacyID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isPharmacyID(int pharmacyID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwPharmacy
                                  where x.PharmacyID == pharmacyID
                                  select x.PharmacyID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }

        #endregion

        #region TakeMedication
        List<vwTakeMedication> IService1.GetAllTakeMedication()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwTakeMedication> list = new List<vwTakeMedication>();
                    list = (from x in context.vwTakeMedication select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwTakeMedication IService1.AddTakeMedication(vwTakeMedication takeMedication)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (takeMedication.TakeMedicationID == 0)
                    {
                        tblTakeMedication newTakeMedication = new tblTakeMedication();
                        newTakeMedication.DateOfTaking = takeMedication.DateOfTaking;
                        newTakeMedication.QuantityOfTheDrug = takeMedication.QuantityOfTheDrug;
                        newTakeMedication.PharmacyID = takeMedication.PharmacyID;
                        newTakeMedication.MedicalStaffID = takeMedication.MedicalStaffID;
                        newTakeMedication.PatientID = takeMedication.PatientID;

                        context.tblTakeMedication.Add(newTakeMedication);
                        context.SaveChanges();
                        takeMedication.TakeMedicationID = newTakeMedication.TakeMedicationID;
                        return takeMedication;
                    }
                    else
                    {
                        tblTakeMedication takeMedicationToEdit = (from s in context.tblTakeMedication
                                                                  where s.TakeMedicationID == takeMedication.TakeMedicationID
                                                                  select s).First();
                        takeMedicationToEdit.DateOfTaking = takeMedication.DateOfTaking;
                        takeMedicationToEdit.QuantityOfTheDrug = takeMedication.QuantityOfTheDrug;
                        takeMedicationToEdit.PharmacyID = takeMedication.PharmacyID;
                        takeMedicationToEdit.MedicalStaffID = takeMedication.MedicalStaffID;
                        takeMedicationToEdit.PatientID = takeMedication.PatientID;
                        context.SaveChanges();
                        return takeMedication;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeleteTakeMedication(int takeMedicationID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblTakeMedication
                                  where m.TakeMedicationID == takeMedicationID
                                  select m).FirstOrDefault();

                    context.tblTakeMedication.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwTakeMedication> IService1.GetTakeMedicationDetail(int takeMedicationID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwTakeMedication> list = new List<vwTakeMedication>();
                    list = (from x in context.vwTakeMedication
                            where x.TakeMedicationID == takeMedicationID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isTakeMedicationID(int takeMedicationID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwTakeMedication
                                  where x.TakeMedicationID == takeMedicationID
                                  select x.TakeMedicationID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }
        #endregion

        #region PharmaceuticalManufacturing
        List<vwPharmaceuticalManufacturing> IService1.GetAllPharmaceuticalManufacturing()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwPharmaceuticalManufacturing> list = new List<vwPharmaceuticalManufacturing>();
                    list = (from x in context.vwPharmaceuticalManufacturing select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwPharmaceuticalManufacturing IService1.AddPharmaceuticalManufacturing(vwPharmaceuticalManufacturing pharmaceuticalManufacturing)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (pharmaceuticalManufacturing.ManufacturerID == 0)
                    {
                        tblPharmaceuticalManufacturing newPharmaceuticalManufacturing = new tblPharmaceuticalManufacturing();
                        newPharmaceuticalManufacturing.ManufacturerName = pharmaceuticalManufacturing.ManufacturerName;
                        newPharmaceuticalManufacturing.AddressOfManufacturer = pharmaceuticalManufacturing.AddressOfManufacturer;
                        newPharmaceuticalManufacturing.PhoneNumberOfManufacturer = pharmaceuticalManufacturing.PhoneNumberOfManufacturer;
                        newPharmaceuticalManufacturing.EmailOfManufacturer = pharmaceuticalManufacturing.EmailOfManufacturer;
                        newPharmaceuticalManufacturing.Note = pharmaceuticalManufacturing.Note;

                        context.tblPharmaceuticalManufacturing.Add(newPharmaceuticalManufacturing);
                        context.SaveChanges();
                        pharmaceuticalManufacturing.ManufacturerID = newPharmaceuticalManufacturing.ManufacturerID;
                        return pharmaceuticalManufacturing;
                    }
                    else
                    {
                        tblPharmaceuticalManufacturing takeMedicationToEdit = (from s in context.tblPharmaceuticalManufacturing
                                                                               where s.ManufacturerID == pharmaceuticalManufacturing.ManufacturerID
                                                                               select s).First();
                        takeMedicationToEdit.ManufacturerName = pharmaceuticalManufacturing.ManufacturerName;
                        takeMedicationToEdit.AddressOfManufacturer = pharmaceuticalManufacturing.AddressOfManufacturer;
                        takeMedicationToEdit.PhoneNumberOfManufacturer = pharmaceuticalManufacturing.PhoneNumberOfManufacturer;
                        takeMedicationToEdit.EmailOfManufacturer = pharmaceuticalManufacturing.EmailOfManufacturer;
                        takeMedicationToEdit.Note = pharmaceuticalManufacturing.Note;
                        context.SaveChanges();
                        return pharmaceuticalManufacturing;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeletePharmaceuticalManufacturing(int manufacturerID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblPharmaceuticalManufacturing
                                  where m.ManufacturerID == manufacturerID
                                  select m).FirstOrDefault();

                    context.tblPharmaceuticalManufacturing.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwPharmaceuticalManufacturing> IService1.GetPharmaceuticalManufacturingDetail(int manufacturerID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwPharmaceuticalManufacturing> list = new List<vwPharmaceuticalManufacturing>();
                    list = (from x in context.vwPharmaceuticalManufacturing
                            where x.ManufacturerID == manufacturerID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isManufacturerID(int manufacurerID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwPharmaceuticalManufacturing
                                  where x.ManufacturerID == manufacurerID
                                  select x.ManufacturerID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }
        #endregion

        #region LoginCurrent
        List<vwLoginCurrent> IService1.GetAllLoginsCurrent()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwLoginCurrent> list = new List<vwLoginCurrent>();
                    list = (from x in context.vwLoginCurrent select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        vwLoginCurrent IService1.AddLoginCurrent(vwLoginCurrent loginCurrent)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    if (loginCurrent.LoginCurrentID == 0)
                    {
                        tblLoginCurrent newLoginCurrent = new tblLoginCurrent();
                        newLoginCurrent.PatientID = loginCurrent.PatientID;
                        newLoginCurrent.MedicalStaffID = loginCurrent.MedicalStaffID;

                        context.tblLoginCurrent.Add(newLoginCurrent);
                        context.SaveChanges();
                        loginCurrent.LoginCurrentID = newLoginCurrent.LoginCurrentID;
                        return loginCurrent;
                    }
                    else
                    {
                        tblLoginCurrent loginCurrentToEdit = (from s in context.tblLoginCurrent
                                                              where s.LoginCurrentID == loginCurrent.LoginCurrentID
                                                              select s).First();
                        loginCurrentToEdit.PatientID = loginCurrent.PatientID;
                        loginCurrentToEdit.MedicalStaffID = loginCurrent.MedicalStaffID;
                        context.SaveChanges();
                        return loginCurrent;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        void IService1.DeleteLoginCurrent(int loginCurrentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var delete = (from m in context.tblLoginCurrent
                                  where m.LoginCurrentID == loginCurrentID
                                  select m).FirstOrDefault();

                    context.tblLoginCurrent.Remove(delete);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        List<vwLoginCurrent> IService1.GetLoginsCurrentDetail(int loginCurrentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    List<vwLoginCurrent> list = new List<vwLoginCurrent>();
                    list = (from x in context.vwLoginCurrent
                            where x.LoginCurrentID == loginCurrentID
                            select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        bool IService1.isLoginCurrentID(int loginCurrentID)
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    int result = (from x in context.vwLoginCurrent
                                  where x.LoginCurrentID == loginCurrentID
                                  select x.LoginCurrentID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return false;
            }
        }

        int IService1.maxCurrentPatientID()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var result = (from x in context.tblLoginCurrent
                                  orderby x.LoginCurrentID descending
                                  select x.PatientID).FirstOrDefault();

                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return 0;
            }
        }

        int IService1.maxCurrentMedicalStaffID()
        {
            try
            {
                using (ClinicEntities1 context = new ClinicEntities1())
                {
                    var result = (from x in context.tblLoginCurrent
                                  orderby x.LoginCurrentID descending
                                  select x.MedicalStaffID).FirstOrDefault();

                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return 0;
            }
        }
        #endregion

    }
}


