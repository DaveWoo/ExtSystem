using System;

namespace NModel
{
	//db_ad
	public class DB_AD
	{
		/// <summary>
		/// auto_increment
		/// </summary>
		private long _ad_id;

		public long AD_ID
		{
			get { return _ad_id; }
			set { _ad_id = value; }
		}

		/// <summary>
		/// AD_Name
		/// </summary>
		private string _ad_name;

		public string AD_Name
		{
			get { return _ad_name; }
			set { _ad_name = value; }
		}

		/// <summary>
		/// AD_URL
		/// </summary>
		private string _ad_url;

		public string AD_URL
		{
			get { return _ad_url; }
			set { _ad_url = value; }
		}

		/// <summary>
		/// AD_Code
		/// </summary>
		private string _ad_num;

		public string AD_Num
		{
			get { return _ad_num; }
			set { _ad_num = value; }
		}

		/// <summary>
		/// AD_Name_En
		/// </summary>
		private string _ad_name_en;

		public string AD_Name_En
		{
			get { return _ad_name_en; }
			set { _ad_name_en = value; }
		}

		/// <summary>
		/// AD_Status
		/// </summary>
		private int? _ad_status;

		public int? AD_Status
		{
			get { return _ad_status; }
			set { _ad_status = value; }
		}

		/// <summary>
		/// AD_EditTime
		/// </summary>
		private DateTime? _ad_edittime;

		public DateTime? AD_EditTime
		{
			get { return _ad_edittime; }
			set { _ad_edittime = value; }
		}

		/// <summary>
		/// AD_AddTime
		/// </summary>
		private DateTime? _ad_addtime;

		public DateTime? AD_AddTime
		{
			get { return _ad_addtime; }
			set { _ad_addtime = value; }
		}

		/// <summary>
		/// AD_Img
		/// </summary>
		private string _ad_img;

		public string AD_Img
		{
			get { return _ad_img; }
			set { _ad_img = value; }
		}

		/// <summary>
		/// AD_Operate
		/// </summary>
		private int? _ad_operate;

		public int? AD_Operate
		{
			get { return _ad_operate; }
			set { _ad_operate = value; }
		}

		/// <summary>
		/// AD_Type_Name
		/// </summary>
		private string _ad_type_name;

		public string AD_Type_Name
		{
			get { return _ad_type_name; }
			set { _ad_type_name = value; }
		}

		/// <summary>
		/// AD_SortNo
		/// </summary>
		private long _ad_sortno;

		public long AD_SortNo
		{
			get { return _ad_sortno; }
			set { _ad_sortno = value; }
		}

		/// <summary>
		/// AD_SortNo
		/// </summary>
		private string _ad_content;

		public string AD_Content
		{
			get { return _ad_content; }
			set { _ad_content = value; }
		}

		/// <summary>
		/// AD_SortNo
		/// </summary>
		private string _ad_content_en;

		public string AD_Content_En
		{
			get { return _ad_content_en; }
			set { _ad_content_en = value; }
		}

		private string _ad_summary_en;

		public string AD_Summary_En
		{
			get { return _ad_summary_en; }
			set { _ad_summary_en = value; }
		}

		private string _ad_summary;

		public string AD_Summary
		{
			get { return _ad_summary; }
			set { _ad_summary = value; }
		}
	}
}