using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Internship_Template.Models.Entity;

namespace Internship_Template.Common
{
    /// <summary>
    /// ログイン認証フィルター
    /// </summary>
    public class LoginFilter : FilterAttribute, IAuthorizationFilter
    {

        /// <summary>
        /// 認証処理
        /// </summary>
        /// <param name="filterContext"></param>
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {

            DPContext _dbDP = new DPContext();
            //セッション内容の取得
            HttpContextBase httpContext = filterContext.HttpContext;
            string debugMode = Internship_Util.GetValue<string>("DebugMode");
            //セッションが生成されるのはログイン画面を経由した場合のみ
            //if (httpContext.Session[M_SESSION.SessionKey] is T_USER)
            if (httpContext.Session[M_SESSION.SessionKey] is USER)
                {
                return;
            }
            else if(debugMode == "Admin")
            {
                //田中太郎固定でセッション作成
                Internship_Context db = new Internship_Context();
                //T_USER admin = db.T_USER.Where(e => e.ID == "00001").FirstOrDefault();
                USER admin = _dbDP.USER.Where(e => e.USER_ID == "1").FirstOrDefault();
                httpContext.Session[M_SESSION.SessionKey] = admin;

                return;
            }
            else
            {
                object controllerName = "Login";
                RouteData currentRoute = filterContext.RequestContext.RouteData;

                /*TODO: 改善の余地あり。今の作りだと{controller}{action}{id}のいずれかにLoginがあるとLoginControllerに飛んでしまう。
                        あと直でmodel渡された場合も侵入できてしまうから何とかする。*/
                if (currentRoute.Values.ContainsValue(controllerName))
                {
                    return;
                }
                else
                {
                    //ユーザーをログイン画面にリダイレクトします  
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                             { "controller", "Login" },
                             { "action", "Index" }
                        });
                }
            }


        }
    }
}