using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
	public class AdminItemController : Controller
	{
		//
		// GET: /AdminItem/

		public ActionResult Index()
		{
			return View();
		}

		[BLL.NMenuAuthorize(MenuToId = 127)]
		public ActionResult Item_admin_user()
		{
			BLL.Admin_User dal = new BLL.Admin_User();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];
			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			NModel.PageData<NModel.Admin_User> PD = new NModel.PageData<NModel.Admin_User>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetListTotal()
			};
			dal.Close();
			return View(PD);
		}

		[BLL.NMenuAuthorize(MenuToId = 126)]
		public ActionResult Item_admin_role()
		{
			BLL.Admin_Role dal = new BLL.Admin_Role();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];
			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			NModel.PageData<NModel.Admin_Role> PD = new NModel.PageData<NModel.Admin_Role>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetListTotal()
			};
			dal.Close();
			return View(PD);
		}

		[BLL.NMenuAuthorize(MenuToId = 23)]
		public ActionResult Item_admin_menu()
		{
			//pageNum=1  numPerPage=100  orderField=${param.orderField}  orderDirection=${param.orderDirection}

			BLL.Admin_Menu dal = new BLL.Admin_Menu();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];
			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "120";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			NModel.PageData<NModel.Admin_Menu> PD = new NModel.PageData<NModel.Admin_Menu>()
			   {
				   NumPerPage = numPerPage,
				   PageNum = pagenum,
				   TotalPage = dal.GetListTotal()
			   };
			dal.Close();
			return View(PD);
		}

		[BLL.NMenuAuthorize(MenuToId = 128)]
		public ActionResult Item_pw_admin_user()
		{
			return View();
		}
	}
}