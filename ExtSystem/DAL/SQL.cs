using System;
using System.Collections.Generic;
using System.Configuration;
using IDAL;
using Tool;

namespace DAL
{
	public abstract class SQL : ISQLAbl
	{
		public Dictionary<string, object> listSqlParam = new Dictionary<string, object>();
		protected string msg { get; set; }
		public string Msg { get { return this.msg; } }
		protected string successMsg { get; set; }

		/// <summary>
		/// 成功信息
		/// </summary>
		public String SuccessMsg { get { return this.successMsg; } }

		protected bool isOpenDataBase = false;
		public bool IsOpenDataBase { get { return this.isOpenDataBase; } }

		/// <summary>
		/// 设置/返回SQL异常信息只有继承才能使用
		/// </summary>
		protected string errorMsg { get; set; }

		/// <summary>
		/// SQL异常信息返回
		/// </summary>
		public String ErrorMsg { get { return this.errorMsg; } }

		/// <summary>
		///  设置/返回sql语句
		/// </summary>
		public String SqlSentence { get; set; }

		// public   OperaterDB OpDB{get;set;}

		public string TableName { get; set; }

		/// <summary>
		/// 设置configName,返回configString
		/// </summary>
		public String ConfigStr
		{
			get { return ConfigurationManager.ConnectionStrings[this.configStr] + ""; }
			set { this.configStr = value; }
		}

		private string configStr;

		public enum EDataBaseType { MsSql, MySql, Oracle, Access, Sqlite }

		public enum EFun { GetID, GetWhere }

		public EFun nFun { get; set; }

		public SQL() { this.ConfigStr = "DBMSSQL"; }

		public SQL(String ConfigStr)
		{
			this.ConfigStr = ConfigStr;
		}

		//  public delegate void OperaterDB(ITSQLAbl<T> ODB);

		/// <summary>
		///构造函数打开数据库
		/// </summary>
		/// <param name="configstring">config名称</param>
		/// <param name="TableName"></param>
		public SQL(string configstringName, string TableName)
		{
			this.nFun = nFun;
			this.configStr = configstringName;

			this.TableName = TableName;

			this.msg = "请调open()方法连接数据库";
			//try
			//{
			//    if (this.Connect(this.ConfigStr))
			//    {
			//        this.msg = "第一步打开数库库据成功……请继续第二步";
			//        this.isOpenDataBase = true;

			//    }
			//    else {
			//        this.msg = "第一步打开数据库失败……请查看   configName:" + configstringName + "   ConfigStr:" + this.ConfigStr+"没有异常信息 ";
			//        this.isOpenDataBase = false;
			//    };
			//    //this.Open();
			//}
			//catch (Exception ex) {
			//    this.isOpenDataBase = false;
			//    this.msg = "第一步打开数据库失败……请查看  configName:" + configstringName + "   ConfigStr:" + this.ConfigStr +" 异常信息: " + ex.Message;
			//}

			// this.OpDB(IOpDB);
			// this.Close();
			this.Open();
		}

		public string InsertCmd(string TableName, object[] key, object[] value)
		{
			string sql = "";
			if (NTool.isLenEquals(key, value))
			{
				string val_str = "";
				string _val_str = "";
				for (int i = 0; i < key.Length; i++)
				{
					string insert_Str = string.Format("@{0}", key[i]);
					string _insert_Str = string.Format("{0}", key[i]);
					if (value.Length - 1 <= i || value.Length <= 1)
					{
						_val_str += _insert_Str;
						val_str += insert_Str;
					}
					else if (value.Length > 1)
					{
						val_str += insert_Str + ",";
						_val_str += _insert_Str + ",";
					}

					listSqlParam.Add(insert_Str, value[i] + "");
				}
				sql = string.Format("insert into  {0}({1})    values({2})  ", TableName, _val_str, val_str);
			}

			return sql;
		}

		public string UpdateCmd(string TableName, List<object> _key, List<object> _value, string where)
		{
			object[] key = _key.ToArray();
			object[] value = _value.ToArray();
			string sql = "";
			if (NTool.isLenEquals(key, value))
			{
				string val_str = "";

				for (int i = 0; i < key.Length; i++)
				{
					string upDate_Str = string.Format("@{0}", key[i]);
					string _upDate_Str = string.Format("{0}", key[i]);
					if (value.Length - 1 <= i || value.Length <= 1)
					{
						val_str += _upDate_Str + "=" + upDate_Str;
					}
					else if (value.Length > 1)
					{
						val_str += _upDate_Str + "=" + upDate_Str + ",";
					}

					listSqlParam.Add(upDate_Str, value[i] + "");
				}

				if (!string.IsNullOrEmpty(where))
				{
					where = " where  " + where;
				}
				sql = string.Format("update {0}  set    {1}     {2}   ", TableName, val_str, where);
				//UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson'
			}

			return sql;
		}

