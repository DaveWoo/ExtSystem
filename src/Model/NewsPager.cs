using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;

namespace NModel
{
	public class NewsPager : WebPager<NModel.DB_Area>
	{
		public IDictionary<string, IList<NModel.DB_News>> IDctNewsList { get; set; }
		public IDictionary<string, NModel.DB_News> IDctNewsModel { get; set; }

		public IDictionary<string, IPagedList<NModel.DB_News>> IDctPagedNews { get; set; }
	}
}