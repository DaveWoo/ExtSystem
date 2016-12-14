using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
    public class MemberController : BLL.NewsPager
    {

        NModel.NewsPager model_pager = null;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            BLL.DB_Session bll_Session = new BLL.DB_Session();
            bll_Session.DelOutTime();
            bll_Session.Close();

            model_pager = LoadConfig("member");


            model_pager.IDictUserModel = new Dictionary<string, NModel.DB_User>();


         var user= BLL.Fun.Current_Login_DB_User();

            //用户是否登录
         if (user == null) {

             filterContext.Result = this.RedirectToAction("login", "mlcq");
         
         }
           

            base.OnActionExecuting(filterContext);



        }
        //
        // GET: /Member/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 买入管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BuyingManagement()
        {

           



            return View(model_pager);
        }
        /// <summary>
        /// 停卖素材
        /// </summary>
        /// <returns></returns>
        public ActionResult Stop_Sell_Material()
        {

          



            return View(model_pager);
        }
        /// <summary>
        /// 卖出素材
        /// </summary>
        /// <returns></returns>
        public ActionResult Sell_Material()
        {

           



            return View(model_pager);
        }
        /// <summary>
        /// 出售中素材
        /// </summary>
        /// <returns></returns>
        public ActionResult SellMaterial()
        {

           


            return View(model_pager);
        }
        /// <summary>
        /// 发布素材
        /// </summary>
        /// <returns></returns>
        public ActionResult ReleaseMaterial()
        {

          



            return View(model_pager);
        }
        /// <summary>
        /// 提现
        /// </summary>
        /// <returns></returns>
        public ActionResult MentionNow()
        {
 



            return View(model_pager);
        }
        /// <summary>
        /// 账号充值
        /// </summary>
        /// <returns></returns>
        public ActionResult Recharge()
        {

            



            return View(model_pager);
        }
        /// <summary>
        /// 推广赚钱
        /// </summary>
        /// <returns></returns>
        public ActionResult Extension()
        {

           



            return View(model_pager);
        }

        /// <summary>
        ///  账目明细
        /// </summary>
        /// <returns></returns>
        public ActionResult Account()
        {

           



            return View(model_pager);
        }

        /// <summary>
        /// 更改头像图片
        /// </summary>
        /// <returns></returns>
        public ActionResult Hd() {

         



            return View(model_pager);
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPw()
        {

          



            return View(model_pager);


        }
        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalData()
        {

           



            return View(model_pager);


        }
        /// <summary>
        /// 站内消息
        /// </summary>
        /// <returns></returns>
        public ActionResult CMember()
        {

            



            return View(model_pager);


        }

    }
}
