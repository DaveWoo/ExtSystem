using System;

namespace NModel
{
	//admin_role
	public class Admin_Role
	{
		/// <summary>
		/// auto_increment
		/// </summary>
		private long? _role_id;

		public long? Role_ID
		{
			get { return _role_id; }
			set { _role_id = value; }
		}

		/// <summary>
		/// Role_Name
		/// </summary>
		private string _role_name;

		public string Role_Name
		{
			get { return _role_name; }
			set { _role_name = value; }
		}

		/// <summary>
		/// Role_Name_en
		/// </summary>
		private string _role_name_en;

		public string Role_Name_en
		{
			get { return _role_name_en; }
			set { _role_name_en = value; }
		}

		/// <summary>
		/// Role_EditTime
		/// </summary>
		private DateTime? _role_edittime;

		public DateTime? Role_EditTime
		{
			get { return _role_edittime; }
			set { _role_edittime = value; }
		}

		/// <summary>
		/// Role_AddTime
		/// </summary>
		private DateTime? _role_addtime;

		public DateTime? Role_AddTime
		{
			get { return _role_addtime; }
			set { _role_addtime = value; }
		}

		/// <summary>
		/// Role_Status
		/// </summary>
		private int? _role_status;

		public int? Role_Status
		{
			get { return _role_status; }
			set { _role_status = value; }
		}

		/// <summary>
		/// Role_SortNo
		/// </summary>
		private long? _role_sortno;

		public long? Role_SortNo
		{
			get { return _role_sortno; }
			set { _role_sortno = value; }
		}

		/// <summary>
		/// Role_Menu
		/// </summary>
		private string _role_menu;

		public string Role_Menu
		{
			get { return _role_menu; }
			set { _role_menu = value; }
		}
	}
}