using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFClinic
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region Medical Departments

        [OperationContract]
        List<vwMedicalDepartments> GetAllMedicalDepartments();

        [OperationContract]
        vwMedicalDepartments AddMedicalDepartments(vwMedicalDepartments medicalDepartment);

        [OperationContract]
        void DeleteMedicalDepartment(int medicalDepartmentID);

        [OperationContract]
        List<vwMedicalDepartments> GetMedicalDepartmentsDetail(int medicalDepartmentID);

        [OperationContract]
        bool isMedicalDepartmentID(int medicalDepartmentID);

        #endregion

        #region Medical Staff

        [OperationContract]
        List<vwMedicalStaff> GetAllMedicalStaff();

        [OperationContract]
        vwMedicalStaff AddMedicalStaff(vwMedicalStaff medicalStaff);

        [OperationContract]
        void DeleteMedicalStaff(int medicalStaffID);

        [OperationContract]
        List<vwMedicalStaff> GetMedicalStaffsDetail(int medicalStaffID);

        [OperationContract]
        bool isMedicalStaffID(int medicalStaffID);

        #endregion

        #region Patients

        [OperationContract]
        List<vwPatients> GetAllPatients();

        [OperationContract]
        vwPatients AddPatient(vwPatients patient);

        [OperationContract]
        void DeletePatient(int patientID);

        [OperationContract]
        List<vwPatients> GetPatientsDetail(int patientID);

        [OperationContract]
        bool isPatientID(int patientID);

        #endregion

        #region Doctor Appointment

        [OperationContract]
        List<vwDoctorAppointment> GetAllDoctorAppointment();

        [OperationContract]
        vwDoctorAppointment AddDoctorAppointment(vwDoctorAppointment appointment);

        [OperationContract]
        void DeleteDoctorAppointment(int doctorAppointmentID);

        [OperationContract]
        List<vwDoctorAppointment> GetDoctorAppointmentsDetail(int doctorAppointmentID);

        [OperationContract]
        bool isDoctorAppointmentID(int doctorAppointmentID);

        #endregion

        #region Login

        [OperationContract]
        List<vwLogin> GetAllLogins();

        [OperationContract]
        vwLogin AddLogin(vwLogin login);

        [OperationContract]
        void DeleteLogin(int loginID);

        [OperationContract]
        List<vwLogin> GetLoginsDetail(int loginID);

        [OperationContract]
        bool isLoginID(int loginID);

        [OperationContract]
        bool LoginUserManagement(string userName, string password);

        [OperationContract]
        bool LoginUserPatient(string userName, string password);

        [OperationContract]
        bool LoginUserMedicalStaff(string userName, string password);

        [OperationContract]
        int maxPatientID();

        [OperationContract]
        int maxMedicalStaffID();

        [OperationContract]
        int currentPatientID(string username, string password);

        [OperationContract]
        int currentMedicalStaffID(string username, string password);
        #endregion

        #region Pharmacy
        [OperationContract]
        List<vwPharmacy> GetAllPharmacy();

        [OperationContract]
        vwPharmacy AddPharmacy(vwPharmacy pharmacy);

        [OperationContract]
        void DeletePharmacy(int pharmacyID);

        [OperationContract]
        List<vwPharmacy> GetPharmacyDetail(int pharmacyID);

        [OperationContract]
        bool isPharmacyID(int pharmacyID);
        #endregion

        #region TakeMedication
        [OperationContract]
        List<vwTakeMedication> GetAllTakeMedication();

        [OperationContract]
        vwTakeMedication AddTakeMedication(vwTakeMedication takeMedication);

        [OperationContract]
        void DeleteTakeMedication(int takeMedicationID);

        [OperationContract]
        List<vwTakeMedication> GetTakeMedicationDetail(int takeMedicationID);

        [OperationContract]
        bool isTakeMedicationID(int takeMedicationID);
        #endregion

        #region PharmaceuticalManufacturing
        [OperationContract]
        List<vwPharmaceuticalManufacturing> GetAllPharmaceuticalManufacturing();

        [OperationContract]
        vwPharmaceuticalManufacturing AddPharmaceuticalManufacturing(vwPharmaceuticalManufacturing pharmaceuticalManufacturing);

        [OperationContract]
        void DeletePharmaceuticalManufacturing(int manufacturerID);

        [OperationContract]
        List<vwPharmaceuticalManufacturing> GetPharmaceuticalManufacturingDetail(int manufacturerID);

        [OperationContract]
        bool isManufacturerID(int manufacurerID);
        #endregion

        #region LoginCurrent

        [OperationContract]
        List<vwLoginCurrent> GetAllLoginsCurrent();

        [OperationContract]
        vwLoginCurrent AddLoginCurrent(vwLoginCurrent loginCurrent);

        [OperationContract]
        void DeleteLoginCurrent(int loginCurrentID);

        [OperationContract]
        List<vwLoginCurrent> GetLoginsCurrentDetail(int loginCurrentID);

        [OperationContract]
        bool isLoginCurrentID(int loginCurrentID);

        [OperationContract]
        int maxCurrentPatientID();

        [OperationContract]
        int maxCurrentMedicalStaffID();
        #endregion


        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
