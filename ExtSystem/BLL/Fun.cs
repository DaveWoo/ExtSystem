using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

namespace BLL
{
	public class Fun
	{
		public static NModel.Admin_User Current_Login_Admin_User()
		{
			NModel.Admin_User nAdmin_User = HttpContext.Current.Session[NModel.EnObject.CurrentLoginSessionName] as NModel.Admin_User;

			return nAdmin_User;
		}

		public static NModel.DB_User Current_Login_DB_User()
		{
			NModel.DB_User nAdmin_User = HttpContext.Current.Session[NModel.EnObject.CurrentLoginUserSessionName] as NModel.DB_User;

			return nAdmin_User;
		}

		public static string admin_login_ck_name = "Admin_User";

		public static byte Auto_Login_Admin()
		{
			String admin_User_Value = Tool.NTool.GetCookieValue(admin_login_ck_name);

			string[] arr = admin_User_Value.Split('_');

			if (Tool.NStr.hasSessionValue(NModel.EnObject.LoginSessionName))
			{
				return 1;
			}
			else if (Tool.NTool.IsArrEqualLen<string>(arr, 2))
			{
				BLL.DB_Session bll_Session = new BLL.DB_Session();
				long User_ID = 0;
				long.TryParse(arr[1], out User_ID);
				bool has = bll_Session.Exists_CID_UserID(arr[0], User_ID);
				if (has)
				{
					BLL.Admin_User bll = new BLL.Admin_User();
					HttpContext.Current.Session[NModel.EnObject.CurrentLoginSessionName] = bll.GetModel(User_ID);
					bll.Close();
					return 2;
				}
			}

			return 0;
		}

		public static string User_login_ck_name = "DB_User";

		public static byte Auto_Login_User()
		{
			String admin_User_Value = Tool.NTool.GetCookieValue(User_login_ck_name);

			string[] arr = admin_User_Value.Split('_');

			if (Tool.NStr.hasSessionValue(NModel.EnObject.CurrentLoginUserSessionName))
			{
				return 1;
			}
			else if (Tool.NTool.IsArrEqualLen<string>(arr, 2))
			{
				BLL.DB_Session bll_Session = new BLL.DB_Session();
				long User_ID = 0;
				long.TryParse(arr[1], out User_ID);
				bool has = bll_Session.Exists_CID_UserID(arr[0], User_ID);
				if (has)
				{
					BLL.DB_User bll = new BLL.DB_User();
					HttpContext.Current.Session[NModel.EnObject.CurrentLoginUserSessionName] = bll.GetModel((long?)User_ID);
					bll.Close();
					return 2;
				}
			}

			return 0;
		}

		public static long GetCookieUserSID()
		{
			String admin_User_Value = Tool.NTool.GetCookieValue(User_login_ck_name);

			string[] arr = admin_User_Value.Split('_');
			if (arr != null && arr.Length > 1)
			{
				long User_ID = 0;
				long.TryParse(arr[1], out User_ID);

				return User_ID;
			}
			return 0;
		}

		public static long GetCookieUserID()
		{
			String admin_User_Value = Tool.NTool.GetCookieValue(admin_login_ck_name);

			string[] arr = admin_User_Value.Split('_');
			if (arr != null && arr.Length > 1)
			{
				long User_ID = 0;
				long.TryParse(arr[1], out User_ID);

				return User_ID;
			}
			return 0;
		}

		public static bool ClearLogin()
		{
			Tool.NTool.AddCookie(admin_login_ck_name, "", -1);
			HttpContext.Current.Session[NModel.EnObject.LoginSessionName] = "";

			return true;
		}

		public static bool ClearLogin_User()
		{
			string Tablename = User_login_ck_name;
			Tool.NTool.AddCookie(Tablename, "", -1);
			HttpContext.Current.Session[NModel.EnObject.LoginUserSessionName] = null;
			;
			HttpContext.Current.Session[NModel.EnObject.CurrentLoginUserSessionName] = null;

			return true;
		}

