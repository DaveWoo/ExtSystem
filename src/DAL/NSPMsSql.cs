using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NModel;
using Tool;

namespace DAL
{
	public class NSPMsSql<T> : NMsSql<T>
	{
		public static int TAG = 1;
		public string TableName { get; set; }
		public string ErrorMsg { get; set; }
		public T EnObj { get; set; }
		public string ETblName { get; set; }

		public NSPMsSql()
		{
			SQLMothed();
		}

		public void EXEEditFree(T menu, ref string vals)
		{
			Type tpe = typeof(T);

			StringBuilder keyvalStrSql = new StringBuilder();

			int _cn = 0;
			System.Reflection.PropertyInfo[] properties = tpe.GetProperties();
			List<System.Reflection.PropertyInfo> pinfo = properties.Where(a => a.DeclaringType.Name.Equals((tpe).Name)).ToList();

			if (pinfo.Count <= 0)
			{
				pinfo = properties.ToList();
			}

			int cn = pinfo.Count;
			foreach (System.Reflection.PropertyInfo pi in pinfo)
			{
				Type tpe1 = typeof(T);
				int len = tpe1.Name.IndexOf('_');
				string DB_NM = tpe1.Name.Substring(len + 1, tpe1.Name.Length - (len + 1));
				DB_NM += "_";

				string val = pi.GetValue(menu, null) + "";

				string name = "" + pi.Name;
				Type tp = pi.PropertyType;
				string pptypeName = "";
				pptypeName = tp.FullName.ToLower();
				if (pptypeName.LastIndexOf("int") != -1 && name.ToLower().Equals((DB_NM + "id").ToLower()))
				{
					continue;
				}

				Type valtype = tp;

				//Tool.NTool.GetNType(val, pi)
				if (string.IsNullOrEmpty(val))
				{
					continue;
				}
				_cn++;

				if (_cn == 1)
				{
					// keyStrSql.Append(string.Format("{0}", name));
					keyvalStrSql.Append(string.Format("{0}={1}", name, Tool.NTool.GetNNType(val, pi)));
				}
				else
				{
					keyvalStrSql.Append(string.Format(",{0}={1}", name, Tool.NTool.GetNNType(val, pi)));
				}
			}

			vals = keyvalStrSql + "";
		}

		public void EXEAddFree(T menu, ref string keys, ref string vals)
		{
			Type tpe = typeof(T);

			StringBuilder keyStrSql = new StringBuilder();

			StringBuilder valStrSql = new StringBuilder();

			int _cn = 0;
			System.Reflection.PropertyInfo[] properties = tpe.GetProperties();
			List<System.Reflection.PropertyInfo> pinfo = properties.Where(a => a.DeclaringType.Name.Equals((tpe).Name)).ToList();

			if (pinfo.Count <= 0)
			{
				pinfo = properties.ToList();
			}

			int cn = pinfo.Count;
			foreach (System.Reflection.PropertyInfo pi in pinfo)
			{
				string val = pi.GetValue(menu, null) + "";

				string name = "" + pi.Name;
				Type tp = pi.PropertyType;

				Type valtype = tp;
				string pptypeName = "";
				pptypeName = valtype.FullName.ToLower();
				//Tool.NTool.GetNType(val, pi)
				if (string.IsNullOrEmpty(val))
				{
					continue;
				}
				_cn++;

				if (_cn == 1)
				{
					keyStrSql.Append(string.Format("{0}", name));
					valStrSql.Append(string.Format("{0}", Tool.NTool.GetNNType(val, pi)));
				}
				else
				{
					keyStrSql.Append(string.Format(",{0}", name));
					valStrSql.Append(string.Format(",{0}", Tool.NTool.GetNNType(val, pi)));
				}
			}

			keys = keyStrSql.ToString();
			vals = valStrSql.ToString();
		}

