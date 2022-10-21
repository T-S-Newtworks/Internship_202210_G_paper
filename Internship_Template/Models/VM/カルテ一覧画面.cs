using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class カルテ一覧画面 :_BaseViewModel
    {
        /// <summary>
        /// カルテ一覧
        /// </summary>
        public List<T_CHART> Chart{ get; set; }

        public List<T_DOCTOR> Doctor{ get; set; }

        public List<T_PATIENT> Patient { get; set; }

        public List<T_HOSPITAL> Hospital { get; set; }
        public T_CHART TargetChart { get; set; }
    }
}