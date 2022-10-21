using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internship_Template.Models.Entity;


namespace Internship_Template.Controllers
{

    /// <summary>
    /// 基底クラス。DB接続とかセッション情報はどのコントローラでも用いるのでここで管理。
    /// </summary>
    /// <remarks>他共通して用いるものがある場合は適宜ここに追加。</remarks>
    public class _BaseController : Controller
    {

        protected Internship_Context _db = new Internship_Context();
        protected DPContext _dbDP = new DPContext();

        public new USER DPUser { 
        get
            {
                if (this.Session != null && this.Session[M_SESSION.SessionKey] != null)
                {
            //return (T_USER)this.Session[M_SESSION.SessionKey];
            return (USER)this.Session[M_SESSION.SessionKey];
        }
                else
                {
            return null;
        }

        }
    }
    public new T_USER User
        {
            get
            {
                if (this.Session != null && this.Session[M_SESSION.SessionKey] != null)
                {
                    //return (T_USER)this.Session[M_SESSION.SessionKey];
                    return (T_USER)this.Session[M_SESSION.SessionKey];
                }
                else
                {
                    return null;
                }

            }
        }

    }
}