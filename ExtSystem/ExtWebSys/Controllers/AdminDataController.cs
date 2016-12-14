using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;   

namespace ExtWebSys.Controllers
{
    public class AdminDataController : Controller
    {
        //
        // GET: /AdminData/


        public ActionResult Index()
        {
            return View();
        }


       


        private int layer = 0; long x = 0, level;

        Dictionary<long, Dictionary<long, NModel.Admin_Menu>> dictListCmodel = new Dictionary<long, Dictionary<long, NModel.Admin_Menu>>();

        Dictionary<long, NModel.Admin_Menu> dictCmodel = new Dictionary<long, NModel.Admin_Menu>();
        private string SetRtMenu(List<NModel.Admin_Menu> _menu, NModel.Admin_Menu chlidModel, int? row, int layer, int Level)
        {





            string iconCls = "cw_application";
            if (!string.IsNullOrEmpty(chlidModel.Menu_Img_url))
            {

                iconCls = chlidModel.Menu_Img_url;

            }
            return Tool.NStr.JsonStr(false,
                "menu_id", chlidModel.Menu_ID + "",
                "menu_name", chlidModel.Menu_Name,
                "menu_url", chlidModel.Menu_URL,
                "menu_status_show", Tool.NStr.SetStatusMsg(chlidModel.Menu_Status, "启用", "禁用"),
                "menu_status", chlidModel.Menu_Status + "",
                "menu_x", chlidModel.Menu_X + "",
                "menu_y", chlidModel.Menu_Y + "",
                "menu_z", chlidModel.Menu_Z + "",
                "menu_sortno", chlidModel.Menu_SortNo + "",
                "menu_operate", chlidModel.Menu_Operate + "",
                "menu_operate_show", Tool.NStr.SetStatusMsg(chlidModel.Menu_Operate, "可删除", "不可删"),
                "menu_img_url", chlidModel.Menu_Img_url,
                "menu_addtime", chlidModel.Menu_AddTime + "",
                "menu_edittime", chlidModel.Menu_EditTime + "",
                "iconCls", iconCls





                );



        }

        private string _SetRtMenu(List<NModel.Admin_Menu> _menu, NModel.Admin_Menu chlidModel, int? row, int layer, int Level)
        {

            string iconCls = "cw_application";
            if (!string.IsNullOrEmpty(chlidModel.Menu_Img_url))
            {

                iconCls = chlidModel.Menu_Img_url;

            }

            //"state":"closed",
            if (!(Level == 2 && layer == 1) && row > 0 && Level == 2)
            {
                return Tool.NStr.JsonStr(false,
                    "id", chlidModel.Menu_ID + "",
                    "text", chlidModel.Menu_Name,
                     "url", chlidModel.Menu_URL,
                     "x", chlidModel.Menu_X + "",
                     "y", chlidModel.Menu_Y + "",
                     "z", chlidModel.Menu_Z + "",
                    //  "state", "closed",
                     "iconCls", iconCls

                    );

            }
            else
            {


                return Tool.NStr.JsonStr(false,
                       "id", chlidModel.Menu_ID + "",
                       "text", chlidModel.Menu_Name,
                        "url", chlidModel.Menu_URL,
                        "x", chlidModel.Menu_X + "",
                        "y", chlidModel.Menu_Y + "",
                        "z", chlidModel.Menu_Z + "",
                        "iconCls", iconCls

                       );


            }



        }



        public dynamic SetProcessResult(NModel.Admin_Menu newValue, NModel.Admin_Menu oldValue, List<NModel.Admin_Menu> _menu, int layer, int Level)
        {



            if (layer <= -1)
            {


                return (Predicate<NModel.Admin_Menu>)(a => a.Menu_Z == 1);


            }
            else
            {



                long eqStr = 0;
                if (Level > 2)
                {



                    string xStr = (oldValue.Menu_X + "");

                    if (xStr.IndexOf("3") != -1)
                    {


                    }

                    xStr = xStr.Replace("-", "");

                    long.TryParse(string.Format("{0}{1}", xStr, oldValue.Menu_Y), out eqStr);


                }
                else
                {
                    string xStr = (newValue.Menu_X + "");
                    xStr = xStr.Replace("-", "");
                    long.TryParse(xStr + "", out eqStr);


                }
                return (Predicate<NModel.Admin_Menu>)(a => (a.Menu_X + "").Replace("-", "").Equals(eqStr + "") && a.Menu_Z == (Level));

            }





        }


