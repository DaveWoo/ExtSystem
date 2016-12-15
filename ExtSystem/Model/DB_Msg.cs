using System;

namespace NModel
{
	//db_msg
	public class DB_Msg
	{
		/// <summary>
		/// auto_increment
		/// </summary>
		private long _msg_id;

		public long Msg_ID
		{
			get { return _msg_id; }
			set { _msg_id = value; }
		}

		/// <summary>
		/// Msg_Name
		/// </summary>
		private string _msg_name;

		public string Msg_Name
		{
			get { return _msg_name; }
			set { _msg_name = value; }
		}

		/// <summary>
		/// Msg_Code
		/// </summary>
		private string _msg_code;

		public string Msg_Code
		{
			get { return _msg_code; }
			set { _msg_code = value; }
		}

		/// <summary>
		/// Msg_Content
		/// </summary>
		private string _msg_content;

		public string Msg_Content
		{
			get { return _msg_content; }
			set { _msg_content = value; }
		}

		/// <summary>
		/// Msg_URL
		/// </summary>
		private string _msg_url;

		public string Msg_URL
		{
			get { return _msg_url; }
			set { _msg_url = value; }
		}

		/// <summary>
		/// Msg_Ip
		/// </summary>
		private string _msg_ip;

		public string Msg_Ip
		{
			get { return _msg_ip; }
			set { _msg_ip = value; }
		}

		/// <summary>
		/// Msg_AddTime
		/// </summary>
		private DateTime? _msg_addtime;

		public DateTime? Msg_AddTime
		{
			get { return _msg_addtime; }
			set { _msg_addtime = value; }
		}

		/// <summary>
		/// Msg_EditTime
		/// </summary>
		private DateTime? _msg_edittime;

		public DateTime? Msg_EditTime
		{
			get { return _msg_edittime; }
			set { _msg_edittime = value; }
		}
	}
}