using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NPinyin;

namespace ExtWebSys.Controllers
{
	public class FactoryController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Area()
		{
			string dataJson = this.Request.Form["dataJson"];

			JavaScriptSerializer jss = new JavaScriptSerializer();
			object[] obj = (object[])jss.DeserializeObject(dataJson);

			BLL.DB_Area bll = new BLL.DB_Area();
			List<NModel.DB_Area> nmodel = bll.GetList();

			foreach (NModel.DB_Area n in nmodel)
			{
				n.Area_PinYin = Pinyin.GetPinyin(n.Area_Name);

				bll.Edit(n);
			}

			return View();
		}
	}
}