        public ActionResult Data_Left_Admin_Menu() {
            this.Response.ContentType = "text/plain";

            // this.Response.ContentType = "text/plain";
            // this.Response.ContentType = "text/plain";
            BLL.Admin_Menu dal = new BLL.Admin_Menu();
             List<NModel.Admin_Menu> _menu=null;
            try { 
            NModel.Admin_User  mdl  =  this.Session[NModel.EnObject.CurrentLoginSessionName] as NModel.Admin_User;
            string[] arrid=  (mdl.Role_Menu+"").Split(',');
            

           int[] lg= Tool.NTool.ArrayToAll<int,string>(arrid);

          

            string[] keys = this.Request.Form.AllKeys;


           _menu = dal.GetIDList(lg);

            }
            catch (Exception ex) {
            
            
            }
           
            StringBuilder sbStr = new StringBuilder();

            Tool.NJson<NModel.Admin_Menu> jstr = new Tool.NJson<NModel.Admin_Menu>();

            sbStr.Append(jstr.JsonNoLevel(SetRtMenu, SetProcessResult, _menu));



            NModel.PageData<NModel.Admin_Menu> outModel = new NModel.PageData<NModel.Admin_Menu>();
            outModel.TotalPage = 1;
            // outModel.OutData = inModel;

            outModel.OutStr = sbStr.ToString();

            dal.Close();

            return View(outModel);
       
          
        }
    [BLL.NMenuAuthorize(MenuToId = 23)]
        public ActionResult Data_Admin_Menu() {
           
            
            this.Response.ContentType = "text/plain";

            // this.Response.ContentType = "text/plain";
            // this.Response.ContentType = "text/plain";
            string page = this.Request.QueryString["page"];
            string rows = this.Request.QueryString["rows"];
            int _page = 0, _rows = 0;
            int.TryParse(page, out _page);
            int.TryParse(rows, out _rows);
            long TatalPage = 0;
            if (_page == 0)
            {
                 _page = 1;
               
            }
            if (_rows == 0) {

                _rows = 130;
            }

            BLL.Admin_Menu dal = new BLL.Admin_Menu();



            string[] keys = this.Request.Form.AllKeys;


            List<NModel.Admin_Menu> _menu = null;

            if (_page > 0)
            {
               // TatalPage = dal.GetListTotal();
                _menu = dal.GetPagerList(_page, _rows);

            }
            else
            {
                _menu = dal.GetList();
            }

            StringBuilder sbStr = new StringBuilder();

            Tool.NJson<NModel.Admin_Menu> jstr = new Tool.NJson<NModel.Admin_Menu>();

            sbStr.Append(jstr.JsonNoLevel(SetRtMenu, SetProcessResult, _menu));



            NModel.PageData<NModel.Admin_Menu> outModel = new NModel.PageData<NModel.Admin_Menu>();
            outModel.TotalPage = TatalPage;
            // outModel.OutData = inModel;

            outModel.OutStr = sbStr.ToString();

            dal.Close();

            return View(outModel);
       
           
        }
          [BLL.NMenuAuthorize(MenuToId = 127)]
        public ActionResult Data_Admin_User() {

            BLL.Admin_User dal = new BLL.Admin_User();
            long TatalPage = 1;

            string[] keys = this.Request.Form.AllKeys;
            string page = this.Request.QueryString["page"];
            string rows = this.Request.QueryString["rows"];
            int _page = 0, _rows = 0;
            int.TryParse(page, out _page);
            int.TryParse(rows, out _rows);

            if (_page == 0)
            {
                _page = 1;

            }
            if (_rows == 0)
            {

                _rows = 20;
            }

            List<NModel.Admin_User> inModel =
                dal.GetPagerList(_page, _rows);
            StringBuilder sbStr = new StringBuilder();

            if (Tool.NTool.IsLtNULL<NModel.Admin_User>(inModel))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                int lastdo = 0;
                foreach (NModel.Admin_User outmodel in inModel)
                {
                   

                    dict.Clear();
                    //自由修改
                    dict.Add("user_id", "" + outmodel.User_ID);

                    dict.Add("user_name", "" + outmodel.User_Name);

                    dict.Add("user_status", "" + outmodel.User_Status);
                    dict.Add("user_realname", "" + outmodel.User_RealName);
                    dict.Add("user_logintime", "" + outmodel.User_LoginTime);
                    dict.Add("user_role_id", "" + outmodel.User_Role_ID);
                    dict.Add("user_ip", "" + outmodel.User_Ip);
                    dict.Add("user_status_show", "" + Tool.NStr.SetStatusMsg(outmodel.User_Status, "启用", "禁用"));
                    dict.Add("user_addtime", "" + outmodel.User_AddTime);
                    dict.Add("user_edittime", "" + outmodel.User_EditTime);
                    ;
                    dict.Add("user_operate_show", "" + Tool.NStr.SetStatusMsg(outmodel.User_Operate, "可删除", "不可删"));

                    dict.Add("user_operate", "" + outmodel.User_Operate);
                    dict.Add("role_name", "" + outmodel.Role_Name);
                    //下面不用动
                    lastdo++;
                    if (lastdo >= inModel.Count)
                    {

                        sbStr.Append(Tool.NTool.ObjectToJsonStr(dict));

                    }
                    else
                    {
                        sbStr.Append(Tool.NTool.ObjectToJsonStr(dict) + ",");
                    }

                }



            }


