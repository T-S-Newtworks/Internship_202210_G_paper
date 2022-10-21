using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;

namespace Internship_Template.Models.VM
{
    public class ログイン画面 :_BaseViewModel
    {
        /// <summary>
        /// メンバー一覧
        /// </summary>
        public T_LOGIN Login { get; set; }

        public USER User { get; set; }


    }
}