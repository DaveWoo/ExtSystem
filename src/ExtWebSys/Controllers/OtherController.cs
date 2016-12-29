using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
	public class OtherController : Controller
	{
		//
		// GET: /Other/

		public ActionResult LocalCity()
		{
			return View();
		}

		public ActionResult Map()
		{
			ViewBag.City = HttpUtility.UrlDecode(this.Request["city"]);
			ViewBag.Address = HttpUtility.UrlDecode(this.Request["address"]);

			return View();
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}