		public long EXECOP<cmodel>(NModel.EnObject.Operate op, cmodel menu, string TblName)
		{
			Type tpe = typeof(cmodel);
			int len = tpe.Name.IndexOf('_');
			string DB_NM = tpe.Name.Substring(len + 1, tpe.Name.Length - (len + 1));
			DB_NM += "_";
			StringBuilder strSql = new StringBuilder();

			//if (EnObject.Operate.Edit == op)
			strSql.AppendFormat("DECLARE	@{0}ID bigint  ", DB_NM);

			strSql.Append(string.Format("EXEC	{0}   ", TblName));

			if (EnObject.Operate.Add == op)
				strSql.AppendFormat("@{0}ID OUTPUT,   ", DB_NM);

			// CModel.Admin_Role mu = null;

			int _cn = 0;
			System.Reflection.PropertyInfo[] properties = tpe.GetProperties();
			List<System.Reflection.PropertyInfo> pinfo = properties.Where(a => a.DeclaringType.Name.Equals((tpe).Name)).ToList();

			if (pinfo.Count <= 0)
			{
				pinfo = properties.ToList();
			}

			int cn = pinfo.Count;
			foreach (System.Reflection.PropertyInfo pi in pinfo)
			{
				//string thisName = ((System.Reflection.MemberInfo)(pi)).DeclaringType.Name;
				//string BaseName=     typeof(T).BaseType.Name;

				_cn++;
				string val = pi.GetValue(menu, null) + "";

				string name = "" + pi.Name;
				Type tp = pi.PropertyType;

				Type valtype = tp;
				string pptypeName = "";
				pptypeName = valtype.FullName.ToLower();

				if (EnObject.Operate.Add == op)
				{
					if (pptypeName.LastIndexOf("int") != -1 && name.ToLower().Equals((DB_NM + "id").ToLower()))
					{
						continue;
					}

					SetDefaultVal(pptypeName, name, "string", "password", ref val, val);

					SetDefaultVal(pptypeName, name, "datetime", "addtime", ref val, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
					/// if (pptypeName.ToLower().IndexOf("datetime") != -1) { val = DateTime.Parse(val).ToString("yyyy-MM-dd hh:mm:ss"); }
				}
				else if (EnObject.Operate.Edit == op)
				{
					SetDefaultVal(pptypeName, name, "string", "password", ref val, val);
					//  SetDefaultVal(pptypeName, name, "datetime", "modtime", ref val, DateTime.Now.ToString("s"));
				}
				//

				if (_cn >= cn)
					strSql.Append(string.Format("@{0}={1}", name, Tool.NTool.GetNType(val, pi) + ""));
				else
					strSql.Append(string.Format("@{0}={1},", name, Tool.NTool.GetNType(val, pi) + ""));
			}

			return base.DUI(strSql + "");
		}

		public bool UpdateClickRate(long? id)
		{
			string execSql = string.Format("Exec  {0}_UpdateClickRate  @ID={1} ", ETblName, id);

			return base.DUI(execSql) > 0 ? true : false;
		}

		/// <summary>
		/// 设置默认值
		/// </summary>
		/// <param name="pptypeName"></param>
		/// <param name="name"></param>
		/// <param name="ofpptypeName"></param>
		/// <param name="ofName"></param>
		/// <param name="setVal"></param>
		/// <returns>真（设置成功）---</returns>
		public static bool SetDefaultVal(string pptypeName, string name, string ofpptypeName, string ofName, ref string setVal, string DFVal)
		{
			if (pptypeName.LastIndexOf(ofpptypeName) != -1)
			{
				if (name.ToLower().IndexOf(ofName) != -1)
				{
					if (string.IsNullOrEmpty(setVal))
					{
						setVal = DFVal;
						return true;
					}
					else
					{
						// setVal = Tool.N_MD5.GetMd5Hash(DFVal);
					}
				}
			}
			return false;
		}

		public void SQLMothed()
		{
			Type tpe = typeof(T);
			this.TableName = tpe.Name;
			//  this.EnObj =(T)this.GetType().Assembly.CreateInstance(this.GetType().FullName);
			this.ETblName = tpe.Name;

			this.EnObj = (T)tpe.Assembly.CreateInstance(tpe.FullName);
		}

		public void SQLMothed(T EnObj)
		{
			this.TableName = this.GetType().Name;
			//  this.EnObj =(T)this.GetType().Assembly.CreateInstance(this.GetType().FullName);
			this.ETblName = this.GetType().Name;

			this.EnObj = EnObj;
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public T GetMaxXANDZYModel(long? x, long? z)
		{
			string execSql = string.Format("Exec  {0}_GetMaxXANDZYModel  @X={1},@Z={2} ", ETblName, x, z);
			List<T> modelList = base.GetList(execSql);

			return modelList != null && modelList.Count > 0 ? modelList[0] : default(T);
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public long DelXList(params string[] xList)
		{
			long OkLen = 0;
			if (Tool.NTool.IsArrNULL<string>(xList))
			{
				foreach (string code in xList)
				{
					string execSql = string.Format("Exec  {0}_DelXList  @X=N'{1}%' ", ETblName, code);

					long num = base.DUI(execSql);
					OkLen += num;
				}
			}

			return OkLen;
		}

		/// <summary>
		/// 获取最大y  存储过程
		/// </summary>
		/// <param name="x">x设置</param>
		/// <returns></returns>
		public DataTable GetXMaxYModel(long? x)
		{
			string execSql = string.Format("Exec  {0}_GetXMaxYModel @X={1}  ", ETblName, x);

			return base.GetTable(execSql);
		}

		/// <summary>
		/// 获取一级菜单最大X 存储过程
		/// </summary>

		/// <returns></returns>
		public DataTable GetFristLevelMaxXModel()
		{
			string execSql = string.Format("Exec  {0}_GetFristLevelMaxXModel  ", ETblName);

			return base.GetTable(execSql);
		}

		//GetPagerList GetListTotal

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public long GetTypeListTotal(string type_name)
		{
			string execSql = string.Format("Exec  {0}_GetTypeListTotal  @Type_Name='{1}'  ", ETblName, type_name);
			object num = base.GetObject(execSql);
			if (num != null)
			{
				long long_num = 0;
				long.TryParse(num + "", out long_num);

				return long_num;
			}
			return 0;
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public long GetListTotal()
		{
			string execSql = string.Format("Exec  {0}_GetListTotal  ", ETblName);
			object num = base.GetObject(execSql);
			if (num != null)
			{
				long long_num = 0;
				long.TryParse(num + "", out long_num);

				return long_num;
			}
			return 0;
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public long GetWhereListTotal(string where)
		{
			string execSql = string.Format("Exec  {0}_GetWhereListTotal  @Where='{1}' ", ETblName, where);
			object num = base.GetObject(execSql);
			if (num != null)
			{
				long long_num = 0;
				long.TryParse(num + "", out long_num);

				return long_num;
			}
			return 0;
		}

		/// <summary>
		/// 获全部内容最新 存储过程
		/// </summary>
		/// <param name="CurrentPage">当前页</param>
		/// <param name="PageSize">每页大小</param>
		/// <returns> List<T></returns>
		public List<T> GetTypePagerList(int CurrentPage, int PageSize, string Type_Name)
		{
			string execSql = string.Format("Exec  {0}_GetTypePagerList @PageSize={1},@CurrentPage={2},@Type_Name='{3}'  ", ETblName, PageSize, CurrentPage, Type_Name);

			return this.GetList(execSql);
		}

		/// <summary>
		/// 获取分类拼排序  存储过程
		/// </summary>
		/// <param name="CurrentPage"></param>
		/// <param name="PageSize"></param>
		/// <param name="Sort"> 如：image_id desc</param>
		/// <returns></returns>
		public List<T> GetSortPagerList(int CurrentPage, int PageSize, string Sort)
		{
			string execSql = string.Format("Exec  {0}_GetSortPagerList @PageSize={1},@CurrentPage={2},@Sort='{3}'  ", ETblName, PageSize, CurrentPage, Sort);

			return this.GetList(execSql);
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public List<T> GetSortAndWherePagerList(int CurrentPage, int PageSize, string Sort, string where)
		{
			string execSql = string.Format("Exec  {0}_GetSortAndWherePagerList @PageSize={1},@CurrentPage={2},@Sort='{3}',@Where='{4}'  ", ETblName, PageSize, CurrentPage, Sort, where);

			return this.GetList(execSql);
		}

		public IList<T> GetNewListByWhereAndTop(String where, int Top)
		{
			string execSql = string.Format("Exec  {0}_GetNewListByWhereAndTop @Where=N'{1}',@Top={2}  ", ETblName, where, Top);
			IList<T> _list = this.GetList(execSql);
			if (_list != null)
			{
				return _list;
			}
			return new List<T>();
		}

		/// <summary>
		/// 获取 Num 列表
		/// </summary>
		/// <returns>The IList<T=(实体)></c></returns>
		/// <param name="_num"> 输入例如：Link_Num</param>
		/// <param name="top">显示多少行</param>
		public IList<T> GetListByNumAndTop(String _num, int top)
		{
			string execSql = string.Format("Exec  {0}_ListByNumAndTop @Num=N'{1}',@Top={2}  ", ETblName, _num, top);
			IList<T> _list = this.GetList(execSql);
			if (_list != null)
			{
				return _list;
			}
			return new List<T>();
		}

		/// <summary>
		/// 获全部内容最新 存储过程
		/// </summary>
		/// <param name="CurrentPage">当前页</param>
		/// <param name="PageSize">每页大小</param>
		/// <returns> List<T></returns>
		public List<T> GetPagerList(int CurrentPage, int PageSize)
		{
			string execSql = string.Format("Exec  {0}_GetPagerList @PageSize={1},@CurrentPage={2}  ", ETblName, PageSize, CurrentPage);

			return this.GetList(execSql);
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu"></param>
		/// <returns></returns>
		public long Add(T menu)
		{
			long Result = EXECOP<T>(EnObject.Operate.Add, menu, string.Format("     {0}_ADD", ETblName));

			return Result;
		}

		/// <summary>
		/// 存储过程
		/// </summary>
		/// <param name="menu">T.id=1</param>
		/// <returns>bool</returns>
		public long Edit(T menu)
		{
			long Result = EXECOP<T>(EnObject.Operate.Edit, menu, string.Format("     {0}_Edit", ETblName));

			return Result;
		}

		public long EditFree(long id, string KeyVal)
		{
			string execSql = string.Format("Exec  {0}_EditFree @KeyVal=N'{1}',@ID={2} ", ETblName, KeyVal, id);

			return base.DUI(execSql);
		}

		public long AddFree(string keys, string vals)
		{
			string execSql = string.Format("Exec  {0}_AddFree @Key=N'{1}', @Val=N'{2}' ", ETblName, keys, vals);

			return base.DUI(execSql);
		}

		public long EditFree(string where, string KeyVal)
		{
			string execSql = string.Format("Exec  {0}_EditFreeByWhere @KeyVal=N'{1}',@Where={2} ", ETblName, KeyVal, where);

			return base.DUI(execSql);
		}

		public long EditFree(T model, string where)
		{
			string vals = "";
			this.EXEEditFree(model, ref vals);
			return this.EditFree(where, vals);
		}

		public long EditFree(T model, long id)
		{
			string vals = "";
			this.EXEEditFree(model, ref vals);
			return this.EditFree(id, vals);
		}

		public long AddFree(T model)
		{
			string keys = "", vals = "";

			this.EXEAddFree(model, ref keys, ref vals);

			return this.AddFree(keys, vals);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public int ExistsX(long X)
		{
			string execSql = string.Format("Exec  {0}_ExistsX @X={1} ", ETblName, X);

			int num = 0;
			int.TryParse(base.GetObject(execSql) + "", out num);

			return num;
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public int Exists(int id)
		{
			string execSql = string.Format("Exec  {0}_Exists @ID={1} ", ETblName, id);

			int num = 0;
			int.TryParse(base.GetObject(execSql) + "", out num);

			return num;
		}

		/// <summary>
		///  是否有下级菜单 储存过程
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public long HasNextLevel(long x, long y, long z)
		{
			string execSql = string.Format("Exec  {0}_HasNextLevel @x={0},@y={1},@z={2}} ", ETblName, x, y, z);

			long num = 0;
			long.TryParse(base.GetObject(execSql) + "", out num);

			return num;
		}

		public long HasNextLevelByID(long ID)
		{
			string execSql = string.Format("Exec  {0}_HasNextLevelByID @ID={1} ", ETblName, ID);

			long num = 0;
			long.TryParse(base.GetObject(execSql) + "", out num);

			return num;
		}

		//public T GetCodeMaxModel(int len, string code = "%")
		//{
		//    string execSql = string.Format("Exec   {0}_GetCodeMaxModel @code=N'{1}', @Len={2} ", ETblName, code, len);

		//    return this.ReadFirst(execSql, 1);
		//}

		//public T GetCodeMaxModel(string code = "%")
		//{
		//    string execSql = string.Format("Exec   {0}_GetCodeMaxModel @code=N'{1}' ", ETblName, code);

		//    return this.ReadFirst(execSql, 1);
		//}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public int ExistsName(string name)
		{
			string execSql = string.Format("DECLARE	@return_value int   Exec   @return_value={0}_ExistsName @Name=N'{1}' SELECT	'ReturnValue' = @return_value ", ETblName, name);

			int num = 0;
			int.TryParse(base.GetObject(execSql) + "", out num);

			return num;
		}

		/// <summary>
		///储存过程 删除 多列
		/// </summary>
		/// <param name="idList">  int ID 数组</param>
		/// <returns>多少行成功操作</returns>
		public long DelList(params int[] idList)
		{
			object[] s = (object[])Tool.NTool.ConvertArray(idList.ToArray(), typeof(object));

			string execSql = string.Format("Exec  {0}_DelList @IDList=N'{1}' ", ETblName, NStr.GetArrayString(s));
			return base.DUI(execSql);
		}

		public long DelAllByNum(string num)
		{
			string execSql = string.Format("Exec  {0}_DelAllByNum @Num=N'{1}'  ", ETblName, num);
			return base.DUI(execSql);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public long DelAll()
		{
			string execSql = string.Format("Exec  {0}_DelAll  ", ETblName);
			return base.DUI(execSql);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public List<T> GetIDList(params int[] idList)
		{
			object[] s = (object[])Tool.NTool.ConvertArray(idList.ToArray(), typeof(object));
			string execSql = string.Format("Exec  {0}_GetIDList @IDList=N'{1}' ", ETblName, Tool.NStr.GetArrayString(s));
			return this.GetList(execSql);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public long DelEqualCode(params   string[] codeList)
		{
			long OkLen = 0;
			if (Tool.NTool.IsArrNULL<string>(codeList))
			{
				foreach (string code in codeList)
				{
					string execSql = string.Format("Exec  {0}_DelEqualCode  @Code=N'{1}%' ", ETblName, code);

					long num = base.DUI(execSql);
					OkLen += num;
				}
			}

			return OkLen;
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public bool Del(int id)
		{
			string execSql = string.Format("Exec  {0}_Del @ID={1} ", ETblName, id);
			return base.DUI(execSql) > 0 ? true : false;
		}

		/// <summary>
		///获取全部内容-存储过程
		/// </summary>
		/// <returns>List<T> T 实体对象</returns>
		public List<T> GetList()
		{
			string execSql = string.Format("Exec  {0}_GetList ", ETblName);
			return base.GetList(execSql);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public T GetRlAndUsModel(long? id)
		{
			string execSql = string.Format("Exec  {0}_GetRlAndUsModel @ID={1} ", ETblName, id);

			List<T> Tmodel = this.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}

		public T GetListByName(string name)
		{
			string execSql = string.Format("Exec  {0}_GetListByName @Name=N'{1}' ", ETblName, name);

			List<T> Tmodel = base.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}

		public List<T> GetListByWhere(string where)
		{
			string execSql = string.Format("Exec  {0}_GetListByWhere @Where=N'{1}' ", ETblName, where);

			List<T> Tmodel = base.GetList(execSql);

			return Tmodel;
		}

		public List<T> GetModelByCode(string code)
		{
			string execSql = string.Format("Exec  {0}_GetCodeModel @Code=N'{1}' ", ETblName, code);

			List<T> Tmodel = base.GetList(execSql);

			return Tmodel;
		}

		public T GetCodeModel(string code)
		{
			string execSql = string.Format("Exec  {0}_GetCodeModel @Code=N'{1}' ", ETblName, code);

			List<T> Tmodel = base.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public T GetNameModel(string name)
		{
			string execSql = string.Format("Exec  {0}_GetNameModel @Name=N'{1}' ", ETblName, name);

			List<T> Tmodel = base.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}

		/// <summary>
		/// 储存过程
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public T GetModel(long? id)
		{
			string execSql = string.Format("Exec  {0}_GetModel @ID={1} ", ETblName, id);

			List<T> Tmodel = base.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}

		/// <summary>
		/// 储存过程下个
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public T GetNextModel(long? id, string where = "1=1")
		{
			string execSql = string.Format("Exec  {0}_GetNextModel @ID={1},@Where=N'{2}' ", ETblName, id, where);

			List<T> Tmodel = base.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}

		/// <summary>
		/// 储存过程上个
		/// </summary>
		/// <param name="idList"></param>
		/// <returns></returns>
		public T GetPrevModel(long? id, string where = "1=1")
		{
			string execSql = string.Format("Exec  {0}_GetPrevModel @ID={1},@Where=N'{2}' ", ETblName, id, where);

			List<T> Tmodel = base.GetList(execSql);
			if (NTool.IsLtNULL<T>(Tmodel))
			{
				return Tmodel[0];
			}
			return default(T);
		}
	}
}