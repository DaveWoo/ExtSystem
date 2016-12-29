using System;
using System.Collections.Generic;
using System.Web;
using DAL;
using Tool;

namespace BLL
{
	public class DB_User : NSPMsSql<NModel.DB_User>
	{
		public bool EditPW(string where_user_nameOrEmail, string edit_PassWord)
		{
			edit_PassWord = NMd5.GetMd5Hash(edit_PassWord);

			string execSql = string.Format("Exec  {0}_EditPW @Name=N'{1}',@PassWord='{2}'   ", base.ETblName, where_user_nameOrEmail, edit_PassWord);

			return base.DUI(execSql) > 0 ? true : false;
		}

		public NModel.DB_User ExistsNamePw(string nameOrEmail, string pw)
		{
			string execSql = string.Format("Exec  {0}_ExistsNamePw @Name=N'{1}',@PW=N'{2}'  ", base.ETblName, nameOrEmail, pw);
			List<NModel.DB_User> nm = base.GetList(execSql);
			if (nm != null && nm.Count > 0) { return nm[0]; }
			return null;
		}

		public bool CheckLogin(string nameOrEmail, string pw, string code, bool isCueStatus = false)
		{
			// if (NTool.IsSessionEquals(NModel.EnObject.UserCodeSessionName, code))
			{
				NModel.DB_User cModel_User = this.ExistsNamePw((nameOrEmail + "").ToLower(), NMd5.GetMd5Hash(pw).ToLower());
				if (cModel_User != null && cModel_User.User_ID > 0)
				{
					HttpContext.Current.Session[NModel.EnObject.LoginUserSessionName] = cModel_User.User_Name;
					HttpContext.Current.Session[NModel.EnObject.CurrentLoginUserSessionName] = cModel_User;

					if (isCueStatus)
					{
						BLL.DB_Session cModel = new DB_Session();
						NModel.DB_Session cModel_Session = new NModel.DB_Session();
						cModel_Session.Session_DB_Name = base.TableName;
						cModel_Session.Session_UserID = cModel_User.User_ID;

						cModel_Session.Session_CID = (HttpContext.Current.Session.SessionID + "");
						cModel_Session.Session_Status = 1;
						cModel_Session.Session_EndTime = DateTime.Parse(DateTime.Now.AddHours(60).ToString("s"));
						///DateTime.Parse(DateTime.Now.AddHours(60).ToString("yyyy-MM-dd hh:mm:ss"));
						cModel_Session.Session_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));

						if (cModel.Add(cModel_Session) > 0)
						{
							Tool.NTool.AddCookie(base.TableName, cModel_Session.Session_CID + "_" + cModel_User.User_ID, 60);

							cModel.Close();
							return true;
						};

						cModel.Close();
					}
					else
					{
						return true;
					}
				}
			}

			return false;
		}

		public bool ExistsName(string name)
		{
			string execSql = string.Format("Exec  {0}_ExistsName @Name=N'{1}' ", base.ETblName, name);

			int num = 0;
			int.TryParse(base.GetObject(execSql) + "", out num);

			return num > 0 ? true : false;
		}

		public bool ExistsEmail(string email)
		{
			string execSql = string.Format("Exec  {0}_ExistsEmail @Email=N'{1}' ", base.ETblName, email);

			int num = 0;
			int.TryParse(base.GetObject(execSql) + "", out num);

			return num > 0 ? true : false;
		}
	}
}