using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;

using System.Web.SessionState;

namespace NModel
{
   public class EnObject
    {
       public static List<Color> listColor = new List<Color>()
       {
        Color.FromArgb(76,147,235),
        Color.FromArgb(240,248,255),
        Color.FromArgb(250,235,215),
        Color.FromArgb(0,255,255),
        Color.FromArgb(127,255,212),
        Color.FromArgb(240,255,255),
        Color.FromArgb(245,245,220),
        Color.FromArgb(255,228,196),
        Color.FromArgb(255,255,205),
        Color.FromArgb(0,0,255),
        Color.FromArgb(165,42,42),
        Color.FromArgb(222,184,135),
        Color.FromArgb(127,255,0),
        Color.FromArgb(210,105,30),
        Color.FromArgb(255,127,80),
        Color.FromArgb(100,149,237),
        Color.FromArgb(255,248,220),
        Color.FromArgb(220,20,60)
       

      
       
       };
       /// 显示类型
       /// </summary>
       public enum EDisplayType { eHtml, eText, eXml, eJs, eCss, eDict, eArray, eJson }
       public static string CaptchaSessionName { set; get; }
       public static string CurrentLoginSessionName { get { return "LoginSession"; } }
       public static string CurrentLoginUserSessionName { get { return "LoginUserSession"; } }
       public static string LoginSessionName { get { return "LoginSessionName"; } }

       public static string UserCodeSessionName { get { return "UserCodeSession"; } }

       public static string TimeSessionName { get { return "TimeSession"; } }
       public static string EmailSessionName { get { return "EmailSession"; } }

       public static string FindUserSessionName { get { return "FindUserSession"; } }
      /// <summary>
      /// 
      /// </summary>
       public static object GetFindUserValue { get { return HttpContext.Current.Session[FindUserSessionName]; } }
       public static object SetFindUserValue { set { HttpContext.Current.Session[FindUserSessionName] = value; } }


       public static object GetEmailValue { get { return HttpContext.Current.Session[EmailSessionName]; } }
       public static object SetEmailValue { set { HttpContext.Current.Session[EmailSessionName] = value; } }
       public static object SetTimeValue { set { HttpContext.Current.Session[TimeSessionName] = value; } }
       public static object GetTimeValue { get { return HttpContext.Current.Session[TimeSessionName]; } }
       public static string  GetCodeValue {  get{return HttpContext.Current.Session[UserCodeSessionName]+"";}}

       public static string SetCodeValue { set { HttpContext.Current.Session[UserCodeSessionName] = value; } }


       public static string LoginUserSessionName { get { return "LoginUserSessionName"; } }
       public enum OperateMothedType { CmdOp, GroupOp, EnOp, CmdOpGet };
       public enum Operate { Read, Edit, Del, Add,SQLTran, All };
       public enum Results {eNum,eBoolean}

       public static string[] AbcStr = {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R",
                                 "S","T","U","V","W","X","Y","Z"};
                                


    }
}
