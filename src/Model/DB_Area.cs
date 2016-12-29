using System;

namespace NModel
{
	public class DB_Area
	{
		public long? Area_ID { get; set; }
		public string Area_Code { get; set; }
		public string Area_Name { get; set; }
		public string Area_Name_En { get; set; }

		public string Area_PinYin { get; set; }
		public DateTime? Area_AddTime { get; set; }
		public DateTime? Area_EditTime { get; set; }

		public long? Area_SortNo { get; set; }

		public int? Area_Status { get; set; }
	}
}