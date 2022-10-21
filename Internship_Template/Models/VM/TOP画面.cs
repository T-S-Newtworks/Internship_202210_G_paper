using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class TOP画面 :_BaseViewModel
    {
        /// <summary>
        /// メンバー一覧
        /// </summary>
        public List<T_USER> Users { get; set; }

        public List<T_DEPARTMENT> Departments { get; set; }

        public List<SelectListItem> DepartmentsList { get; set; }

        public List<T_HOSPITAL> Hospitals { get; set; }

        public List<SelectListItem> HospitalsList { get; set; }

        public List<T_CHART> Charts { get; set; }

        public List<T_PATIENT> Patients { get; set; }

        public List<T_DOCTOR> Doctors { get; set; }

    }
}