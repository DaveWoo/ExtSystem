using System;

namespace NModel
{
	public class DB_File_Category
	{
		public long? File_Category_ID { get; set; }

		public string File_Category_Name { get; set; }
		public string File_Category_Name_En { get; set; }
		public long? File_Category_Size { get; set; }
		public DateTime? File_Category_AddTime { get; set; }
		public DateTime? File_Category_EditTime { get; set; }
		public long? File_Category_SortNo { get; set; }
		public int? File_Category_Status { get; set; }
		public int? File_Category_Operate { get; set; }
		public string File_Category_Folder { get; set; }
	}
}