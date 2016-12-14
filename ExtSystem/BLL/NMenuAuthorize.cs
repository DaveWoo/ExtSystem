using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BLL
{
    public class NMenuAuthorize : AuthorizeAttribute
    {
        public long MenuToId { get; set; }

        public static bool Get_model_user(long? id)
        {


            NModel.Admin_User _rlAndUsModel = HttpContext.Current.Session[NModel.EnObject.CurrentLoginSessionName] as NModel.Admin_User;
          
            
            if (_rlAndUsModel != null && !string.IsNullOrEmpty(_rlAndUsModel.Role_Menu))
            {
                string[] arrStr = _rlAndUsModel.Role_Menu.Split(',');
                long[] Arr = Tool.NTool.ArrayToAll<long, string>(arrStr);
                int i = Arr.Where(a => a.Equals(id)).ToList().Count;
                if (i > 0)
                    return true;
            }


            return false;

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            bool tre = Get_model_user(this.MenuToId);

            if (!tre)
            {


                System.Web.HttpContext.Current.Response.Redirect("~/lcq/login");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }

        }
    }
}
