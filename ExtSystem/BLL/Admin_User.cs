using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Tool;
using System.Web;

namespace BLL
{
   public class Admin_User:NSPMsSql<NModel.Admin_User>
    {
        public NModel.Admin_User GetNameModel(string name)
        {

            string execSql = string.Format("Exec  {0}_GetNameModel @Name=N'{1}'  ", base.ETblName, name);
          List<NModel.Admin_User>  nm=   base.GetList(execSql);
            if(nm!=null&&nm.Count>0){  return nm[0];}
            return null;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="where_user_name">修改那个用户名</param>
        /// <param name="edit_PassWord">修改的密码</param>
        /// <returns></returns>
        public bool EditPW(string where_user_name, string edit_PassWord)
        {
            edit_PassWord = NMd5.GetMd5Hash(edit_PassWord);

            string execSql = string.Format("Exec  {0}_EditPW @Name=N'{1}',@PassWord='{2}'   ", base.ETblName, where_user_name, edit_PassWord);

            return base.DUI(execSql) > 0 ? true : false;

        }

        public NModel.Admin_User ExistsNamePw(string name, string pw)
        {

            string execSql = string.Format("Exec  {0}_ExistsNamePw @Name=N'{1}',@PW=N'{2}'  ", base.ETblName, name, pw);
            List<NModel.Admin_User> nm = base.GetList(execSql);
            if (nm != null && nm.Count > 0) { return nm[0]; }
            return null;


        }
        public bool CheckLogin(string name, string pw, string code)
        {
            if (NTool.IsSessionEquals(NModel.EnObject.CaptchaSessionName, code))
            {
                NCaptcha.Generate(true);

                NModel.Admin_User cModel_User = this.ExistsNamePw((name + "").ToLower(), NMd5.GetMd5Hash(pw).ToLower());
                if (cModel_User != null && cModel_User.User_ID > 0)
                {
                    HttpContext.Current.Session[NModel.EnObject.LoginSessionName] = cModel_User.User_Name;
                    HttpContext.Current.Session[NModel.EnObject.CurrentLoginSessionName] = cModel_User;

                    
                    
                    BLL.DB_Session cModel = new DB_Session();
                    NModel.DB_Session cModel_Session = new NModel.DB_Session();
                    cModel_Session.Session_DB_Name = base.TableName;
                    cModel_Session.Session_UserID = cModel_User.User_ID;

                    cModel_Session.Session_CID = (HttpContext.Current.Session.SessionID + "");
                    cModel_Session.Session_Status = 1;
                    cModel_Session.Session_EndTime = DateTime.Parse(DateTime.Now.AddHours(60).ToString("s"));
                    ///DateTime.Parse(DateTime.Now.AddHours(60).ToString("yyyy-MM-dd hh:mm:ss"));
                    cModel_Session.Session_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                  
                    if (cModel.Add(cModel_Session)>0)
                    {
                        cModel.Close();

                        Tool.NTool.AddCookie(base.TableName, cModel_Session.Session_CID + "_" + cModel_User.User_ID, 60);
                        return true;

                    };
                    cModel.Close();

                }


            }



            return false;
        }


    }
}
