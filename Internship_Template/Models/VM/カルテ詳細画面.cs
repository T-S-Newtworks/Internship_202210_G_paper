using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class カルテ詳細画面 :_BaseViewModel
    {
        /// <summary>
        /// カルテ一覧
        /// </summary>
        public T_CHART Chart{ get; set; }

        public T_DOCTOR Doctor{ get; set; }

        public T_PATIENT Patient { get; set; }

        public T_HOSPITAL Hospital { get; set; }
    }
}