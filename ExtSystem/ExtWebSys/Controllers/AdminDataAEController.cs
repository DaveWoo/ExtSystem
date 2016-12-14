using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
    public class AdminDataAEController : Controller
    {
        public ActionResult Move_Admin_Menu()
        {


            string delList = this.Request.Form["delList[]"];

            string name = this.Request.Form["id"];



            string _nlevel = this.Request.Form["nlevel"];

            long x = 0, y = 0, z = 0, nlevel;
            long.TryParse(_nlevel, out nlevel);


            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {
                    long long_Result = 0;
                    BLL.Admin_Menu dal = new BLL.Admin_Menu();
                    long num = long.Parse(name);
                    NModel.Admin_Menu model = dal.GetModel(((long?)num));
                    DataTable dtMax = null;
                    if (model != null)
                    {

                        switch (nlevel)
                        {
                            case 0:
                                {


                                    if (model.Menu_Y == 1 && model.Menu_Z == 1)
                                    {


                                        dtMax = dal.GetFristLevelMaxXModel();

                                        if (dtMax != null && dtMax.Rows.Count > 0)
                                        {
                                            long.TryParse(dtMax.Rows[0]["next_x"] + "", out x);
                                            y = (long)model.Menu_Y;
                                            z = (long)model.Menu_Z;

                                        }
                                        else
                                        {

                                            x = 1;
                                            y = 1;
                                            z = 1;
                                        }

                                    }
                                    else if (model.Menu_Z >= 2)
                                    {

                                        NModel.Admin_Menu nadm = dal.GetMaxXANDZYModel(model.Menu_X, model.Menu_Z);
                                        y = (long)(nadm.Menu_Y + 1);


                                    }


                                }


                                break;
                            case 1:
                                {
                                    string xStr = (model.Menu_X + "");

                                    xStr = xStr.Replace("-", "");
                                    long eqStr = 0;
                                    if (model.Menu_Z >= 2)
                                    {
                                        long.TryParse(string.Format("{0}{1}", xStr, model.Menu_Y), out eqStr);

                                    }
                                    else
                                    {

                                        long.TryParse(xStr + "", out eqStr);


                                    }


                                    dtMax = dal.GetXMaxYModel(eqStr);



                                    if (dtMax != null && dtMax.Rows.Count > 0)
                                    {
                                        long.TryParse(dtMax.Rows[0]["next_y"] + "", out y);

                                    }
                                    else
                                    {

                                        y = 1;
                                    }
                                    x = eqStr;
                                    z = (long)model.Menu_Z + 1;


                                    y = y > 0 ? y : 1;



                                }

                                break;
                        }


                        for (int i = 0; i < delIntArr.Length; i++)
                        {

                            long? _id = delIntArr[i];
                            NModel.Admin_Menu mdl = dal.GetModel(_id);
                            if (model.Menu_X == mdl.Menu_X &&
                                model.Menu_Y == mdl.Menu_Y &&
                                model.Menu_Z == mdl.Menu_Z
                                ) { continue; }

                            long _x = 0, _y = 0;
                            if (z > 1) { _x = x; _y = y + i; }
                            else { _x = x + i; _y = y; }









                            long_Result += dal.EditFree(delIntArr[i], string.Format("Menu_X={0},Menu_Y={1},Menu_Z={2}", _x, _y, z));

                        }
                    }

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("移动成功！总共{0}条", long_Result), "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("移动失败！您输入分类名称不存在！", "0"));
                        this.Response.End();

                    }

                    dal.Close();
                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                    this.Response.End();


                }





            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                this.Response.End();


            }




            return View();

        }
      


        [BLL.NMenuAuthorize(MenuToId = 128)]
        public ActionResult Edit_Admin_User_Pw() { 
        
            this.Response.ContentType = "text/plain";


            NModel.Admin_User model = new NModel.Admin_User();
            BLL.Admin_User dal = new BLL.Admin_User();            //admin_user_pw
            long? l_admin_user_name = BLL.Fun.GetCookieUserID();


            model = dal.GetModel(l_admin_user_name);
            string admin_user_pw = this.Request.Form["admin_user_pw"];

            string r_admin_user_pw = this.Request.Form["r_admin_user_pw"];

            string o_admin_user_pw = this.Request.Form["o_admin_user_pw"];

            // 下面代码编写
            if (model != null && !string.IsNullOrEmpty(admin_user_pw) && !string.IsNullOrEmpty(o_admin_user_pw) &&
                         !string.IsNullOrEmpty(r_admin_user_pw)&&admin_user_pw.Length>5&&admin_user_pw.Equals(r_admin_user_pw)
                           &&o_admin_user_pw.Length > 5


                )
            {
                



                    NModel.Admin_User cModel_User = dal.ExistsNamePw((model.User_Name + "").ToLower(), Tool.NMd5.GetMd5Hash(o_admin_user_pw).ToLower());
                    if (cModel_User!=null&&cModel_User.User_ID > 0)
                    {
                        bool isEdit = dal.EditPW(model.User_Name, r_admin_user_pw);



                        string msg = dal.ErrorMsg;
                        if (!string.IsNullOrEmpty(msg))
                        {

                            msg = "原因：" + msg;

                        }


                        if (isEdit)
                        {
                           bool isLogin=  BLL.Fun.ClearLogin();


                            this.Response.Write(Tool.NMsg.SetMsg("修改成功", "1"));
                            this.Response.End();
                        }
                        else
                        {
                            this.Response.Write(Tool.NMsg.SetMsg("修改失败" + msg, "0"));
                            this.Response.End();

                        }
                    }
                    else {
                       
                        this.Response.Write(Tool.NMsg.SetMsg("修改失败,旧密码不正确", "0"));
                        this.Response.End();
                    
                    }

                    dal.Close();



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                this.Response.End();
            }
            dal.Close();
            return View();

            ;

        }
        public ActionResult Index()
        {

           
            return View();
        }

          [BLL.NMenuAuthorize(MenuToId = 127)]
        public ActionResult Add_Admin_User()
        {
            this.Response.ContentType = "text/plain";

            NModel.Admin_User model = new NModel.Admin_User();
            BLL.Admin_User dal = new BLL.Admin_User();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.Admin_User>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.User_Name) &&
                    !string.IsNullOrEmpty(model.User_PassWord) &&
                     model.User_PassWord.Length > 5 &&
                     model.User_Name.Length > 1

                )
            {





                if (dal.ExistsName(model.User_Name) > 0)
                {

                    this.Response.Write(Tool.NMsg.SetMsg("用户存在", "0"));

                    this.Response.End();

                }
                else
                {

                    model.User_AddTime =DateTime.Parse( DateTime.Now.ToString("s"));
                    model.User_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    model.User_LoginTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    model.User_PassWord = Tool.NMd5.GetMd5Hash(model.User_PassWord);
                    //
                    bool isAdd = dal.Add(model)>0?true:false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败", "1"));
                        this.Response.End();
                    }
                }




                dal.Close();



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));

            }







            return View();


        }

          [BLL.NMenuAuthorize(MenuToId = 127)]
        public ActionResult Del_Admin_User()
        {

            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.Admin_User dal = new BLL.Admin_User();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除-{0}条", long_Result), "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("删除失败！", "0"));
                        this.Response.End();

                    }
                    dal.Close();

                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                    this.Response.End();


                }



             

            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                this.Response.End();


            }


           

            return View();



        }
          [BLL.NMenuAuthorize(MenuToId = 127)]
        public ActionResult Edit_Admin_User()
        {
            this.Response.ContentType = "text/plain";







            NModel.Admin_User model = new NModel.Admin_User();
            BLL.Admin_User dal = new BLL.Admin_User();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.Admin_User>(ref model, ref JsonStr, "");

            if (isGet &&
               !string.IsNullOrEmpty(model.User_Name) &&
                model.User_Role_ID > 0

               )
            {
                //取默认值

                NModel.Admin_User oldModel = dal.GetModel(model.User_ID);
                model.User_AddTime = oldModel.User_AddTime;
                //post 请求过来密码为空，设置数据库原密码
                if (string.IsNullOrEmpty(model.User_PassWord))
                {


                    model.User_PassWord = oldModel.User_PassWord;

                }
                else
                {

                    model.User_PassWord = Tool.NMd5.GetMd5Hash(model.User_PassWord);

                }

                model.User_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                model.User_LoginTime = DateTime.Parse(DateTime.Now.ToString("s"));
                if (model.User_PassWord.Length > 5)
                {

                    bool isEdit = dal.Edit(model)>0?true:false;



                    string msg = dal.Msg;
                    if (!string.IsNullOrEmpty(msg))
                    {

                        msg = "原因：" + msg;

                    }


                    if (isEdit)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("修改成功", "1"));
                        this.Response.End();

                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("修改失败" + msg, "0"));
                        this.Response.End();

                    }

                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                    this.Response.End();


                }


                dal.Close();


            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                this.Response.End();
            }

            return View();



        }
        [BLL.NMenuAuthorize(MenuToId = 126)]
        public ActionResult Edit_Admin_Role()
        {
            this.Response.ContentType = "text/plain";






            NModel.Admin_Role model = new NModel.Admin_Role();
            BLL.Admin_Role dal = new BLL.Admin_Role();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.Admin_Role>(ref model, ref JsonStr, "");

            if (isGet &&
               !string.IsNullOrEmpty(model.Role_Name) &&
               !string.IsNullOrEmpty(model.Role_Menu) && !string.IsNullOrEmpty(model.Role_SortNo + "") &&
               !string.IsNullOrEmpty(model.Role_Status + "") &&
                model.Role_AddTime != null &&
                model.Role_ID > 0



               )
            {
                bool isEdit = dal.Edit(model)>0?true:false;
                string msg = dal.ErrorMsg;

                if (isEdit)
                {

                    this.Response.Write(Tool.NMsg.SetMsg("修改成功", "1"));
                    this.Response.End();

                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("修改失败", "0"));
                    this.Response.End();

                }


            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                this.Response.End();
            }
            dal.Close();
            return View();


        }
        [BLL.NMenuAuthorize(MenuToId = 126)]
        public ActionResult Add_Admin_Role()
        {
            this.Response.ContentType = "text/plain";

            NModel.Admin_Role model = new NModel.Admin_Role();
            BLL.Admin_Role dal = new BLL.Admin_Role();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.Admin_Role>(ref model, ref JsonStr, "");
            if (isGet &&
                !string.IsNullOrEmpty(model.Role_Name) &&
                !string.IsNullOrEmpty(model.Role_Menu) && !string.IsNullOrEmpty(model.Role_SortNo + "") &&
                !string.IsNullOrEmpty(model.Role_Status + "")
                )
            {

                model.Role_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                model.Role_EditTime = DateTime.Parse(DateTime.Now.ToString("s")); ;
                bool isAdd = dal.Add(model)>0?true:false;
                if (isAdd)
                {
                    this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));

                }
                else
                {

                    this.Response.Write(Tool.NMsg.SetMsg("增加失败", "0"));
                }




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                this.Response.End();
            }




            dal.Close();


            return View();
        }
          [BLL.NMenuAuthorize(MenuToId = 126)]
        public ActionResult Del_Admin_Role()
        {
            // 'choices[]': ["Jon", "Susan"] }

            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.Admin_Role dal = new BLL.Admin_Role();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除-{0}条", long_Result), "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("删除失败！", "0"));
                        this.Response.End();

                    }
                    dal.Close();

                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                    this.Response.End();


                }

              



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                this.Response.End();


            }




            return View();


        }

          [BLL.NMenuAuthorize(MenuToId = 23)]
        public ActionResult Add_Admin_Menu()
        {

            this.Response.ContentType = "text/plain";
            string level = this.Request.Form["level"];//0顶级1下一级


            byte bLevel = 0;
            byte.TryParse(level, out bLevel);

            if (bLevel == 2)
            {
                this.Response.Write(Tool.NMsg.SetMsg("请选择加级", "0"));

                return View();
            }

    


            NModel.Admin_Menu model = new NModel.Admin_Menu();
           BLL.Admin_Menu dal = new BLL.Admin_Menu();
           dal.Open();

            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.Admin_Menu>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && model != null && !string.IsNullOrEmpty(model.Menu_Name)&&!string.IsNullOrEmpty( model.Menu_Img_url)
                && model.Menu_X != null && model.Menu_X>0&& model.Menu_Y > 0 && model.Menu_Z > 0
                )
            {
                string Xstr = (model.Menu_X + "");

                long xOutLong = 0;
                long? zOutLong = 0;
                long yOutLong = 0;
                long? xLong = 0;
                DataTable dtMax = null;
                if (bLevel == 0)
                {



                    if (model.Menu_Y == 1 && model.Menu_Z == 1)
                    {


                        dtMax = dal.GetFristLevelMaxXModel();

                        if (dtMax != null && dtMax.Rows.Count > 0)
                        {
                            long.TryParse(dtMax.Rows[0]["next_x"] + "", out xOutLong);

                            model.Menu_X = xOutLong;
                        }
                        else
                        {

                            xOutLong = 1;
                            yOutLong = 1;
                            zOutLong = 1;
                        }

                    }
                    else if (model.Menu_Z >= 2)
                    {

                        NModel.Admin_Menu nadm = dal.GetMaxXANDZYModel(model.Menu_X, model.Menu_Z);
                        model.Menu_Y = (long)(nadm.Menu_Y + 1);


                    }



                }
                else if (bLevel == 1)
                {



                    string xStr = (model.Menu_X + "");

                    xStr = xStr.Replace("-", "");
                    long eqStr = 0;
                    if (model.Menu_Z >= 2)
                    {
                        long.TryParse(string.Format("{0}{1}", xStr, model.Menu_Y), out eqStr);

                    }
                    else
                    {

                        long.TryParse(xStr + "", out eqStr);


                    }


                    dtMax = dal.GetXMaxYModel(eqStr);



                    if (dtMax != null && dtMax.Rows.Count > 0)
                    {
                        long.TryParse(dtMax.Rows[0]["next_y"] + "", out yOutLong);

                    }
                    else
                    {

                        yOutLong = 1;
                    }
                    xOutLong = eqStr;
                    zOutLong = model.Menu_Z + 1;

                    model.Menu_X = xOutLong;
                    model.Menu_Y = yOutLong > 0 ? yOutLong : 1;
                    model.Menu_Z = zOutLong;



                }
                //else if (bLevel == 0)
                //{

                //    xLong = xOutLong = model.Menu_X != null ? Convert.ToInt64(model.Menu_X) : 1;


                //    if (model.Menu_Y == 1 && model.Menu_Z == 1)
                //    {


                //        model.Menu_X = xOutLong;
                //        model.Menu_Y = yOutLong > 0 ? yOutLong : 1;
                //        model.Menu_Z = zOutLong;
                //    }

                //    dtMax = dal.GetXMaxYModel(model.Menu_X);

                //    if (dtMax != null && dtMax.Rows.Count > 0)
                //    {
                //        long.TryParse(dtMax.Rows[0]["next_y"] + "", out yOutLong);

                //    }

                //    zOutLong = model.Menu_Z;





                //}
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("请选择加级", "0"));
                    return View();
                
                }





                model.Menu_AddTime = DateTime.Parse(DateTime.Now.ToString("s")); ;
                model.Menu_EditTime = DateTime.Parse(DateTime.Now.ToString("s")); ;


                long isAdd = dal.Add(model);
                if (isAdd > 0)
                {
                    this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));

                }
                else
                {

                    this.Response.Write(Tool.NMsg.SetMsg("增加失败", "0"));
                }
                dal.Close();



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                this.Response.End();
            }


            dal.Close();


            return View();

        }

          [BLL.NMenuAuthorize(MenuToId = 23)]
        public ActionResult Edit_Admin_Menu() {
          




            this.Response.ContentType = "text/plain";
            string level = this.Request.Form["level"];//0顶级1下一级
            string menu_cc = this.Request.Form["menu_cc"];


            byte bLevel = 0;
            byte.TryParse(level, out bLevel);





            NModel.Admin_Menu model = new NModel.Admin_Menu();
           BLL.Admin_Menu dal = new BLL.Admin_Menu();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.Admin_Menu>(ref model, ref JsonStr, "");


            // 下面代码编写 && !string.IsNullOrEmpty(model.Menu_Img_url)
            if (isGet && model != null && !string.IsNullOrEmpty(model.Menu_Name)
                && model.Menu_X != null && model.Menu_X > 0 && model.Menu_Y > 0 && model.Menu_Z > 0
                )
            {


                string Xstr = (model.Menu_X + "");

                long xOutLong = 0;
                long? zOutLong = 0;
                long yOutLong = 0;
                long? xLong = 0;
                DataTable dtMax = null;
                if (bLevel == 0)
                {



                    if (model.Menu_Y == 1 && model.Menu_Z == 1)
                    {


                        dtMax = dal.GetFristLevelMaxXModel();

                        if (dtMax != null && dtMax.Rows.Count > 0)
                        {
                            long.TryParse(dtMax.Rows[0]["next_x"] + "", out xOutLong);

                            model.Menu_X = xOutLong;
                        }
                        else
                        {

                            xOutLong = 1;
                            yOutLong = 1;
                            zOutLong = 1;
                        }

                    }
                    else if (model.Menu_Z >= 2)
                    {

                        NModel.Admin_Menu nadm = dal.GetMaxXANDZYModel(model.Menu_X, model.Menu_Z);
                        model.Menu_Y = (long)(nadm.Menu_Y + 1);


                    }



                }
                else if (bLevel == 1)
                {



                    string xStr = (model.Menu_X + "");

                    xStr = xStr.Replace("-", "");
                    long eqStr = 0;
                    if (model.Menu_Z >= 2)
                    {
                        long.TryParse(string.Format("{0}{1}", xStr, model.Menu_Y), out eqStr);

                    }
                    else
                    {

                        long.TryParse(xStr + "", out eqStr);


                    }


                    dtMax = dal.GetXMaxYModel(eqStr);



                    if (dtMax != null && dtMax.Rows.Count > 0)
                    {
                        long.TryParse(dtMax.Rows[0]["next_y"] + "", out yOutLong);

                    }
                    else
                    {

                        yOutLong = 1;
                    }
                    xOutLong = eqStr;
                    zOutLong = model.Menu_Z + 1;

                    model.Menu_X = xOutLong;
                    model.Menu_Y = yOutLong > 0 ? yOutLong : 1;
                    model.Menu_Z = zOutLong;



                }
                else if (bLevel == 0)
                {

                    xLong = xOutLong = model.Menu_X != null ? Convert.ToInt64(model.Menu_X) : 1;


                    if (model.Menu_Y == 1 && model.Menu_Z == 1)
                    {


                        model.Menu_X = xOutLong;
                        model.Menu_Y = yOutLong > 0 ? yOutLong : 1;
                        model.Menu_Z = zOutLong;
                    }

                    dtMax = dal.GetXMaxYModel(model.Menu_X);

                    if (dtMax != null && dtMax.Rows.Count > 0)
                    {
                        long.TryParse(dtMax.Rows[0]["next_y"] + "", out yOutLong);

                    }

                    zOutLong = model.Menu_Z;





                }



              






                model.Menu_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));

                bool isEdit =dal.Edit(model)>0? true:false;

                if (isEdit)
                {

                    this.Response.Write(Tool.NMsg.SetMsg("修改成功", "1"));
                    this.Response.End();

                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("修改失败", "0"));
                    this.Response.End();

                }


            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整", "0"));
                this.Response.End();
            }
            dal.Close();

            return View();
        
        }

          [BLL.NMenuAuthorize(MenuToId = 127)]
        public ActionResult Del_All_Admin_User()
        {


            BLL.Admin_User bll = new BLL.Admin_User();
            long delNumber = bll.DelAll();

            if (delNumber > 0)
            {

                this.Response.Write(Tool.NMsg.SetMsg("删除成功", "1"));
                this.Response.End();

            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("删除失败", "0"));
                this.Response.End();

            }
            bll.Close();
            return View();
        }
          [BLL.NMenuAuthorize(MenuToId = 126)]
        public ActionResult Del_All_Admin_Role()
        {


            BLL.Admin_Role bll = new BLL.Admin_Role();
            long delNumber = bll.DelAll();

            if (delNumber > 0)
            {

                this.Response.Write(Tool.NMsg.SetMsg("删除成功", "1"));
                this.Response.End();

            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("删除失败", "0"));
                this.Response.End();

            }
            bll.Close();
            return View();
        }

        public ActionResult Del_All_Admin_Menu() {


            BLL.Admin_Menu bll = new BLL.Admin_Menu();
            long delNumber= bll.DelAll();
            
                if (delNumber>0)
                {

                    this.Response.Write(Tool.NMsg.SetMsg("删除成功", "1"));
                    this.Response.End();

                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("删除失败", "0"));
                    this.Response.End();

                }
            bll.Close();
            return View();
        }
        [BLL.NMenuAuthorize(MenuToId = 23)]
        public ActionResult Del_Admin_Menu()
        {
            // 'choices[]': ["Jon", "Susan"] }

            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];

            if (string.IsNullOrEmpty(delList)) {

                delList = this.Request.QueryString["id"];            
            }
            

            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');





                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);

                Dictionary<long, bool> dictIT = new Dictionary<long, bool>();
                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.Admin_Menu dal = new BLL.Admin_Menu();
                    List<NModel.Admin_Menu> listModel = new List<NModel.Admin_Menu>();

                    long oldValue = 0;
                    foreach (int id in delIntArr)
                    {

                        NModel.Admin_Menu cModel = dal.GetModel(id);

                        if (cModel == null)
                        {
                            continue;

                        }

                        long eqStr = 0, elStr = 0;
                        string xStr = (cModel.Menu_X + "");
                        xStr = xStr.Replace("-", "");
                        long.TryParse(xStr + "", out elStr);



                        if (cModel.Menu_X > 0)
                        {
                            long.TryParse(string.Format("{0}{1}", xStr, cModel.Menu_Y), out eqStr);
                            //long.TryParse(xStr, out elStr);
                        }
                        else
                        {

                            long.TryParse(xStr + "", out eqStr);


                        }



                        int isFg = dal.ExistsX(eqStr);

                        if (isFg > 0)
                        {

                            if (!dictIT.ContainsKey(eqStr))
                            {
                                dictIT.Add(eqStr, false);

                            }



                        }
                        if (dictIT.ContainsKey(oldValue)
                            // || dictIT.ContainsKey(elStr))
                            && cModel.Menu_Operate >= 1)
                        {
                            dictIT.Remove(elStr);

                        }
                        oldValue = eqStr;




                    }


                    if (dictIT.Count <= 0)
                    {
                        long long_Result = dal.DelList(delIntArr);


                        if (long_Result > 0)
                        {
                            this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除({0})条", long_Result), "1"));
                            this.Response.End();
                        }
                        else
                        {
                            this.Response.Write(Tool.NMsg.SetMsg("删除失败！或不可删除", "0"));
                            this.Response.End();

                        }
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("请勾选中菜单的下级或不可删除", "0"));
                        this.Response.End();

                    }
                    dal.Close();
                }
                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                    this.Response.End();


                }


               


            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败/填写数据不完整！", "0"));
                this.Response.End();


            }


           

            return View();



        }


    
    
    }
}
