using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class _BaseViewModel
    {
        public _BaseViewModel()
        {
            Messages = new Dictionary<string, string>();
        }

        /// <summary>
        /// ログインユーザ情報
        /// </summary>
        public T_USER LoginedUser { get; set; }

        public USER DPUser { get; set; }

        /// <summary>
        /// メッセージ(汎用)
        /// </summary>
        public Dictionary<string, string> Messages { get; set; }
    }
}