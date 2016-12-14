using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Tool
{
 public   class NMsg
 {
     public static void AlertAndRedirect(string msg, string url)
     {


         HttpContext.Current.Response.Write(string.Format("<script type='text/javascript'>alert('{0}');window.location.href='{1}'; </script>", msg, url));
         // HttpContext.Current.Response.Redirect(url);
         HttpContext.Current.ApplicationInstance.CompleteRequest();

     }
     public static void Redirect(string url)
     {
         HttpContext.Current.Response.Redirect(url);
         HttpContext.Current.ApplicationInstance.CompleteRequest();
     }
     public static void Alert(string msg)
     {


         HttpContext.Current.Response.Write(string.Format("<script type='text/javascript'>alert('{0}'); </script>", msg));
         // HttpContext.Current.Response.Redirect(url);
         HttpContext.Current.ApplicationInstance.CompleteRequest();

     }
     public static string SetMsg(string msg, string msgcode)
     {


         return ("{\"msg\":\"" + msg + "\",\"msgcode\":\"" + msgcode + "\"}");


     }
    }
}
