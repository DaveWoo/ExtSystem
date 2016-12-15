using System.Collections.Generic;

namespace NModel
{
	public class PageData<T>
	{
		public long? TotalPage { get; set; }
		public List<T> OutData { get; set; }
		public T OutObj { get; set; }
		public string PageNum { get; set; }
		public string NumPerPage { get; set; }
		public string OutStr { get; set; }

		public string OrderField { get; set; }
		public string Where { get; set; }
	}
}