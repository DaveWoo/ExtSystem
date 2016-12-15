using System;

namespace NModel
{
	//admin_menu
	public class Admin_Menu
	{
		/// <summary>
		/// auto_increment
		/// </summary>
		private long? _menu_id;

		public long? Menu_ID
		{
			get { return _menu_id; }
			set { _menu_id = value; }
		}

		/// <summary>
		/// Menu_Name
		/// </summary>
		private string _menu_name;

		public string Menu_Name
		{
			get { return _menu_name; }
			set { _menu_name = value; }
		}

		/// <summary>
		/// Menu_Name_en
		/// </summary>
		private string _menu_name_en;

		public string Menu_Name_en
		{
			get { return _menu_name_en; }
			set { _menu_name_en = value; }
		}

		/// <summary>
		/// Menu_URL
		/// </summary>
		private string _menu_url;

		public string Menu_URL
		{
			get { return _menu_url; }
			set { _menu_url = value; }
		}

		/// <summary>
		/// Menu_Status
		/// </summary>
		private int? _menu_status;

		public int? Menu_Status
		{
			get { return _menu_status; }
			set { _menu_status = value; }
		}

		/// <summary>
		/// Menu_AddTime
		/// </summary>
		private DateTime? _menu_addtime;

		public DateTime? Menu_AddTime
		{
			get { return _menu_addtime; }
			set { _menu_addtime = value; }
		}

		/// <summary>
		/// Menu_EditTime
		/// </summary>
		private DateTime? _menu_edittime;

		public DateTime? Menu_EditTime
		{
			get { return _menu_edittime; }
			set { _menu_edittime = value; }
		}

		/// <summary>
		/// Menu_SortNo
		/// </summary>
		private long? _menu_sortno;

		public long? Menu_SortNo
		{
			get { return _menu_sortno; }
			set { _menu_sortno = value; }
		}

		/// <summary>
		/// Menu_Operate
		/// </summary>
		private int? _menu_operate;

		public int? Menu_Operate
		{
			get { return _menu_operate; }
			set { _menu_operate = value; }
		}

		/// <summary>
		/// Menu_Img_url
		/// </summary>
		private string _menu_img_url;

		public string Menu_Img_url
		{
			get { return _menu_img_url; }
			set { _menu_img_url = value; }
		}

		/// <summary>
		/// Menu_X
		/// </summary>
		private long? _menu_x;

		public long? Menu_X
		{
			get { return _menu_x; }
			set { _menu_x = value; }
		}

		/// <summary>
		/// Menu_Y
		/// </summary>
		private long? _menu_y;

		public long? Menu_Y
		{
			get { return _menu_y; }
			set { _menu_y = value; }
		}

		/// <summary>
		/// Menu_Z
		/// </summary>
		private long? _menu_z;

		public long? Menu_Z
		{
			get { return _menu_z; }
			set { _menu_z = value; }
		}

		private string _menu_num;

		public string Menu_Num
		{
			get { return _menu_num; }
			set { _menu_num = value; }
		}
	}
}