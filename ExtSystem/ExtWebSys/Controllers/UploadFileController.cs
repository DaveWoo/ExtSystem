using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
    public class UploadFileController : Controller
    {
        //
        // GET: /Upload/

     
        public static string DictFilePath = "~/upload/FilePath";
        public static string DFPath = "~/upload/imgPath";
        public string UFPath = "~/upload/FilePath";
        public static string filePath = "filePath";
        public static string imgPath = "imgPath";
        string uploadPath = "~/upload/";
        public static string Path = "~/upload";
        //
        // GET: /UploadFile/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadHead() {
            Response.ContentType = "text/plain";


          
            string allpath = Server.MapPath(uploadPath);

            HttpPostedFileBase file = Request.Files["file"];
          
            string _name = System.IO.Path.GetExtension(file.FileName);

            string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "" + _name; ;

            string path = allpath + "\\" + filename;


            //压缩图片
             System.Drawing.Image  img=   Tool.NImage.ZoomPicture(System.Drawing.Image.FromStream(file.InputStream), 405, 344);



              img.Save(path);
              Response.Write(filename);
            return View();
        
        }
        public ActionResult UploadHeadCut()
        {

            BLL.DB_Img_Category dal = new BLL.DB_Img_Category();
            System.Drawing.Image img = null;
               //进行裁剪
            System.Drawing.Bitmap bmpCrop = null;

            BLL.DB_Image dal_di = new BLL.DB_Image();
            NModel.DB_Img_Category c_dic = new NModel.DB_Img_Category();
            try {
            Response.ContentType = "text/plain";
            String _img = Request.Form["img"];
           

            string oldPath = Server.MapPath(uploadPath) + "\\" + _img.Trim();
            //新图片路径
            string newimg = "small_" + DateTime.Now.ToString("hhmmssfff") + _img; ;
            String newPath = Server.MapPath(uploadPath) + "\\" + newimg;

            //设置截取的坐标和大小
            int x = 0, y = 20, width = 200, height = 2400;

            string _x = Request.Form["x"];
            string _y = Request.Form["y"];
            string _w = Request.Form["w"];
            string _h = Request.Form["h"];

            int.TryParse(_x, out x);
            int.TryParse(_y, out y);
            int.TryParse(_w, out width);
            int.TryParse(_h, out height);
            //计算新的文件名，在旧文件名后加_new


            //定义截取矩形
            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(x, y, width, height); //要截取的区域大小

            //加载图片
            img = System.Drawing.Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(oldPath)));

            //判断超出的位置否
            if ((img.Width < x + width) || img.Height < y + height)
            {
                Response.Write("截取的区域超过了图片本身的高度、宽度.");
                img.Dispose();
                return View();
            }
            //定义Bitmap对象
            System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);

            //进行裁剪
             bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);

            //保存成新文件


            string dffpath = this.Server.MapPath(UFPath);
            bool Has = System.IO.Directory.Exists(dffpath);
            if (!Has)
            {
                System.IO.Directory.CreateDirectory(dffpath);
            }


            string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string file_id = DateTime.Now.ToString("yyyyMMdd");


            string AllFilePath = dffpath;


            string allPath = this.Server.MapPath(DictFilePath);
            string ALLDFPath = this.Server.MapPath(DFPath);
            string cid = this.Request.QueryString["cid"];
            int count = Request.Files.Count;


            long _cid = 0;
            long.TryParse(cid, out _cid);
            string size = "0x0";
          
         
            {
              




                if (_cid > 0)
                {
                    c_dic = dal.GetModel(_cid);
                    if (c_dic != null && !string.IsNullOrEmpty(c_dic.Img_category_Folder))
                    {

                        file_id = c_dic.Img_category_Folder;

                    }


                    if (c_dic != null && !string.IsNullOrEmpty(c_dic.Img_category_Size))
                    {
                        size = c_dic.Img_category_Size;

                    }

                }

                // original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                //system.Drawing.Image.FromFile(ph)
                string[] sz = size.Split(';');
                string[] fr = file_id.Split(';');

                if (fr != null && fr.Length <= 10 && sz != null && sz.Length <= 10)
                    for (int i = 0; i < fr.Length; i++)
                    {


                        bool _Has = System.IO.Directory.Exists(ALLDFPath + "/" + fr[i]);
                        if (!_Has)
                        {
                            System.IO.Directory.CreateDirectory(ALLDFPath + "/" + fr[i]);
                        }

                        string[] xy = sz[i].Split('x');

                        int.TryParse(xy[0], out x);
                        int.TryParse(xy[1], out y);


                        string savepath = "/" + fr[i] + "/" + newimg;

                        string ap = ALLDFPath + savepath;

                       

                        dal_di.DelByBelongsID(BLL.Fun.Current_Login_DB_User().User_ID);
                        string delImagepath = "";
                        NModel.DB_Image cdi = dal_di.GetModelByBelongsID(BLL.Fun.Current_Login_DB_User().User_ID);
                        if (cdi != null && !string.IsNullOrEmpty(cdi.Image_Url))
                        {


                            delImagepath = UploadFileController.DFPath + cdi.Image_Url;
                            bool isdelOk = Tool.NImage.DelImage(delImagepath);

                           


                        }

                        bmpCrop.Save(ap.Trim());

                        var user=BLL.Fun.Current_Login_DB_User();
                        NModel.DB_Image c_di = new NModel.DB_Image();

                        c_di.Image_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                        c_di.Image_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                        c_di.Image_Category_ID = _cid;
                        c_di.Image_SortNo = 1;
                        c_di.Image_Status = 1;
                        c_di.Image_Size = sz[i];
                        c_di.Image_Operate = 1;
                        c_di.Image_Url = savepath;
                        c_di.Image_Belongs_ID = long.Parse(user.User_ID+"");

                        c_di.Image_Name =user .User_Name;
                        bool isadd = dal_di.Add(c_di) > 0 ? true : false;


                        if (isadd)
                        {

                            BLL.DB_User bll = new BLL.DB_User();
                            NModel.DB_User user_model=new NModel.DB_User();
                            user_model.User_HeadImg = "/upload/"+imgPath +"" +savepath;
                            bll.EditFree(user_model,(long)user.User_ID);

                            bll.Close();
                            Response.Write("/upload/" +imgPath +"" +savepath);

                        }
                        else
                        {

                            Response.Write("/content/web/img/noerr.png");

                        }
                      
                    }
            }

            }
            catch (Exception ex) { }
            finally {
                if(dal!=null)
                dal.Close();

                if (dal_di != null)
                dal_di.Close();
                //释放对象

                if (img != null)
                img.Dispose();
                if (bmpCrop != null)
                bmpCrop.Dispose();
            
            }
      

          

        

            return View();

        }

        public ActionResult UploadEditer() {


            string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string allPath = this.Server.MapPath(Path);
            string   img_path="imgEditer";
            string   file_path = "fileEditer";
            string msg = "";
            HttpPostedFileBase  file_upload = Request.Files["filedata"];
            string extName = System.IO.Path.GetExtension(file_upload.FileName);
            switch (extName.ToLower()) { 
                case".jpg":
                case ".png":
                case ".jpeg":
                case ".gif":
                    { 
                       bool _Has = System.IO.Directory.Exists(allPath+"/"+img_path);
                                if (!_Has)
                                {
                                    System.IO.Directory.CreateDirectory(allPath + "/" + img_path);
                                }

                                file_upload.SaveAs(allPath + "/" + img_path + "/" + thumbnail_id + extName);
                               msg = "{'err':'','msg':{'url':'/upload/" + img_path + "/" + thumbnail_id + extName + "','localname':'" + file_upload.FileName + "','id':'1'}}";


                    }
                
                break;
                default: {
                bool _Has = System.IO.Directory.Exists(allPath + "/" + file_path);
                if (!_Has)
                {
                    System.IO.Directory.CreateDirectory(allPath + "/" + file_path);
                }


                       file_upload.SaveAs(allPath+"/"+file_path+"/"+thumbnail_id+extName);

                       msg = "{'err':'','msg':{'url':'/upload/" + file_path + "/" + thumbnail_id + extName + "','localname':'" + file_upload.FileName + "','id':'1'}}";
                }


                break;
            }

          
            this.Response.Write(msg);
            return View();
        
        }

        public ActionResult UploadFile()
        {
            Response.Clear();
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Image original_image = null;
            System.Drawing.Bitmap final_image = null;
            System.Drawing.Graphics graphic = null;
            MemoryStream ms = null;

            try
            {


                string dffpath = this.Server.MapPath(UFPath);
                bool Has = System.IO.Directory.Exists(dffpath);
                if (!Has)
                {
                    System.IO.Directory.CreateDirectory(dffpath);
                }



                // Get the data
                HttpPostedFileBase jpeg_image_upload = Request.Files["image"];
                if (jpeg_image_upload == null) {

                    jpeg_image_upload = Request.Files["file"];
                }

                string type = this.Request.QueryString["type"];

                string cid = this.Request.QueryString["cid"];
                int count = Request.Files.Count;


                // Retrieve the uploaded image





                string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string file_id = DateTime.Now.ToString("yyyyMMdd");
               

                string AllFilePath = dffpath;


                string allPath = this.Server.MapPath(DictFilePath);
                string ALLDFPath = this.Server.MapPath(DFPath);




                long _cid = 0;
                long.TryParse(cid, out _cid);
                string size = "0x0";
                int x = 0, y = 0;


                //jpeg_image_upload.FileName



              string extName=   System.IO.Path.GetExtension(jpeg_image_upload.FileName);

              switch (extName.ToLower()) {
                  case ".jpg":
                  case ".png":
                  case ".jpeg":
                  case ".gif":
                  case ".pdf":
                  case ".txt":
                  case ".zip":
                  case ".rar":
                  case ".doc":
                  
                      break;


                  default: return View();
                 
              
              }




                string mine = type;
                switch (mine)
                {
                  
                    case "img":
                        {
                BLL.DB_Img_Category dal = new  BLL.DB_Img_Category();
               NModel.DB_Img_Category c_dic = new NModel.DB_Img_Category();

             
              

                if (_cid > 0)
                {
                    c_dic = dal.GetModel(_cid);
                    if (c_dic != null && !string.IsNullOrEmpty(c_dic.Img_category_Folder))
                    {

                        file_id = c_dic.Img_category_Folder;

                    }


                    if (c_dic != null && !string.IsNullOrEmpty(c_dic.Img_category_Size))
                    {
                        size = c_dic.Img_category_Size;

                    }

                }

                            // original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);
                            //system.Drawing.Image.FromFile(ph)
                            string[] sz = size.Split(';');
                            string[] fr = file_id.Split(';');

                            if (fr != null && fr.Length <= 10 && sz!=null&&sz.Length<=10)
                            for (int i = 0; i < fr.Length; i++)
                            {


                                bool _Has = System.IO.Directory.Exists(ALLDFPath + "/" + fr[i]);
                                if (!_Has)
                                {
                                    System.IO.Directory.CreateDirectory(ALLDFPath + "/" + fr[i]);
                                }

                                string[] xy = sz[i].Split('x');

                                int.TryParse(xy[0], out x);
                                int.TryParse(xy[1], out y);
                                string lastName = System.IO.Path.GetExtension(jpeg_image_upload.FileName);

                                string savepath = "/" + fr[i] + "/" + thumbnail_id + "" + lastName; ;

                                string ap = ALLDFPath + savepath;
                                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);

                                if (x == 0 && y == 0)
                                {
                                    jpeg_image_upload.SaveAs(ap);


                                }
                                else
                                {

                                    Tool.NImage.MakeThumbnail(original_image, ap, x, y, "Cut");
                                }


                                BLL.DB_Image dal_di = new BLL.DB_Image();
                                NModel.DB_Image c_di = new NModel.DB_Image();

                                c_di.Image_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                                c_di.Image_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                                c_di.Image_Category_ID = _cid;
                                c_di.Image_SortNo = 1;
                                c_di.Image_Status = 1;
                                c_di.Image_Size = sz[i];
                                c_di.Image_Operate = 1;
                                c_di.Image_Url = savepath;
                                c_di.Image_Name = System.IO.Path.GetFileNameWithoutExtension(jpeg_image_upload.FileName);
                                bool isadd = dal_di.Add(c_di)>0?true:false;

                              
                                if (isadd)
                                {
                                    Response.Write(Tool.NMsg.SetMsg("" + savepath + "", "1"));
                                    Response.End();
                                    
                                }
                                else
                                {

                                    Response.Write(Tool.NMsg.SetMsg("上传失败", "0"));
                                    Response.End();
                               
                                }
                                dal.Close();
                                dal_di.Close();
                            }
                        }
                        break;

                    default:
                        {
                            BLL.DB_File dal_di = new BLL.DB_File();
                            NModel.DB_File c_di = new NModel.DB_File();
                            BLL.DB_File_Category dal = new BLL.DB_File_Category();
                            

                            if (_cid > 0)
                            {
                             NModel.DB_File_Category  _c_di= dal.GetModel(_cid);
                             if (_c_di != null && !string.IsNullOrEmpty(c_di.File_Category_Folder))
                                {

                                    file_id = _c_di.File_Category_Folder;

                                }


                                //if (c_dic != null && !string.IsNullOrEmpty(c_dic.File_Category_Size))
                                //{
                                //    size = c_dic.File_Category_Size;

                                //}
                           
                            }

                            //string[] sz = size.Split(';');
                            string[] fr = file_id.Split(';');
                            if(fr!=null&&fr.Length<=10)
                            for (int i = 0; i < fr.Length; i++)
                            {


                                bool _Has = System.IO.Directory.Exists(allPath + "/" + fr[i]);
                                if (!_Has)
                                {
                                    System.IO.Directory.CreateDirectory(allPath + "/" + fr[i]);
                                }


                                string lastName = System.IO.Path.GetExtension(jpeg_image_upload.FileName);

                                string savepath = "/" + fr[i] + "/" + thumbnail_id + "" + lastName; ;

                                string ap = allPath + savepath;


                                /// 保存文件
                                jpeg_image_upload.SaveAs(ap);





                                c_di.File_AddTime = DateTime.Parse(DateTime.Now.ToString("s"));
                                c_di.File_EditTime = DateTime.Parse(DateTime.Now.ToString("s"));
                                c_di.File_Cty_ID = _cid;
                                c_di.File_SortNo = 1;
                                c_di.File_Status = 1;
                                c_di.File_Size = jpeg_image_upload.ContentLength + "";
                                c_di.File_Operate = 1;
                                c_di.File_URL = savepath;
                                c_di.File_Name = System.IO.Path.GetFileNameWithoutExtension(jpeg_image_upload.FileName);

                                bool isadd = dal_di.Add(c_di)>0?true:false;
                              
                                if (isadd)
                                {  //  var img= info["response"];

                                  
                                    Response.Write(Tool.NMsg.SetMsg(""+savepath+"", "1"));
                                    Response.End();
                                }
                                else
                                {

                                    Response.Write(Tool.NMsg.SetMsg("上传失败", "0"));
                                    Response.End();
                                }
                              
                            }
                            dal_di.Close();
                            dal.Close();
                        }
                        break;

                }


                Response.StatusCode = 200;

            }
            catch
            {
                // If any kind of error occurs return a 500 Internal Server error
                Response.StatusCode = 500;
                Response.Write("{\"jsonrpc\" : \"2.0\", \"error\" : {\"code\": 101, \"message\": \"Failed to open input stream.\"}, \"id\" : \"id\"}");
                // Response.End();
            }
            finally
            {
                // Clean up
                if (final_image != null) final_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
                // Response.End();
            }
           
            return View();
          
        }
    }
}