		public string UpdateCmd(string TableName, object[] key, object[] value, string where)
		{
			string sql = "";
			if (NTool.isLenEquals(key, value))
			{
				string val_str = "";

				for (int i = 0; i < key.Length; i++)
				{
					string upDate_Str = string.Format("@{0}", key[i]);
					string _upDate_Str = string.Format("{0}", key[i]);
					if (value.Length - 1 <= i || value.Length <= 1)
					{
						val_str += _upDate_Str + "=" + upDate_Str;
					}
					else if (value.Length > 1)
					{
						val_str += _upDate_Str + "=" + upDate_Str + ",";
					}

					listSqlParam.Add(upDate_Str, value[i] + "");
				}

				if (!string.IsNullOrEmpty(where))
				{
					where = " where  " + where;
				}
				sql = string.Format("update {0}  set    {1}     {2}   ", TableName, val_str, where);
				//UPDATE Person SET FirstName = 'Fred' WHERE LastName = 'Wilson'
			}

			return sql;
		}

		public string InsertCmd(string TableName, List<object> _key, List<object> _value)
		{
			object[] key = _key.ToArray();
			object[] value = _value.ToArray();
			string sql = "";
			if (NTool.isLenEquals(key, value))
			{
				string val_str = "";
				string _val_str = "";
				for (int i = 0; i < key.Length; i++)
				{
					string insert_Str = string.Format("@{0}", key[i]);
					string _insert_Str = string.Format("{0}", key[i]);
					if (value.Length - 1 <= i || value.Length <= 1)
					{
						_val_str += _insert_Str;
						val_str += insert_Str;
					}
					else if (value.Length > 1)
					{
						val_str += insert_Str + ",";
						_val_str += _insert_Str + ",";
					}

					listSqlParam.Add(insert_Str, value[i] + "");
				}
				sql = string.Format("insert into  {0}({1})    values({2})  ", TableName, _val_str, val_str);
			}

			return sql;
		}

		public List<string> listDelete_str = new List<string>();

		public string DeleteCmd(string TableName, object[] key, object[] value)
		{
			string sql = "";
			string val_str = "";
			if (NTool.hasData(key) && NTool.hasData(value))
			{
				string Delete_str = string.Format("@{0}", key[0]);

				for (int i = 0; i < value.Length; i++)
				{
					if (value.Length - 1 <= i || value.Length <= 1)
					{
						val_str += Delete_str + i;
					}
					else if (value.Length > 1)
					{
						val_str += Delete_str + i + ",";
					}

					listDelete_str.Add(Delete_str + i);
					listSqlParam.Add(listDelete_str[i], value[i] + "");
				}

				sql = string.Format("delete from  {0}  where  {1}  in({2})", TableName, key[0], val_str);
			}
			else
			{
				sql = string.Format("DELETE FROM {0}", TableName);
			}

			return sql;
		}

		public abstract bool Close();

		public abstract bool Connect(string sql);

		public abstract bool Open();

		public abstract long Edit(List<long> editIds, System.Data.DataTable editTable);

		public abstract bool Edit(long EidtID, System.Data.DataTable editTable);

		public abstract System.Data.DataTable GetTable(List<long> getTableID);

		public abstract bool Add(System.Data.DataTable addTable);

		/// <summary>
		/// 没有防注入
		/// </summary>
		/// <param name="getsWhere"></param>
		/// <returns></returns>
		public abstract System.Data.DataTable GetTable(string getsWhere);

		public abstract System.Data.DataTable GetTable(string getsWhere, int top, string orders);

		public abstract System.Data.DataTable GetTable(List<long> getTableID, string orders);

		public abstract System.Data.DataTable GetTable(string getsWhere, int top);

		public abstract System.Data.DataTable GetTable(long getTableID);

		public abstract System.Data.DataTable GetTable(string getsWhere, List<string> keys, List<object> values);

		public abstract System.Data.DataTable GetTable(string getsWhere, string orders);

		public abstract bool Edit(string editWhere, System.Data.DataTable editTable);

		public abstract long Del(string delWhere);

		public abstract long Del(List<long> delListID);

		public abstract bool Del(long delID);

		public abstract long DUI(string cmd);

		public abstract object GetObject(string cmd);
	}
}