using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
    public class DBDataAEController : Controller
    {
        public ActionResult Del_DB_Classify()
        {
            

            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);




                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_Classify dal = new BLL.DB_Classify();

                    bool isGO = true;
                    long hasID = 0;
                    for (int i = 0; i < delIntArr.Length; i++)
                    {
                        long isHas = dal.HasNextLevelByID(delIntArr[i]);

                        if (isHas > 0)
                        {
                            isGO = false;
                            hasID = delIntArr[i];
                            break;
                        }

                    }

                    if (isGO)
                    {



                        long long_Result = dal.DelList(delIntArr);

                        if (long_Result > 0)
                        {
                            this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
                            this.Response.End();
                        }
                        else
                        {
                            this.Response.Write(Tool.NMsg.SetMsg("删除失败！", "0"));
                            this.Response.End();

                        }
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("删除失败！有下级菜单 请删除下级菜单 ID 为 " + hasID, "0"));
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


        public ActionResult Edit_DB_Classify()
        {

            this.Response.ContentType = "text/plain";


            string classify_num = this.Request.QueryString["Num"];

            NModel.DB_Classify model = new NModel.DB_Classify();
            BLL.DB_Classify dal = new BLL.DB_Classify();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Classify>(ref model, ref JsonStr, "");
            model.Classify_Num = classify_num;

            // 下面代码编写
            if (isGet && model.Classify_AddTime != null &&
                    !string.IsNullOrEmpty(model.Classify_Name)&&
                        !string.IsNullOrEmpty(model.Classify_Num)
                      && model.Classify_ID > 0



                )
            {
                //取默认值


                model.Classify_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.EditFree(model, string.Format(" 'Classify_ID={0}'", model.Classify_ID)) > 0 ? true : false;

               

                string msg = dal.ErrorMsg;
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


                dal.Close();




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }
            dal.Close();
            return View();

        }
        public ActionResult Del_All_DB_Classify()
        {


            string Num=this.Request.QueryString["Num"];


            if (!string.IsNullOrEmpty(Num))
            {

                BLL.DB_Classify bll = new BLL.DB_Classify();
                long delNumber = bll.DelAllByNum(Num);

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
            }
            else {

                this.Response.Write(Tool.NMsg.SetMsg("删除失败Num==null", "0"));
                this.Response.End();
            
            }
           
            return View();
        }
        public ActionResult Add_DB_Classify()
        {
            byte bLevel = 0;
            this.Response.ContentType = "text/plain";
            string level = "0";

            //this.Request.Form["level"];//0顶级1下一级


            string classify_num = this.Request.QueryString["Num"];







            NModel.DB_Classify model = new NModel.DB_Classify();
            BLL.DB_Classify dal = new BLL.DB_Classify();
            dal.Open();

            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Classify>(ref model, ref JsonStr, "");
            model.Classify_Num = classify_num;
           
            if (model.Classify_X == null || model.Classify_X <= 0 && model.Classify_Y <= 0 && model.Classify_Z <= 0)
            {

                model.Classify_X = 1;
                model.Classify_Y = 1;
                model.Classify_Z = 1;
            }
            else
            {

                level = "1";
                model.Classify_Z = model.Classify_Z - 1;
            }

            byte.TryParse(level, out bLevel);


            // 下面代码编写
            if (isGet && model != null && !string.IsNullOrEmpty(model.Classify_Name)&&!string.IsNullOrEmpty(model.Classify_Num)
                && model.Classify_X != null && model.Classify_X > 0 && model.Classify_Y > 0 && model.Classify_Z > 0
                )
            {
                string Xstr = (model.Classify_X + "");

                long xOutLong = 0;
                long? zOutLong = 0;
                long yOutLong = 0;
                long? xLong = 0;
                DataTable dtMax = null;
                if (bLevel == 0)
                {



                    if (model.Classify_Y == 1 && model.Classify_Z == 1)
                    {


                        dtMax = dal.GetFristLevelMaxXModel();

                        if (dtMax != null && dtMax.Rows.Count > 0)
                        {
                            long.TryParse(dtMax.Rows[0]["next_x"] + "", out xOutLong);

                            model.Classify_X = xOutLong;
                        }
                        else
                        {

                            xOutLong = 1;
                            yOutLong = 1;
                            zOutLong = 1;
                        }

                    }
                    else if (model.Classify_Z >= 2)
                    {

                        NModel.DB_Classify nadm = dal.GetMaxXANDZYModel(model.Classify_X, model.Classify_Z);
                        model.Classify_Y = (long)(nadm.Classify_Y + 1);


                    }



                }
                else if (bLevel == 1)
                {



                    string xStr = (model.Classify_X + "");

                    xStr = xStr.Replace("-", "");
                    long eqStr = 0;
                    if (model.Classify_Z >= 2)
                    {
                        long.TryParse(string.Format("{0}{1}", xStr, model.Classify_Y), out eqStr);

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
                    zOutLong = model.Classify_Z + 1;

                    model.Classify_X = xOutLong;
                    model.Classify_Y = yOutLong > 0 ? yOutLong : 1;
                    model.Classify_Z = zOutLong;



                }

                else
                {
                    this.Response.Write(Tool.NMsg.SetMsg("请选择加级", "0"));
                    return View();

                }





                model.Classify_AddTime = DateTime.Parse(DateTime.Now.ToString("s")); ;
                model.Classify_EditTime = DateTime.Parse(DateTime.Now.ToString("s")); ;


                long isAdd = dal.AddFree(model);
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

        //end

 
        public ActionResult Add_DB_WebConfig()
        {


            this.Response.ContentType = "text/plain";
            NModel.DB_WebConfig model = new NModel.DB_WebConfig();
            BLL.DB_WebConfig dal = new BLL.DB_WebConfig();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_WebConfig>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet 
                //&& !string.IsNullOrEmpty(model.WebConfig_Code) &&
                //    !string.IsNullOrEmpty(model.WebConfig_Description)
                // &&!string.IsNullOrEmpty(model.WebConfig_Title)
                //&&!string.IsNullOrEmpty(model.WebConfig_Keywords
              


                )
            {





                {
                    model.WebConfig_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    model.WebConfig_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    //
                    bool isAdd = dal.AddFree(model) > 0 ? true : false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败" + dal.ErrorMsg, "1"));
                        this.Response.End();
                    }



                }








            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));

            }



            dal.Close();



            return View();
        }
        public ActionResult Del_All_DB_WebConfig()
        {



            BLL.DB_WebConfig bll = new BLL.DB_WebConfig();
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
        public ActionResult Del_All_DB_File_Category()
        {



            BLL.DB_File_Category bll = new BLL.DB_File_Category();
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


        public ActionResult Del_DB_WebConfig()
        {


            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_WebConfig dal = new BLL.DB_WebConfig();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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

        


        public ActionResult Edit_DB_WebConfig()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_WebConfig model = new NModel.DB_WebConfig();
            BLL.DB_WebConfig dal = new BLL.DB_WebConfig();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_WebConfig>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet &&
                
                //&& !string.IsNullOrEmpty(model.WebConfig_Code) &&
                //    !string.IsNullOrEmpty(model.WebConfig_Title) && !
                //    string.IsNullOrEmpty(model.WebConfig_Keywords) &&
                //    !string.IsNullOrEmpty(model.WebConfig_Description) &&
                //// !string.IsNullOrEmpty(model.File_Category_Size)&&
                          (model.WebConfig_ID > 0)


                )
            {
                //取默认值


                model.WebConfig_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.EditFree(model,(long)model.WebConfig_ID) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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


                dal.Close();




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }

            return View();

        }
        public ActionResult Del_DB_File_Category()
        {


            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_File_Category dal = new BLL.DB_File_Category();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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


        public ActionResult Edit_DB_File_Category()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_File_Category model = new NModel.DB_File_Category();
            BLL.DB_File_Category dal = new BLL.DB_File_Category();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_File_Category>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.File_Category_Name) &&
                    !string.IsNullOrEmpty(model.File_Category_Folder) &&
                // !string.IsNullOrEmpty(model.File_Category_Size)&&
                          (model.File_Category_ID > 0)


                )
            {
                //取默认值


                model.File_Category_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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


                dal.Close();




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }

            return View();

        }

        public ActionResult Add_DB_File_Category()
        {


            this.Response.ContentType = "text/plain";
            NModel.DB_File_Category model = new NModel.DB_File_Category();
            BLL.DB_File_Category dal = new BLL.DB_File_Category();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_File_Category>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.File_Category_Name) &&
                    !string.IsNullOrEmpty(model.File_Category_Folder)
                //&&!string.IsNullOrEmpty(model.File_category_Size)


                )
            {





                {

                    model.File_Category_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    //
                    bool isAdd = dal.Add(model) > 0 ? true : false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败" + dal.ErrorMsg, "1"));
                        this.Response.End();
                    }


               
                }








            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));

            }



            dal.Close();



            return View();
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Del_All_DB_File()
        {






            BLL.DB_File bll = new BLL.DB_File();
            long delNumber = bll.DelAll();
            Tool.NImage.DelFile(UploadFileController.DictFilePath);
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
        public ActionResult Del_All_DB_Image() {






            BLL.DB_Image bll = new BLL.DB_Image();
            long delNumber = bll.DelAll();
            Tool.NImage.DelFile(UploadFileController.DFPath);
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

        public ActionResult Del_All_DB_Img_Category()
        {



            BLL.DB_Img_Category bll = new BLL.DB_Img_Category();
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




        public ActionResult Del_DB_Image() {

            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    int[] delOkImage = new int[delIntArr.Length];
                    string[] delImage = new string[delIntArr.Length];
                    BLL.DB_Image dal = new BLL.DB_Image();
                    int lt = 0, dt = 0;
                    foreach (int it in delIntArr)
                    {

                        NModel.DB_Image cdi = dal.GetModel(it);
                        if (cdi != null && !string.IsNullOrEmpty(cdi.Image_Url))
                        {


                            delImage[lt] = UploadFileController.DFPath + cdi.Image_Url;
                            bool isdelOk = Tool.NImage.DelImage(delImage[lt]);

                            if (isdelOk) { delOkImage[dt] = it; dt++; }
                            lt++;


                        }

                    }

                    
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {


                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败！", "0"));
                    this.Response.End();


                }





            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败！", "0"));
                this.Response.End();


            }

      

            return View();
        
        }
        public ActionResult Del_DB_Img_Category()
        {


            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_Img_Category dal = new BLL.DB_Img_Category();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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


        public ActionResult Edit_DB_Img_Category()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_Img_Category model = new NModel.DB_Img_Category();
            BLL.DB_Img_Category dal = new BLL.DB_Img_Category();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Img_Category>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.Img_category_Name) &&
                    !string.IsNullOrEmpty(model.Img_category_Folder) &&
                // !string.IsNullOrEmpty(model.Img_category_Size)&&
                          (model.Img_category_ID > 0)


                )
            {
                //取默认值


                model.Img_category_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }
            dal.Close();
            return View();

        }
        public ActionResult Edit_DB_Image()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_Image model = new NModel.DB_Image();
            BLL.DB_Image dal = new BLL.DB_Image();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Image>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.Image_Name) &&
                    !string.IsNullOrEmpty(model.Image_Url) &&
                
                          (model.Image_Category_ID>0)


                )
            {
                //取默认值


                model.Image_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }

            dal.Close();
            return View();

        }


        public ActionResult Edit_DB_File()
        {


            this.Response.ContentType = "text/plain";

            NModel.DB_File model = new NModel.DB_File();
            BLL.DB_File dal = new BLL.DB_File();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_File>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.File_Name) &&
                    !string.IsNullOrEmpty(model.File_URL) &&

                          (model.File_Cty_ID > 0)


                )
            {
                //取默认值


                model.File_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }
            dal.Close();

            return View();

        }
        public ActionResult Add_DB_Img_Category()
        {


            this.Response.ContentType = "text/plain";
            NModel.DB_Img_Category model = new NModel.DB_Img_Category();
            BLL.DB_Img_Category dal = new BLL.DB_Img_Category();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Img_Category>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.Img_category_Name) &&
                    !string.IsNullOrEmpty(model.Img_category_Folder)
                //&&!string.IsNullOrEmpty(model.Img_category_Size)


                )
            {





                {

                    model.Img_category_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    //
                    bool isAdd = dal.Add(model) > 0 ? true : false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败" + dal.ErrorMsg, "1"));
                        this.Response.End();
                    }


                  
                }








            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));

            }

            dal.Close();





            return View();
        }


        public ActionResult Del_DB_File()
        {

            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    int[] delOkImage = new int[delIntArr.Length];
                    string[] delImage = new string[delIntArr.Length];
                    BLL.DB_File dal = new BLL.DB_File();
                    int lt = 0, dt = 0;
                    foreach (int it in delIntArr)
                    {

                        NModel.DB_File cdi = dal.GetModel((long?)it);
                        if (cdi != null && !string.IsNullOrEmpty(cdi.File_URL))
                        {


                            delImage[lt] = UploadFileController.DictFilePath + cdi.File_URL;
                            bool isdelOk = Tool.NImage.DelImage(delImage[lt]);

                            if (isdelOk) { delOkImage[dt] = it; dt++; }
                            lt++;


                        }

                    }


                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {


                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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
                    this.Response.Write(Tool.NMsg.SetMsg("提交失败！", "0"));
                    this.Response.End();


                }





            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败！", "0"));
                this.Response.End();


            }



            return View();

        }
    
        public ActionResult Del_All_DB_Link()
        {

            string num=this.Request.QueryString["Num"];

            BLL.DB_Link bll = new BLL.DB_Link();
            long delNumber = bll.DelAllByNum(num);

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
        public ActionResult Add_DB_Link()
        {


            this.Response.ContentType = "text/plain";
            NModel.DB_Link model = new NModel.DB_Link();
            BLL.DB_Link dal = new BLL.DB_Link();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Link>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.Link_Name)
                && !string.IsNullOrEmpty(model.Link_Num)
               
                 && !string.IsNullOrEmpty(model.Link_Url)


                )
            {





                {
                    model.Link_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    model.Link_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    //
                    bool isAdd = dal.Add(model) > 0 ? true : false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败" + dal.ErrorMsg, "1"));
                        this.Response.End();
                    }



                }



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));

            }



            dal.Close();



            return View();
        }

        public ActionResult Del_DB_Link()
        {


            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_Link dal = new BLL.DB_Link();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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
        public ActionResult Edit_DB_Link()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_Link model = new NModel.DB_Link();
            BLL.DB_Link dal = new BLL.DB_Link();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_Link>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.Link_Name)
                && !string.IsNullOrEmpty(model.Link_Num)

                 && !string.IsNullOrEmpty(model.Link_Url)
                 &&model.Link_AddTime!=null



                )
            {
                //取默认值


                model.Link_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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


                dal.Close();




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }

            return View();

        }

      
        

        public ActionResult Edit_DB_News()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_News model = new NModel.DB_News();
            BLL.DB_News dal = new BLL.DB_News();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_News>(ref model, ref JsonStr, "");



            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.News_Content)
                && !string.IsNullOrEmpty(model.News_Num)
                 && !string.IsNullOrEmpty(model.News_Title)

                  && !string.IsNullOrEmpty(model.News_ClassifyName)

                        &&model.News_AddTime!=null

                )
            {
                //取默认值


                model.News_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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


                dal.Close();




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }

            return View();

        }
        public ActionResult Add_DB_News()
        {


            this.Response.ContentType = "text/plain";
            NModel.DB_News model = new NModel.DB_News();
            BLL.DB_News dal = new BLL.DB_News();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_News>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.News_Content)
                && !string.IsNullOrEmpty(model.News_Num)
                 && !string.IsNullOrEmpty(model.News_Title) 

                  && !string.IsNullOrEmpty(model.News_ClassifyName)
            


                )
            {





                {
                    model.News_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    model.News_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    //
                    bool isAdd = dal.Add(model) > 0 ? true : false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败" + dal.ErrorMsg, "1"));
                        this.Response.End();
                    }



                }



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));

            }



            dal.Close();



            return View();
        }
        public ActionResult Del_All_DB_News()
        {

            string num = this.Request.QueryString["Num"];

            BLL.DB_News bll = new BLL.DB_News();
            long delNumber = bll.DelAllByNum(num);

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

        public ActionResult Del_DB_News()
        {


            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_News dal = new BLL.DB_News();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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









        public ActionResult Edit_DB_AD()
        {


            this.Response.ContentType = "text/plain";




            NModel.DB_AD model = new NModel.DB_AD();
            BLL.DB_AD dal = new BLL.DB_AD();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_AD>(ref model, ref JsonStr, "");



            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.AD_Name)
                && !string.IsNullOrEmpty(model.AD_Num)

                  && !string.IsNullOrEmpty(model.AD_Img)

                        && model.AD_AddTime != null

                )
            {
                //取默认值


                model.AD_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));




                bool isEdit = dal.Edit(model) > 0 ? true : false;



                string msg = dal.ErrorMsg;
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


                dal.Close();




            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));
                this.Response.End();
            }

            return View();

        }
        public ActionResult Add_DB_AD()
        {


            this.Response.ContentType = "text/plain";
            NModel.DB_AD model = new NModel.DB_AD();
            BLL.DB_AD dal = new BLL.DB_AD();


            String JsonStr = "";

            bool isGet = Tool.NStr.PostForSetVal<NModel.DB_AD>(ref model, ref JsonStr, "");


            // 下面代码编写
            if (isGet && !string.IsNullOrEmpty(model.AD_Name)
                && !string.IsNullOrEmpty(model.AD_Num)

                  && !string.IsNullOrEmpty(model.AD_Img)

                      

                )
            {





                {
                    model.AD_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    model.AD_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                    //
                    bool isAdd = dal.Add(model) > 0 ? true : false;

                    if (isAdd)
                    {

                        this.Response.Write(Tool.NMsg.SetMsg("增加成功", "1"));
                        this.Response.End();
                    }
                    else
                    {
                        this.Response.Write(Tool.NMsg.SetMsg("增加失败" + dal.ErrorMsg, "1"));
                        this.Response.End();
                    }



                }



            }
            else
            {
                this.Response.Write(Tool.NMsg.SetMsg("提交失败", "0"));

            }



            dal.Close();



            return View();
        }
        public ActionResult Del_All_DB_AD()
        {

            string num = this.Request.QueryString["Num"];

            BLL.DB_AD bll = new BLL.DB_AD();
            long delNumber = bll.DelAllByNum(num);

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

        public ActionResult Del_DB_AD()
        {


            this.Response.ContentType = "text/plain";

            string delList = this.Request.Form["delList[]"];




            if (!string.IsNullOrEmpty(delList))
            {
                string[] arrStr = delList.Split(',');






                int[] delIntArr = Tool.NTool.ArrayToAll<int, string>(arrStr);


                if (Tool.NTool.IsArrNULL<int>(delIntArr))
                {

                    BLL.DB_AD dal = new BLL.DB_AD();
                    long long_Result = dal.DelList(delIntArr);

                    if (long_Result > 0)
                    {
                        this.Response.Write(Tool.NMsg.SetMsg(string.Format("删除成功！总共删除{0}条", long_Result), "1"));
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








    }
      



}
