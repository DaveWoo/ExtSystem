using System;

namespace NModel
{
	public class DB_News
	{
		public long? News_ID { get; set; }

		public string News_Title { get; set; }
		public string News_Title_En { get; set; }
		public string News_Content { get; set; }

		public string News_Content_En { get; set; }

		public int? News_Status { get; set; }
		public long? News_SortNo { get; set; }

		public DateTime? News_AddTime { get; set; }

		public DateTime News_EditTime { get; set; }

		public string News_Num { get; set; }

		public string News_Summary { get; set; }

		public string News_ClassifyName { get; set; }

		public int? News_IsIndex { get; set; }

		public int? News_IsHot { get; set; }

		public int? News_IsRecommend { get; set; }
		public long? News_ClickRate { get; set; }
	}
}