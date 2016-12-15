using System;

namespace NModel
{
	public class DB_User
	{
		public long? User_ID { get; set; }

		/// <summary>
		/// 用户账号
		/// </summary>
		public string User_Name { get; set; }

		public string User_RealName { get; set; }

		/// <summary>
		/// 用户头像
		/// </summary>
		public string User_Img_Url { get; set; }

		/// <summary>
		/// 微信
		/// </summary>
		public string User_WeChat { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		public string User_Phone { get; set; }

		/// <summary>
		/// 固定电话
		/// </summary>
		public string User_Telephone { get; set; }

		/// <summary>
		/// 企业QQ号
		/// </summary>
		public string User_EnterpriseQQ { get; set; }

		/// <summary>
		/// 用户QQ
		/// </summary>
		public string User_QQ { get; set; }

		/// <summary>
		/// 用户密码
		/// </summary>
		public string User_PW { get; set; }

		public DateTime? User_AddTime { get; set; }

		public DateTime? User_EditTime { get; set; }

		public long? User_SortNo { get; set; }
		public int? User_Status { get; set; }

		/// <summary>
		/// 邮编
		/// </summary>
		public string User_ZipCode { get; set; }

		/// <summary>
		/// 家乡城市
		/// </summary>
		public string User_Home_City_Code { get; set; }

		/// <summary>
		/// 个性签名
		/// </summary>
		public string User_Signature { get; set; }

		/// <summary>
		/// 呢称
		/// </summary>
		public string User_Nickname { get; set; }

		/// <summary>
		/// 用户Email
		/// </summary>
		public string User_Email { set; get; }

		/// <summary>
		/// 用户住址
		/// </summary>
		public string User_Adderss { get; set; }

		/// <summary>
		/// 用户年龄
		/// </summary>
		public string User_Age { get; set; }

		public int? User_Sex { get; set; }

		/// <summary>
		/// 用户生活圈
		/// </summary>
		public string User_Life_Circle_Code { get; set; }

		/// <summary>
		/// 用户工作圈
		/// </summary>
		public string User_Work_Coil_Code { get; set; }

		/// <summary>
		/// 省份
		/// </summary>
		public string User_Prov { get; set; }

		/// <summary>
		/// 城市
		/// </summary>
		public string User_City { get; set; }

		/// <summary>
		/// 地区
		/// </summary>
		public string User_Dist { get; set; }

		public string User_HeadImg { get; set; }
	}
}