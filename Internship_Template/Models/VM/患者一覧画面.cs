using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class 患者一覧画面 :_BaseViewModel
    {
        /// <summary>
        /// メンバー一覧
        /// </summary>
        public List<T_PATIENT> Patients { get; set; }

        public T_PATIENT TargetPatient { get; set; }

        //public T_DEPARTMENT DoctorDepartment { get; set; }

        //public T_HOSPITAL DoctorHospital { get; set; }
    }
}