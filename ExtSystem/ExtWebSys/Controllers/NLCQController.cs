using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;

namespace ExtWebSys.Controllers
{
    public class NLCQController : BLL.NewsPager
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //删除登录状态
            BLL.DB_Session bll_Session = new BLL.DB_Session();
            bll_Session.DelOutTime();
            bll_Session.Close();






            //user = BLL.Fun.Current_Login_DB_User();

            //if (user == null)
            //{
            //    filterContext.Result = this.RedirectToAction("Index", "Main");

            //}

            base.OnActionExecuting(filterContext);

        }
    
        //
        // GET: /NLCQ/
        public ActionResult Reg()
        {
            NModel.NewsPager model_pager = LoadConfig("/nlcq/reg");



           
            string  _Captcha   = this.Request.Form["Captcha"];
            string password = this.Request.Form["password"];
            string isCueLoginStatus = this.Request["isCueLoginStatus"];
            //验证码
            if (!string.IsNullOrEmpty(_Captcha) && !string.IsNullOrEmpty(NModel.EnObject.GetCodeValue + ""))
            {

                if (_Captcha.Equals(NModel.EnObject.GetCodeValue + ""))
                {



                    String JsonStr = "";

                    NModel.DB_User model = new NModel.DB_User();
                    BLL.DB_User bll_User = new BLL.DB_User();


                    bool isGet = Tool.NStr.PostForSetVal<NModel.DB_User>(ref model, ref JsonStr, "");


                    bool isUserNameExists =!String.IsNullOrEmpty(model.User_Name) &&bll_User.ExistsName(model.User_Name);
                  
                    if (isGet&&!isUserNameExists) {
                        bool isUserEmailExists = !String.IsNullOrEmpty(model.User_Email) && bll_User.ExistsEmail(model.User_Email);
                        if (!isUserEmailExists)
                        {

                            if (!string.IsNullOrEmpty(model.User_PW)&&!string.IsNullOrEmpty(password)) {

                                //密码是否相同
                                if (model.User_PW.Trim().Equals(password.Trim())) {

                                    model.User_PW=NMd5.GetMd5Hash(model.User_PW);
                                    //增加数据
                                    if(bll_User.AddFree(model)>0){

                                        bll_User.CheckLogin(model.User_Name, model.User_PW, _Captcha, true);
                                         Tool.NMsg.AlertAndRedirect("注册成功","/nlcq/index");
                                    
                                    
                                    }


                                    //登录




                                    //刷新验证码
                                    NCaptcha.Generate(NModel.EnObject.UserCodeSessionName);
                                
                                }



                              
                            
                            }



                           
                           

                        }
                    
                    
                    }
                    bll_User.Close();

                    
                
                }
            }
           

           // if(isGet&&model.)



            return View(model_pager);
        }
        public ActionResult Login() {
            NModel.NewsPager model_pager = LoadConfig("/nlcq/login");
            string loginCaptcha=this.Request.Form["loginCaptcha"];
            string User_ID=this.Request.Form["User_ID"];
            string User_Pw=this.Request.Form["User_Pw"];
            string isCutLoginStatus=this.Request.Form["isCutLoginStatus"];

            bool UserNotEmpty=!string.IsNullOrEmpty(User_ID)&&!string.IsNullOrEmpty(User_Pw);
            bool isNotEmpty = !string.IsNullOrEmpty(loginCaptcha) && !string.IsNullOrEmpty(NModel.EnObject.GetCodeValue);
            if (isNotEmpty&&UserNotEmpty) {

                if (loginCaptcha.Trim().ToLower().Equals(NModel.EnObject.GetCodeValue.Trim().ToLower())) {
                    byte isBool=0;
                    byte.TryParse(isCutLoginStatus,out isBool );
                    bool isCue=isBool==1?true:false;

                    BLL.DB_User bll_User = new BLL.DB_User();
                  bool isLogin=  bll_User.CheckLogin(User_ID, User_Pw, "", isCue);
                  bll_User.Close();
                  if (isLogin) {

                     return this.RedirectToAction("index", "nlcq");
                  
                  }


                   


                
                }
            
            
            }



            return View(model_pager);
        }
        public ActionResult Index()
        {   //分类
            BLL.DB_Classify bll_Classify = new BLL.DB_Classify();
            //广告
            BLL.DB_AD bll_ad = new BLL.DB_AD();
            NModel.NewsPager model_pager = LoadConfig("/nlcq/index");

            model_pager.IDictClassify = new Dictionary<string, IList<NModel.DB_Classify>>();
            {
                IList<NModel.DB_Classify> modelClassifyList = null;
                modelClassifyList = bll_Classify.GetListByNumAndTop("1", 150);
                modelClassifyList = modelClassifyList != null ? modelClassifyList : new List<NModel.DB_Classify>();

                model_pager.IDictClassify.Add("代码分类", modelClassifyList);
            }

            {
                IList<NModel.DB_Classify> modelClassifyList = null;
                modelClassifyList = bll_Classify.GetListByNumAndTop("2", 150);
                modelClassifyList = modelClassifyList != null ? modelClassifyList : new List<NModel.DB_Classify>();

                model_pager.IDictClassify.Add("素材分类", modelClassifyList);
            }


            { 
            model_pager.IDictListAd = new Dictionary<string, IList<NModel.DB_AD>>();
         
            string sql_ad = string.Format(" ''01'' ");
            IList<NModel.DB_AD> model_ad_list = bll_ad.GetListByNumAndTop(sql_ad, 8);

            model_pager.IDictListAd.Add("首页广告一", model_ad_list != null ? model_ad_list : new List<NModel.DB_AD>());

                
            }

            bll_ad.Close();



            bll_Classify.Close();
            return View(model_pager);
        }

    }
}
