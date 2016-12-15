using System.Web.Mvc;

namespace ExtWebSys.Controllers
{
	public class DBItemController : Controller
	{
		public ActionResult Item_db_ad()
		{
			BLL.DB_AD dal = new BLL.DB_AD();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];

			string dbaddate = this.Request.Form["dbaddate"];

			string dbadname = this.Request.Form["dbadname"];
			string num = this.Request.QueryString["Num"];
			string where = "", order = "ad_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			where += " 1=1 ";

			if (!string.IsNullOrEmpty(num))
			{
				where += string.Format(" and ad_num=''{0}''  ", num);
			}
			if (!string.IsNullOrEmpty(dbaddate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), ad_addtime, 23)=''{0}''  )", dbaddate);
			}

			if (!string.IsNullOrEmpty(dbadname))
			{
				where += string.Format(" and ad_name like ''%{0}%''  ", dbadname);
			}

			string _where = Server.UrlDecode(this.Request.QueryString["nwhere"] + "");
			bool HasOneEqOne = !((_where + "").Trim().Length <= 3);

			bool IsOneEqOne = ((where + "").Trim().Length <= 3);

			if (IsOneEqOne && !string.IsNullOrEmpty(_where) && HasOneEqOne)
			{
				_where = this.Server.UrlDecode(_where);

				where = _where;
			}
			else
			{
				_where = where;
			}

			NModel.PageData<NModel.DB_AD> PD = new NModel.PageData<NModel.DB_AD>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_news()
		{
			BLL.DB_News dal = new BLL.DB_News();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];

			string dbnewsdate = this.Request.Form["dbnewsdate"];

			string dbnewsname = this.Request.Form["dbnewsname"];
			string num = this.Request.QueryString["Num"];
			string where = "", order = "news_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			where += " 1=1 ";

			if (!string.IsNullOrEmpty(num))
			{
				where += string.Format(" and news_num=''{0}''  ", num);
			}
			if (!string.IsNullOrEmpty(dbnewsdate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), news_addtime, 23)=''{0}''  )", dbnewsdate);
			}

			if (!string.IsNullOrEmpty(dbnewsname))
			{
				where += string.Format(" and news_title like ''%{0}%''  ", dbnewsname);
			}

			string _where = Server.UrlDecode(this.Request.QueryString["nwhere"] + "");
			bool HasOneEqOne = !((_where + "").Trim().Length <= 3);

			bool IsOneEqOne = ((where + "").Trim().Length <= 3);

			if (IsOneEqOne && !string.IsNullOrEmpty(_where) && HasOneEqOne)
			{
				_where = this.Server.UrlDecode(_where);

				where = _where;
			}
			else
			{
				_where = where;
			}

			NModel.PageData<NModel.DB_News> PD = new NModel.PageData<NModel.DB_News>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_link()
		{
			BLL.DB_Link dal = new BLL.DB_Link();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];

			string dblinkdate = this.Request.Form["dblinkdate"];

			string dblinkname = this.Request.Form["dblinkname"];
			string where = "", order = "link_addtime desc";
			string Num = this.Request.QueryString["Num"];

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			where += " 1=1 ";
			bool IsNotEmpty = !string.IsNullOrEmpty(Num);
			if (IsNotEmpty)
			{
				where += string.Format("  and Link_Num=''{0}''  ", Num);
			}
			if (!string.IsNullOrEmpty(dblinkdate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), link_addtime, 23)=''{0}''  )", dblinkdate);
			}

			if (!string.IsNullOrEmpty(dblinkname))
			{
				where += string.Format(" and link_name like ''%{0}%''  ", dblinkname);
			}

			NModel.PageData<NModel.DB_Link> PD = new NModel.PageData<NModel.DB_Link>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Item_db_classify()
		{
			BLL.DB_Classify dal = new BLL.DB_Classify();
			string num = this.Request.QueryString["Num"];
			string id = this.Request.QueryString["id"];
			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];

			string dbClassifydate = this.Request.Form["dbClassifydate"];
			string dbClassifyurl = this.Request.Form["dbClassifyurl"];

			string search = this.Request.Form["search"];
			string x = this.Request.QueryString["x"];

			string y = this.Request.QueryString["y"];

			string z = this.Request.QueryString["z"];

			string _where = this.Request.QueryString["_where"];
			string where = "", order = "Classify_sortno asc,Classify_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			where += " 1=1 ";
			if (!string.IsNullOrEmpty(num))
			{
				where += string.Format(" and   Classify_Num={0}", num);
			}
			if (!string.IsNullOrEmpty(dbClassifydate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), Classify_addtime, 23)=''{0}''  )", dbClassifydate);
			}
			if (!string.IsNullOrEmpty(dbClassifyurl))
			{
				where += string.Format(" and Classify_Name like ''%{0}%''", dbClassifyurl);
			}

			if (!string.IsNullOrEmpty(search))
			{
			}
			else
			{
				if (!string.IsNullOrEmpty(y) && !string.IsNullOrEmpty(z))
				{
					long _x = 0, _y = 0, _z = 0;
					long.TryParse(x, out _x);
					long.TryParse(y, out _y);
					long.TryParse(z, out _z);
					if (_x == 0 && _y == 0 && _z == 0)
					{
						_x = 1;
						_y = 1;
						_z = 1;
					}

					bool isThree = _z > 2;
					string xy = x + "";
					if (isThree) { xy = string.Format("{0}{1}", _x, _y); }

					where += string.Format(" and Classify_x={0} and Classify_z={1} ", xy, (_z));
				}
				else
				{
					where += string.Format(" and Classify_y={0} and Classify_z={1} ", 1, 1);
				}
			}

			if (!string.IsNullOrEmpty(_where)) { where = this.Server.UrlDecode(_where); }
			NModel.PageData<NModel.DB_Classify> PD = new NModel.PageData<NModel.DB_Classify>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_webconfig()
		{
			BLL.DB_WebConfig dal = new BLL.DB_WebConfig();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];

			string dbwebconfigdate = this.Request.Form["dbwebconfigdate"];
			string dbwebconfigurl = this.Request.Form["dbwebconfigurl"];
			string where = "", order = "webconfig_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			where += " 1=1 ";

			if (!string.IsNullOrEmpty(dbwebconfigdate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), webconfig_addtime, 23)=''{0}''  )", dbwebconfigdate);
			}
			if (!string.IsNullOrEmpty(dbwebconfigurl))
			{
				where += string.Format(" and WebConfig_Code like ''%{0}%''", dbwebconfigurl);
			}

			NModel.PageData<NModel.DB_WebConfig> PD = new NModel.PageData<NModel.DB_WebConfig>()
		{
			NumPerPage = numPerPage,
			PageNum = pagenum,
			TotalPage = dal.GetWhereListTotal(where),
			Where = where,
			OrderField = order
		};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_file()
		{
			BLL.DB_File dal = new BLL.DB_File();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];
			string dbimagetype = this.Request.Form["dbimagetype"];
			string dbimagedate = this.Request.Form["dbimagedate"];
			string dbimagename = this.Request.Form["dbimagename"];
			string where = "", order = "file_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			int _dbimagetype = 0;
			int.TryParse(dbimagetype, out _dbimagetype);

			where += " 1=1 ";
			if (_dbimagetype > 0)
			{
				where += string.Format(" and File_Cty_ID={0} ", _dbimagetype);
			}
			if (!string.IsNullOrEmpty(dbimagedate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), file_addtime, 23)=''{0}''  )", dbimagedate);
			}
			if (!string.IsNullOrEmpty(dbimagename))
			{
				where += string.Format(" and file_name like ''%{0}%''", dbimagename);
			}

			NModel.PageData<NModel.DB_File> PD = new NModel.PageData<NModel.DB_File>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_img_dialog()
		{
			BLL.DB_Image dal = new BLL.DB_Image();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];
			string dbimagetype = this.Request.Form["dbimagetype"];
			string dbimagedate = this.Request.Form["dbimagedate"];
			string dbimagename = this.Request.Form["dbimagename"];

			string db_image_type = this.Request.QueryString["db_image_type"];

			string selectid = this.Request.QueryString["selectid"];
			if (!string.IsNullOrEmpty(selectid))
			{
				dbimagetype = selectid;
			}

			if (!string.IsNullOrEmpty(db_image_type))
			{
				dbimagetype = db_image_type;
			}

			string where = "", order = "Image_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			int _dbimagetype = 0;
			int.TryParse(dbimagetype, out _dbimagetype);

			where += " 1=1 ";
			if (_dbimagetype > 0)
			{
				where += string.Format(" and Image_Category_ID={0} ", _dbimagetype);
			}
			if (!string.IsNullOrEmpty(dbimagedate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), Image_addtime, 23)=''{0}''  )", dbimagedate);
			}
			if (!string.IsNullOrEmpty(dbimagename))
			{
				where += string.Format(" and Image_name like ''%{0}%''", dbimagename);
			}

			NModel.PageData<NModel.DB_Image> PD = new NModel.PageData<NModel.DB_Image>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_image()
		{
			BLL.DB_Image dal = new BLL.DB_Image();

			string pagenum = this.Request.Form["pagenum"];
			string numPerPage = this.Request.Form["numPerPage"];
			string dbimagetype = this.Request.Form["dbimagetype"];
			string dbimagedate = this.Request.Form["dbimagedate"];
			string dbimagename = this.Request.Form["dbimagename"];
			string where = "", order = "Image_addtime desc";

			if (string.IsNullOrEmpty(numPerPage))
			{
				numPerPage = "20";
			}
			if (string.IsNullOrEmpty(pagenum))
			{
				pagenum = "1";
			}

			int _dbimagetype = 0;
			int.TryParse(dbimagetype, out _dbimagetype);

			where += " 1=1 ";
			if (_dbimagetype > 0)
			{
				where += string.Format(" and Image_Category_ID={0} ", _dbimagetype);
			}

			if (_dbimagetype == -1)
			{
				where += string.Format(" and Image_Category_ID>{0} ", 0);
			}
			if (!string.IsNullOrEmpty(dbimagedate))
			{
				where += string.Format(" and ( CONVERT(varchar(100), Image_addtime, 23)=''{0}''  )", dbimagedate);
			}
			if (!string.IsNullOrEmpty(dbimagename))
			{
				where += string.Format(" and Image_name like ''%{0}%''", dbimagename);
			}

			string _where = Server.UrlDecode(this.Request.QueryString["nwhere"] + "");
			bool HasOneEqOne = !((_where + "").Trim().Length <= 3);

			bool IsOneEqOne = ((where + "").Trim().Length <= 3);

			if (IsOneEqOne && !string.IsNullOrEmpty(_where) && HasOneEqOne)
			{
				_where = this.Server.UrlDecode(_where);

				where = _where;
			}
			else
			{
				_where = where;
			}

			NModel.PageData<NModel.DB_Image> PD = new NModel.PageData<NModel.DB_Image>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetWhereListTotal(where),
				Where = where,
				OrderField = order
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_img_category()
		{
			BLL.DB_Img_Category dal = new BLL.DB_Img_Category();

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

			NModel.PageData<NModel.DB_Img_Category> PD = new NModel.PageData<NModel.DB_Img_Category>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetListTotal()
			};
			dal.Close();
			return View(PD);
		}

		public ActionResult Item_db_file_category()
		{
			BLL.DB_File_Category dal = new BLL.DB_File_Category();

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

			NModel.PageData<NModel.DB_File_Category> PD = new NModel.PageData<NModel.DB_File_Category>()
			{
				NumPerPage = numPerPage,
				PageNum = pagenum,
				TotalPage = dal.GetListTotal()
			};
			dal.Close();
			return View(PD);
		}
	}
}