		public static NModel.PageData<T> SetJsonData<T>(List<T> inModel)
		{
			StringBuilder sbStr = new StringBuilder();

			if (Tool.NTool.IsLtNULL<T>(inModel))
			{
				Dictionary<string, string> dict = new Dictionary<string, string>();

				int lastdo = 0;
				foreach (T outmodel in inModel)
				{
					dict.Clear();

					foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
					{
						if (propertyInfo != null)
						{
							string lastName = "_Show";
							if (propertyInfo.Name.ToLower().IndexOf("_operate") != -1)
							{
								int _out = 0;
								int.TryParse(propertyInfo.GetValue(outmodel, null) + "", out _out);
								dict.Add(propertyInfo.Name + lastName, "" + Tool.NStr.SetStatusMsg(_out, "可删除", "不可删"));
							}
							else if (propertyInfo.Name.ToLower().IndexOf("_status") != -1)
							{
								int _out = 0;
								int.TryParse(propertyInfo.GetValue(outmodel, null) + "", out _out);
								dict.Add(propertyInfo.Name + lastName, "" + Tool.NStr.SetStatusMsg(_out, "启用", "禁用"));
							}
							else
							{
							}

							dict.Add(propertyInfo.Name, propertyInfo.GetValue(outmodel, null) + "");
						}
					}
					//cw_group_key

					//Tool.N_STR.SetStatusMsg(outmodel.Level_Status, "启用", "禁用")
					lastdo++;
					if (lastdo >= inModel.Count)
					{
						sbStr.Append(Tool.NTool.ObjectToJsonStr(dict));
					}
					else
					{
						sbStr.Append(Tool.NTool.ObjectToJsonStr(dict) + ",");
					}
				}
			}

			NModel.PageData<T> outModel = new NModel.PageData<T>();
			outModel.TotalPage = 1;
			// outModel.OutData = inModel;

			outModel.OutStr = sbStr.ToString();

			return outModel;
		}

		public delegate void AppendJsonData(ref Dictionary<string, string> appdict, string jsonStr);

		public static NModel.PageData<T> SetDJsonData<T>(List<T> inModel, AppendJsonData AJD)
		{
			StringBuilder sbStr = new StringBuilder();

			if (Tool.NTool.IsLtNULL<T>(inModel))
			{
				Dictionary<string, string> dict = new Dictionary<string, string>();

				int lastdo = 0;
				foreach (T outmodel in inModel)
				{
					dict.Clear();

					foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
					{
						if (propertyInfo != null)
						{
							string lastName = "_Show";
							if (propertyInfo.Name.ToLower().IndexOf("_operate") != -1)
							{
								int _out = 0;
								int.TryParse(propertyInfo.GetValue(outmodel, null) + "", out _out);
								dict.Add(propertyInfo.Name + lastName, "" + Tool.NStr.SetStatusMsg(_out, "可删除", "不可删"));
							}
							else if (propertyInfo.Name.ToLower().IndexOf("_status") != -1)
							{
								int _out = 0;
								int.TryParse(propertyInfo.GetValue(outmodel, null) + "", out _out);
								dict.Add(propertyInfo.Name + lastName, "" + Tool.NStr.SetStatusMsg(_out, "启用", "禁用"));
							}
							else
							{
							}

							dict.Add(propertyInfo.Name, propertyInfo.GetValue(outmodel, null) + "");
						}
					}

					string dct = Tool.NTool.ObjectToJsonStr(dict);

					lastdo++;
					if (lastdo >= inModel.Count)
					{
						sbStr.Append(dct);
					}
					else
					{
						sbStr.Append(dct + ",");
					}

					AJD(ref dict, dct);
				}
			}

			NModel.PageData<T> outModel = new NModel.PageData<T>();
			outModel.TotalPage = 1;
			// outModel.OutData = inModel;

			outModel.OutStr = sbStr.ToString();

			return outModel;
		}

		public static void WhileAddArea()
		{
		}
	}
}