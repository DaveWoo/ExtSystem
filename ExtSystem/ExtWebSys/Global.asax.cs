using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Tool;

namespace ExtWebSys
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {
           

            try
            {//sql 防注入
                string error_page = "/error/err_404.html?";
                string getkeys = "";
                //string sqlErrorPage = System.Configuration.ConfigurationSettings.AppSettings["CustomErrorPage"].ToString();
                if (System.Web.HttpContext.Current.Request.QueryString != null)
                {
                    if (System.Web.HttpContext.Current.Request.QueryString.Count > 400)
                    {
                        System.Web.HttpContext.Current.Response.Redirect(error_page);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        return;

                    }

                    for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.QueryString.Keys[i];
                        string val = System.Web.HttpContext.Current.Request.QueryString[getkeys];
                        if (!Tool.NFTool.ProcessSqlStr(val, 0))
                        {


                            //System.Web.HttpContext.Current.Response.Redirect (sqlErrorPage+"?errmsg=sqlserver&sqlprocess=true");
                            System.Web.HttpContext.Current.Response.Redirect(error_page);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();

                        }
                    }
                }
                if (System.Web.HttpContext.Current.Request.Form != null)
                {
                    if (System.Web.HttpContext.Current.Request.Form.Count > 3200)
                    {
                        System.Web.HttpContext.Current.Response.Redirect(error_page);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        return;

                    }


                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.Form.Keys[i];
                        string val = System.Web.HttpContext.Current.Request.Form[getkeys];
                        if (!Tool.NFTool.ProcessSqlStr(val, 1))
                        {
                            //System.Web.HttpContext.Current.Response.Redirect (sqlErrorPage+"?errmsg=sqlserver&sqlprocess=true");
                            System.Web.HttpContext.Current.Response.Redirect(error_page);
                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误处理: 处理用户提交信息!
            }
        }
    }
}