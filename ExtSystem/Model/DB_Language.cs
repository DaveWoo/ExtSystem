using System;

namespace NModel
{
	public class DB_Language
	{
		public long? Language_ID { get; set; }

		public string Language_Name { get; set; }
		public string Language_Name_En { get; set; }

		public long? Language_SortNo { get; set; }
		public DateTime? Language_AddTime { get; set; }
		public DateTime? Language_EditTime { get; set; }
	}
}