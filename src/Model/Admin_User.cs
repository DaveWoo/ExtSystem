using System;

namespace NModel
{
	//admin_user
	public class Admin_User : Admin_Role
	{
		/// <summary>
		/// auto_increment
		/// </summary>
		private long _user_id;

		public long User_ID
		{
			get { return _user_id; }
			set { _user_id = value; }
		}

		/// <summary>
		/// User_Name
		/// </summary>
		private string _user_name;

		public string User_Name
		{
			get { return _user_name; }
			set { _user_name = value; }
		}

		/// <summary>
		/// User_PassWord
		/// </summary>
		private string _user_password;

		public string User_PassWord
		{
			get { return _user_password; }
			set { _user_password = value; }
		}

		/// <summary>
		/// User_Status
		/// </summary>
		private int _user_status;

		public int User_Status
		{
			get { return _user_status; }
			set { _user_status = value; }
		}

		/// <summary>
		/// User_AddTime
		/// </summary>
		private DateTime _user_addtime;

		public DateTime User_AddTime
		{
			get { return _user_addtime; }
			set { _user_addtime = value; }
		}

		/// <summary>
		/// User_EditTime
		/// </summary>
		private DateTime _user_edittime;

		public DateTime User_EditTime
		{
			get { return _user_edittime; }
			set { _user_edittime = value; }
		}

		/// <summary>
		/// User_SortNo
		/// </summary>
		private long _user_sortno;

		public long User_SortNo
		{
			get { return _user_sortno; }
			set { _user_sortno = value; }
		}

		/// <summary>
		/// User_Ip
		/// </summary>
		private string _user_ip;

		public string User_Ip
		{
			get { return _user_ip; }
			set { _user_ip = value; }
		}

		/// <summary>
		/// User_LoginTime
		/// </summary>
		private DateTime _user_logintime;

		public DateTime User_LoginTime
		{
			get { return _user_logintime; }
			set { _user_logintime = value; }
		}

		/// <summary>
		/// User_RealName
		/// </summary>
		private string _user_realname;

		public string User_RealName
		{
			get { return _user_realname; }
			set { _user_realname = value; }
		}

		/// <summary>
		/// User_Email
		/// </summary>
		private string _user_email;

		public string User_Email
		{
			get { return _user_email; }
			set { _user_email = value; }
		}

		/// <summary>
		/// User_Role_ID
		/// </summary>
		private long _user_role_id;

		public long User_Role_ID
		{
			get { return _user_role_id; }
			set { _user_role_id = value; }
		}

		/// <summary>
		/// User_Operate
		/// </summary>
		private int _user_operate;

		public int User_Operate
		{
			get { return _user_operate; }
			set { _user_operate = value; }
		}
	}
}