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
    
    public partial class tblLogin
    {
        public int LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> MedicalStaffID { get; set; }
        public Nullable<int> PatientID { get; set; }
    
        public virtual tblMedicalStaff tblMedicalStaff { get; set; }
        public virtual tblPatients tblPatients { get; set; }
    }
}
