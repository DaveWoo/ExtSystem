using System;

namespace NModel
{
	public class DB_WebConfig
	{
		public long? WebConfig_ID { get; set; }
		public string WebConfig_Title { get; set; }
		public string WebConfig_Title_En { get; set; }
		public string WebConfig_Keywords { get; set; }
		public string WebConfig_Keywords_En { get; set; }
		public string WebConfig_Description { get; set; }

		public string WebConfig_Description_En { get; set; }

		public string WebConfig_Author { get; set; }

		public string WebConfig_Author_En { get; set; }
		public DateTime? WebConfig_AddTime { get; set; }
		public DateTime? WebConfig_EditTime { get; set; }
		public long? WebConfig_SortNo { get; set; }
		public long? WebConfig_Language_ID { get; set; }
		public int WebConfig_Operate { get; set; }
		public string WebConfig_Code { get; set; }

		public string WebConfig_SendEmail { get; set; }
		public string WebConfig_SendPw { get; set; }
		public string WebConfig_SubContent { get; set; }
		public string WebConfig_ServerAdrress { get; set; }
		public string WebConfig_SubTitle { get; set; }
		public string WebConfig_ToEmail { get; set; }

		public string WebConfig_SendName { get; set; }
		public string WebConfig_ToName { get; set; }
	}
}