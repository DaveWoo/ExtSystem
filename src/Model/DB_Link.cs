using System;

namespace NModel
{
	public class DB_Link
	{
		public long? Link_ID { get; set; }
		public string Link_Name { get; set; }
		public string Link_Name_En { get; set; }

		public string Link_Url { get; set; }

		public int? Link_Operate { get; set; }

		public int? Link_Status { get; set; }

		public string Link_Num { get; set; }

		public long? Link_SortNo { get; set; }

		public DateTime? Link_AddTime { get; set; }

		public DateTime? Link_EditTime { get; set; }
	}
}