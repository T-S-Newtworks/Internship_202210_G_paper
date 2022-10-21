using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class 先生一覧画面 :_BaseViewModel
    {
        /// <summary>
        /// メンバー一覧
        /// </summary>
        public List<T_DOCTOR> Doctors { get; set; }

        public T_DOCTOR TargetDoctor { get; set; }

        public T_DEPARTMENT DoctorDepartment { get; set; }

        public T_HOSPITAL DoctorHospital { get; set; }

        //public List<T_DEPARTMENT> Departments { get; set; }

        //public List<SelectListItem> DepartmentsList { get; set; }

        //public List<T_HOSPITAL> Hospitals { get; set; }
        //public List<SelectListItem> HospitalsList { get; set; }


    }
}