using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace ExtWebSys.Controllers
{
	public class NewsController : BLL.NewsPager
	{
		//
		// GET: /News/
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
		}

		public void LoadLeftRight(ref NModel.NewsPager model_pager)
		{
			IList<NModel.DB_News> model_news_left = null;
			IList<NModel.DB_News> model_news_right = null;
			BLL.DB_News bll_news = new BLL.DB_News();

			model_news_left = bll_news.GetNewListByWhereAndTop(" News_IsHot=1 ", 6);
			model_news_right = bll_news.GetNewListByWhereAndTop(" News_IsRecommend=1 ", 6);
			model_pager.IDctNewsList.Add("left", model_news_left != null ? model_news_left : new List<NModel.DB_News>());
			model_pager.IDctNewsList.Add("right", model_news_right != null ? model_news_right : new List<NModel.DB_News>());

			bll_news.Close();
		}

		public ActionResult Detailed(long? id)
		{
			BLL.DB_News bll_news = new BLL.DB_News();

			NModel.NewsPager model_pager = LoadConfig("Index");

			model_pager.IDctNewsList = new Dictionary<string, IList<NModel.DB_News>>();

			LoadLeftRight(ref model_pager);

			model_pager.IDictMenu = new Dictionary<string, IList<NModel.Admin_Menu>>();
			model_pager.IDctNewsModel = new Dictionary<string, NModel.DB_News>();
			NModel.DB_News model_news = new NModel.DB_News();

			bll_news.UpdateClickRate(id);
			model_news = bll_news.GetModel(id);
			model_news = model_news != null ? model_news : new NModel.DB_News();

			ViewBag.Index = model_news.News_Num;
			ViewBag.nIndex = model_news.News_Num;
			ViewBag.NewsNum = model_news.News_Num;
			if (!string.IsNullOrEmpty(model_news.News_Num))
			{
				if (model_news.News_Num.Length >= 2)
					ViewBag.Index = ViewBag.NewsNum = model_news.News_Num.Substring(0, 2);
			}

			BLL.Admin_Menu bll_Menu = new BLL.Admin_Menu();
			IList<NModel.Admin_Menu> menuList = bll_Menu.GetNewListByWhereAndTop("menu_num like ''" + ViewBag.NewsNum + "%'' and len(menu_num)<=4  ", 12);
			menuList = menuList != null ? menuList : new List<NModel.Admin_Menu>();

			model_pager.IDictMenu.Add("新闻分类", menuList);
			model_pager.IDctNewsModel.Add("新闻实体", model_news);

			bll_news.Close();
			bll_Menu.Close();
			return View(model_pager);
		}

		public ActionResult List(string id)
		{
			ViewBag.Index = id;
			ViewBag.nIndex = id;
			ViewBag.NewsNum = id;
			if (!string.IsNullOrEmpty(id))
			{
				if (id.Length >= 2)
					ViewBag.Index = ViewBag.NewsNum = id.Substring(0, 2);
			}

			int pageSize = 20;
			string order = "news_addtime desc";
			StringBuilder where = new StringBuilder();
			where.Append("1=1 ");
			where.AppendFormat("and news_num like ''{0}%''", id);

			NModel.NewsPager model_pager = LoadConfig("Index");

			model_pager.IDctNewsList = new Dictionary<string, IList<NModel.DB_News>>();

			LoadLeftRight(ref model_pager);

			model_pager.IDictMenu = new Dictionary<string, IList<NModel.Admin_Menu>>();
			model_pager.IDctPagedNews = new Dictionary<string, IPagedList<NModel.DB_News>>();

			BLL.Admin_Menu bll_Menu = new BLL.Admin_Menu();
			IList<NModel.Admin_Menu> menuList = bll_Menu.GetNewListByWhereAndTop("menu_num like ''" + ViewBag.NewsNum + "%'' and len(menu_num)<=4  ", 12);
			menuList = menuList != null ? menuList : new List<NModel.Admin_Menu>();

			model_pager.IDictMenu.Add("新闻分类", menuList);
			int pageIndex = 0;
			int.TryParse(this.Request.QueryString["pageindex"], out pageIndex);
			if (pageIndex <= 0) { pageIndex = 1; }
			BLL.DB_News bll_news = new BLL.DB_News();

			List<NModel.DB_News> model_news_list = new List<NModel.DB_News>();

			model_news_list = bll_news.GetSortAndWherePagerList(pageIndex, pageSize, order, where.ToString());
			int totalItems = 0;

			int.TryParse(bll_news.GetWhereListTotal(where.ToString()) + "", out totalItems);
			if (model_news_list != null)
			{
				model_pager.IDctPagedNews.Add("新闻列表", new PagedList<NModel.DB_News>(model_news_list, pageIndex, pageSize, totalItems));
			}
			else
			{
				model_pager.IDctPagedNews.Add("新闻列表", null);
			}

			bll_news.Close();
			bll_Menu.Close();
			return View(model_pager);
		}

		public ActionResult Index()
		{
			NModel.NewsPager model_pager = LoadConfig("Index");

			BLL.DB_News bll_news = new BLL.DB_News();

			model_pager.IDctNewsList = new Dictionary<string, IList<NModel.DB_News>>();

			LoadLeftRight(ref model_pager);

			IList<NModel.DB_News> model_news_num = null;
			IList<string> numList = new List<string>();
			numList.Add("0101");
			numList.Add("0102");
			numList.Add("0103");
			numList.Add("0201");
			numList.Add("0202");
			numList.Add("0203");
			numList.Add("0301");
			numList.Add("0302");
			numList.Add("0303");
			numList.Add("0401");
			numList.Add("0402");
			numList.Add("0403");
			numList.Add("0501");
			numList.Add("0502");
			numList.Add("0503");
			numList.Add("0504");
			numList.Add("0601");
			numList.Add("0602");
			numList.Add("0603");

			foreach (string val in numList)
			{
				model_news_num = bll_news.GetNewListByWhereAndTop(string.Format(" news_num=''{0}''and  News_IsIndex=1", val), 6);

				model_pager.IDctNewsList.Add(val, model_news_num != null ? model_news_num : new List<NModel.DB_News>());
			}

			model_pager.IDictListAd = new Dictionary<string, IList<NModel.DB_AD>>();
			BLL.DB_AD bll_ad = new BLL.DB_AD();
			string sql_ad = string.Format(" ''01'' ");
			IList<NModel.DB_AD> model_ad_list = bll_ad.GetListByNumAndTop(sql_ad, 8);

			model_pager.IDictListAd.Add("中间广告", model_ad_list != null ? model_ad_list : new List<NModel.DB_AD>());

			bll_ad.Close();
			bll_news.Close();
			return View(model_pager);
		}
	}
}