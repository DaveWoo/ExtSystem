using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
    public class DBDataController : Controller
    {
        //
        // GET: /DBData/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Data_DB_Img_Category()
        {



            string[] keys = this.Request.Form.AllKeys;
            string page = this.Request.QueryString["page"];
            string rows = this.Request.QueryString["rows"];
            string type_name = this.Request.QueryString["type_name"];

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

            BLL.DB_Img_Category dal = new BLL.DB_Img_Category();
            List<NModel.DB_Img_Category> inModel =
                dal.GetPagerList(_page, _rows);



            long TatalPage = dal.GetListTotal();
            StringBuilder sbStr = new StringBuilder();
          //  BLL.Fun.SetJsonData<>
            if (Tool.NTool.IsLtNULL<NModel.DB_Img_Category>(inModel))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();

                int lastdo = 0;
                foreach (NModel.DB_Img_Category outmodel in inModel)
                {


                    dict.Clear();
                    //自由修改

                  
                    dict.Add("img_category_id", "" + outmodel.Img_category_ID);

                    dict.Add("img_category_name", "" + outmodel.Img_category_Name);

                    dict.Add("img_category_name_en", "" + outmodel.Img_category_Name_en);
                    dict.Add("img_category_Operate", "" + outmodel.Img_category_Operate);


                    dict.Add("img_category_addtime", "" + outmodel.Img_category_AddTime);
                    dict.Add("img_category_edittime", "" + outmodel.Img_category_EditTime);

                    dict.Add("img_category_operate_show", "" + Tool.NStr.SetStatusMsg(outmodel.Img_category_Operate, "可删除", "不可删"));

                    dict.Add("img_category_folder", "" + outmodel.Img_category_Folder);
                    dict.Add("img_category_size", "" + outmodel.Img_category_Size);
                    dict.Add("img_category_sortno", "" + outmodel.Img_category_SortNo);

                    dict.Add("img_category_status", "" + outmodel.Img_category_Status);

                    dict.Add("img_category_status_show", "" + Tool.NStr.SetStatusMsg(outmodel.Img_category_Status, "启用", "禁用"));
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


            NModel.PageData<NModel.DB_Img_Category> outModel = new NModel.PageData<NModel.DB_Img_Category>();
            outModel.TotalPage = TatalPage;
            // outModel.OutData = inModel;
            outModel.OutStr = sbStr.ToString();
            dal.Close();
            return View(outModel);


        }
        public ActionResult Data_DB_File_Category()
        {



            string[] keys = this.Request.Form.AllKeys;
            string page = this.Request.QueryString["page"];
            string rows = this.Request.QueryString["rows"];
            string type_name = this.Request.QueryString["type_name"];

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

            BLL.DB_File_Category dal = new BLL.DB_File_Category();
            List<NModel.DB_File_Category> inModel =
                dal.GetPagerList(_page, _rows);



      
      


            NModel.PageData<NModel.DB_File_Category> outModel = BLL.Fun.SetJsonData<NModel.DB_File_Category>(inModel);
            dal.Close();
            return View(outModel);


        }

           public ActionResult  Data_DB_Image()
        {
            

            string[] keys = this.Request.Form.AllKeys;
            string page = this.Request.Form["page"];
            string rows = this.Request.Form["rows"];
            string where = this.Request.Form["where"];
            string order = this.Request.Form["order"];

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

            BLL.DB_Image dal = new BLL.DB_Image();
            List<NModel.DB_Image> inModel = new List<NModel.DB_Image>();
                 inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);


            NModel.PageData<NModel.DB_Image> outModel = BLL.Fun.SetJsonData<NModel.DB_Image>(inModel);
            dal.Close();
            return View(outModel);


        }
           public ActionResult Data_DB_WebConfig()
           {



               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
               string where = this.Request.Form["where"];
               string order = this.Request.Form["order"];



               //   where = Server.UrlDecode(where);
               //1=1  and file_name like &#39;%va%&#39;


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


               BLL.DB_WebConfig dal = new BLL.DB_WebConfig();
               List<NModel.DB_WebConfig> inModel = new List<NModel.DB_WebConfig>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_WebConfig> outModel = BLL.Fun.SetJsonData<NModel.DB_WebConfig>(inModel);

               dal.Close();

               return View(outModel);


           }

       
     

         
           public void DataApped_DB_Jobcfn(ref Dictionary<string, string> appdict, string jsonStr) {

               
           
           
           }

           public void DataApped_DB_Classify(ref Dictionary<string, string> appdict, string jsonStr)
           {

               
              //string Jobcfn_Y=   appdict["Jobcfn_Y"];
              //string Jobcfn_Z=   appdict["Jobcfn_Z"];  
              //long y=0,z=0;
              //long.TryParse(Jobcfn_Y, out y);
              //long.TryParse(Jobcfn_Z, out z);
              // BLL.DB_Jobcfn bll = new BLL.DB_Jobcfn();
              // string sql = string.Format("Jobcfn_y={0} and Jobcfn_z={0}",Jobcfn_Y,Jobcfn_Z);
              // long? count=bll.GetWhereListTotal(sql);
              // appdict.Add("Next_Count_Show", "" + count);

              // bll.Close();
           
           }

           public ActionResult Data_DB_ClassifyByWhere()
           {

               string page = this.Request.QueryString["page"];
               string rows = this.Request.QueryString["rows"];
               string where = this.Request.QueryString["Num"];
              /// string order = this.Request.Form["order"];


               string   order = " Classify_sortno asc, Classify_addtime desc  ";
             bool IsNotEmpty=  !string.IsNullOrEmpty(where);
             if (IsNotEmpty) {

                 where =string.Format(" Classify_Num=''{0}''  ",where);
             
             }


               int _page = 0, _rows = 0;
               int.TryParse(page, out _page);
               int.TryParse(rows, out _rows);
               if (_page == 0)
               {
                   _page = 1;

               }
               if (_rows == 0)
               {

                   _rows = 220;
               }


               BLL.DB_Classify dal = new BLL.DB_Classify();
               List<NModel.DB_Classify> inModel = new List<NModel.DB_Classify>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_Classify> outModel = BLL.Fun.SetDJsonData<NModel.DB_Classify>(inModel, DataApped_DB_Classify);


               // inModel[0].Count_Show
               dal.Close();

               return View(outModel);




           
           }
           public ActionResult Data_DB_Classify()
           {



               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
               string where = this.Request.Form["where"];
               string order = this.Request.Form["order"];



               //   where = Server.UrlDecode(where);
               //1=1  and file_name like &#39;%va%&#39;


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


               BLL.DB_Classify dal = new BLL.DB_Classify();
               List<NModel.DB_Classify> inModel = new List<NModel.DB_Classify>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_Classify> outModel = BLL.Fun.SetDJsonData<NModel.DB_Classify>(inModel, DataApped_DB_Classify);


               // inModel[0].Count_Show
               dal.Close();

               return View(outModel);


           }

           public ActionResult Data_DB_File()
           {



               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
               string where = this.Request.Form["where"];
               string order = this.Request.Form["order"];



               //   where = Server.UrlDecode(where);
               //1=1  and file_name like &#39;%va%&#39;


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


               BLL.DB_File dal = new BLL.DB_File();
               List<NModel.DB_File> inModel = new List<NModel.DB_File>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_File> outModel = BLL.Fun.SetJsonData<NModel.DB_File>(inModel);

               dal.Close();

               return View(outModel);


           }


           public ActionResult Data_DB_Area_Level() {

               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
			   string code = this.Request.QueryString ["code"]; 
			   string where = " 1=1 ";
               string levelStr = this.Request.QueryString["level"];
               int level=0;
               int.TryParse(levelStr, out level);
               level = level <= 0 ? 1 : level;
			   bool isNotEmpty = !string.IsNullOrEmpty (code);
			   long? TotalPage = 0; 


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


               BLL.DB_Area dal = new BLL.DB_Area();
               List<NModel.DB_Area> inModel = new List<NModel.DB_Area>();
			   if(level==1){
               inModel=dal.GetListNextLevel(level);
			   }
			else{
				if (isNotEmpty) {
					if(level==2)
							where += string.Format (" and len(Area_code)-len(replace(Area_code,''_'',''''))=2 and Area_code like ''%[_]0[_]{0}''", code);
					else
							where += string.Format ( " and len(Area_code)-len(replace(Area_code,''_'',''''))=3 and Area_code like ''%[_]{0}''", code.Replace("_","[_]"));
				}
				inModel = dal.GetListByWhere (where);
				TotalPage=dal.GetWhereListTotal(where);
			}

               NModel.PageData<NModel.DB_Area> outModel = BLL.Fun.SetJsonData<NModel.DB_Area>(inModel);
			   outModel.TotalPage=TotalPage;
               dal.Close();

               return View(outModel);

           
           
           }


           public ActionResult Data_DB_Link()
           {

               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
               string where = this.Request.Form["where"];
               string order = this.Request.Form["order"];

             

          


               int _page = 0, _rows = 0;
               int.TryParse(page, out _page);
               int.TryParse(rows, out _rows);
               if (_page == 0)
               {
                   _page = 1;

               }
               if (_rows == 0)
               {

                   _rows = 50;
               }


               BLL.DB_Link dal = new BLL.DB_Link();
               List<NModel.DB_Link> inModel = new List<NModel.DB_Link>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_Link> outModel = BLL.Fun.SetJsonData<NModel.DB_Link>(inModel);


               // inModel[0].Count_Show
               dal.Close();

               return View(outModel);





           }

        



           public ActionResult Data_DB_News()
           {

               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
               string where = this.Request.Form["where"];
               string order = this.Request.Form["order"];






               int _page = 0, _rows = 0;
               int.TryParse(page, out _page);
               int.TryParse(rows, out _rows);
               if (_page == 0)
               {
                   _page = 1;

               }
               if (_rows == 0)
               {

                   _rows = 50;
               }


               BLL.DB_News dal = new BLL.DB_News();
               List<NModel.DB_News> inModel = new List<NModel.DB_News>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_News> outModel = BLL.Fun.SetJsonData<NModel.DB_News>(inModel);


               // inModel[0].Count_Show
               dal.Close();

               return View(outModel);





           }


           public ActionResult Data_DB_AD()
           {

               string[] keys = this.Request.Form.AllKeys;
               string page = this.Request.Form["page"];
               string rows = this.Request.Form["rows"];
               string where = this.Request.Form["where"];
               string order = this.Request.Form["order"];






               int _page = 0, _rows = 0;
               int.TryParse(page, out _page);
               int.TryParse(rows, out _rows);
               if (_page == 0)
               {
                   _page = 1;

               }
               if (_rows == 0)
               {

                   _rows = 50;
               }


               BLL.DB_AD dal = new BLL.DB_AD();
               List<NModel.DB_AD> inModel = new List<NModel.DB_AD>();

               inModel = dal.GetSortAndWherePagerList(_page, _rows, order, where);

               NModel.PageData<NModel.DB_AD> outModel = BLL.Fun.SetJsonData<NModel.DB_AD>(inModel);


               // inModel[0].Count_Show
               dal.Close();

               return View(outModel);





           }
    }
}