            NModel.PageData<NModel.Admin_User> outModel = new NModel.PageData<NModel.Admin_User>();
            outModel.TotalPage = TatalPage;
            // outModel.OutData = inModel;
            outModel.OutStr = sbStr.ToString();

            dal.Close();
            return View(outModel);
        
        
        }
          [BLL.NMenuAuthorize(MenuToId = 23)]
          public ActionResult Data_Admin_Menu_New()
          {


              this.Response.ContentType = "text/plain";

              // this.Response.ContentType = "text/plain";
              // this.Response.ContentType = "text/plain";
              string page = this.Request.QueryString["page"];
              string rows = this.Request.QueryString["rows"];
              int _page = 0, _rows = 0;
              int.TryParse(page, out _page);
              int.TryParse(rows, out _rows);
              long TatalPage = 0;
              if (_page == 0)
              {
                  _page = 1;

              }
              if (_rows == 0)
              {

                  _rows = 170;
              }

              BLL.Admin_Menu dal = new BLL.Admin_Menu();



              string[] keys = this.Request.Form.AllKeys;


              List<NModel.Admin_Menu> _menu = null;

              if (_page > 0)
              {
                  // TatalPage = dal.GetListTotal();
                  _menu = dal.GetPagerList(_page, _rows);

              }
              else
              {
                  _menu = dal.GetList();
              }

              List<NModel.Admin_Menu> inModel =_menu;
              
            StringBuilder sbStr = new StringBuilder();

            if (Tool.NTool.IsLtNULL<NModel.Admin_Menu>(inModel))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                int lastdo = 0;
                foreach (NModel.Admin_Menu outmodel in inModel)
                {


                    dict.Clear();
                    //自由修改
                    dict.Add("menu_id", "" + outmodel.Menu_ID);

                    dict.Add("menu_name", "" + outmodel.Menu_Name);
                    dict.Add("menu_num", "" + outmodel.Menu_Num);

                    dict.Add("menu_url", "" + outmodel.Menu_URL);
                    dict.Add("menu_x", "" + outmodel.Menu_X);
                    dict.Add("menu_y", "" + outmodel.Menu_Y);
                    dict.Add("menu_z", "" + outmodel.Menu_Z);
                    dict.Add("menu_sortno", "" + outmodel.Menu_SortNo);
                    dict.Add("menu_status_show", "" + Tool.NStr.SetStatusMsg(outmodel.Menu_Status, "启用", "禁用"));
                    dict.Add("menu_addtime", "" + outmodel.Menu_AddTime);
                    dict.Add("menu_edittime", "" + outmodel.Menu_EditTime);
                    ;
                    dict.Add("menu_operate_show", "" + Tool.NStr.SetStatusMsg(outmodel.Menu_Operate, "可删除", "不可删"));

                    dict.Add("iconCls", "" + outmodel.Menu_Img_url);
                    dict.Add("menu_img_url", "" + outmodel.Menu_Img_url);
                    //下面不用动
                    lastdo++;
                    if (lastdo >= inModel.Count)
                    {

                        sbStr.Append(Tool.NTool.ObjectToJsonStr(dict));

                    }
                    else
                    {
                        sbStr.Append(Tool.NTool.ObjectToJsonStr(dict) + ",");
                    }

                }
            }

            NModel.PageData<NModel.Admin_User> outModel = new NModel.PageData<NModel.Admin_User>();
            outModel.TotalPage = TatalPage;
             // outModel.OutData = inModel;
            outModel.OutStr = sbStr.ToString();

