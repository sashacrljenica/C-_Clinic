//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFClinic
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDoctorAppointment
    {
        public int DoctorAppointmentID { get; set; }
        public Nullable<System.DateTime> DateOfScheduling { get; set; }
        public Nullable<int> MedicalDepartmentID { get; set; }
        public Nullable<int> PatientID { get; set; }
    
        public virtual tblMedicalDepartments tblMedicalDepartments { get; set; }
        public virtual tblPatients tblPatients { get; set; }
    }
}
