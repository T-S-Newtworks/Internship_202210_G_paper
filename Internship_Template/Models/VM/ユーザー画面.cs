using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;

namespace Internship_Template.Models.VM
{
    public class ユーザー画面 :_BaseViewModel
    {
        /// <summary>
        /// メンバー一覧
        /// </summary>
        public List<T_USER> Users { get; set; }

        /// <summary>
        /// 対象メンバ(登録、更新、削除など)
        /// </summary>
        public T_USER TargetUser { get; set; }

    }
}