              dal.Close();
              return View(outModel);


          }
          //[BLL.NMenuAuthorize(MenuToId = 127)]
          //public ActionResult Data_Admin_User()
          //{

          //    BLL.Admin_User dal = new BLL.Admin_User();
          //    long TatalPage = 1;

          //    string[] keys = this.Request.Form.AllKeys;
          //    string page = this.Request.QueryString["page"];
          //    string rows = this.Request.QueryString["rows"];
          //    int _page = 0, _rows = 0;
          //    int.TryParse(page, out _page);
          //    int.TryParse(rows, out _rows);

          //    if (_page == 0)
          //    {
          //        _page = 1;

          //    }
          //    if (_rows == 0)
          //    {

          //        _rows = 20;
          //    }

          //    List<NModel.Admin_User> inModel =
          //        dal.GetPagerList(_page, _rows);
          //    StringBuilder sbStr = new StringBuilder();

          //    if (Tool.NTool.IsLtNULL<NModel.Admin_User>(inModel))
          //    {
          //        Dictionary<string, string> dict = new Dictionary<string, string>();

          //        int lastdo = 0;
          //        foreach (NModel.Admin_User outmodel in inModel)
          //        {


          //            dict.Clear();
          //            //自由修改
          //            dict.Add("user_id", "" + outmodel.User_ID);

          //            dict.Add("user_name", "" + outmodel.User_Name);

          //            dict.Add("user_status", "" + outmodel.User_Status);
          //            dict.Add("user_realname", "" + outmodel.User_RealName);
          //            dict.Add("user_logintime", "" + outmodel.User_LoginTime);
          //            dict.Add("user_role_id", "" + outmodel.User_Role_ID);
          //            dict.Add("user_ip", "" + outmodel.User_Ip);
          //            dict.Add("user_status_show", "" + Tool.NStr.SetStatusMsg(outmodel.User_Status, "启用", "禁用"));
          //            dict.Add("user_addtime", "" + outmodel.User_AddTime);
          //            dict.Add("user_edittime", "" + outmodel.User_EditTime);
          //            ;
          //            dict.Add("user_operate_show", "" + Tool.NStr.SetStatusMsg(outmodel.User_Operate, "可删除", "不可删"));

          //            dict.Add("user_operate", "" + outmodel.User_Operate);
          //            dict.Add("role_name", "" + outmodel.Role_Name);
          //            //下面不用动
          //            lastdo++;
          //            if (lastdo >= inModel.Count)
          //            {

          //                sbStr.Append(Tool.NTool.ObjectToJsonStr(dict));

          //            }
          //            else
          //            {
          //                sbStr.Append(Tool.NTool.ObjectToJsonStr(dict) + ",");
          //            }

          //        }



          //    }


          //    NModel.PageData<NModel.Admin_User> outModel = new NModel.PageData<NModel.Admin_User>();
          //    outModel.TotalPage = TatalPage;
          //    // outModel.OutData = inModel;
          //    outModel.OutStr = sbStr.ToString();

          //    dal.Close();
          //    return View(outModel);


          //}




          [BLL.NMenuAuthorize(MenuToId = 126)]
        public ActionResult Data_Admin_Role()
        {
            // this.Response.ContentType = "text/plain";
            // this.Response.ContentType = "text/plain";
            string page = this.Request.QueryString["page"];
            string rows = this.Request.QueryString["rows"];
            int _page = 0, _rows = 0;
            int.TryParse(page, out _page);
            int.TryParse(rows, out _rows);

            if (_page == 0)
            {
                _page = 1;

            }
            if (_rows == 0)
            {

                _rows = 20;
            }


            BLL.Admin_Role dal = new BLL.Admin_Role();
           // long TatalPage = dal.GetListTotal();

            string[] keys = this.Request.Form.AllKeys;


            List<NModel.Admin_Role> inModel = dal.GetPagerList(_page, _rows);


            StringBuilder sbStr = new StringBuilder();

            if (Tool.NTool.IsLtNULL<NModel.Admin_Role>(inModel))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                int lastdo = 0;
                foreach (NModel.Admin_Role outmodel in inModel)
                {


                    dict.Clear();

                    dict.Add("role_id", "" + outmodel.Role_ID);
                    dict.Add("role_menu", ("" + outmodel.Role_Menu).Trim());
                    dict.Add("role_name", "" + outmodel.Role_Name);

                    dict.Add("role_sortno", "" + outmodel.Role_SortNo);
                    dict.Add("role_status", "" + outmodel.Role_Status);


                    dict.Add("role_status_show", "" + Tool.NStr.SetStatusMsg(outmodel.Role_Status, "启用", "禁用"));

                    dict.Add("role_edittime", "" + outmodel.Role_EditTime);
                    dict.Add("role_addtime", "" + outmodel.Role_AddTime);

                    //cw_group_key

                    //Tool.N_STR.SetStatusMsg(outmodel.Level_Status, "启用", "禁用")
                    lastdo++;
                    if (lastdo >= inModel.Count)
                    {

                        sbStr.Append(Tool.NTool.ObjectToJsonStr(dict));

                    }
                    else
                    {
                        sbStr.Append(Tool.NTool.ObjectToJsonStr(dict) + ",");
                    }

                }



            }

            NModel.PageData<NModel.Admin_Role> outModel = new NModel.PageData<NModel.Admin_Role>();
           outModel.TotalPage =1;
            // outModel.OutData = inModel;

            outModel.OutStr = sbStr.ToString();

            dal.Close();

            return View(outModel);




        }
    }
}

