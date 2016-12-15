using System;

namespace NModel
{
	//db_file
	public class DB_File : DB_File_Category
	{
		/// <summary>
		/// auto_increment
		/// </summary>
		private long? _file_id;

		public long? File_ID
		{
			get { return _file_id; }
			set { _file_id = value; }
		}

		/// <summary>
		/// File_Name
		/// </summary>
		private string _file_name;

		public string File_Name
		{
			get { return _file_name; }
			set { _file_name = value; }
		}

		/// <summary>
		/// File_Size
		/// </summary>
		private string _file_size;

		public string File_Size
		{
			get { return _file_size; }
			set { _file_size = value; }
		}

		/// <summary>
		/// File_Belongs
		/// </summary>
		private string _file_belongs;

		public string File_Belongs
		{
			get { return _file_belongs; }
			set { _file_belongs = value; }
		}

		/// <summary>
		/// File_AddTime
		/// </summary>
		private DateTime? _file_addtime;

		public DateTime? File_AddTime
		{
			get { return _file_addtime; }
			set { _file_addtime = value; }
		}

		/// <summary>
		/// File_EditTime
		/// </summary>
		private DateTime? _file_edittime;

		public DateTime? File_EditTime
		{
			get { return _file_edittime; }
			set { _file_edittime = value; }
		}

		/// <summary>
		/// File_Status
		/// </summary>
		private int? _file_status;

		public int? File_Status
		{
			get { return _file_status; }
			set { _file_status = value; }
		}

		/// <summary>
		/// File_Operate
		/// </summary>
		private int? _file_operate;

		public int? File_Operate
		{
			get { return _file_operate; }
			set { _file_operate = value; }
		}

		/// <summary>
		/// File_SortNo
		/// </summary>
		private long? _file_sortno;

		public long? File_SortNo
		{
			get { return _file_sortno; }
			set { _file_sortno = value; }
		}

		/// <summary>
		/// File_Category_ID
		/// </summary>
		private long? _file_cty_id;

		public long? File_Cty_ID
		{
			get { return _file_cty_id; }
			set { _file_cty_id = value; }
		}

		/// <summary>
		/// File_URL
		/// </summary>
		private string _file_url;

		public string File_URL
		{
			get { return _file_url; }
			set { _file_url = value; }
		}
	}
}