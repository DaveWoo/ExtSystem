using System;
using System.Collections.Generic;
using System.Data;

namespace IDAL
{
	public interface ISQLAbl
	{
		/// <summary>
		/// 连接数据库
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		bool Connect(String sql); //连接数据库

		bool Open(); //打开数据库

		bool Close();// 关闭数据库

		DataTable GetTable(long getTableID);

		DataTable GetTable(List<long> getTableID);

		DataTable GetTable(string getsWhere);

		DataTable GetTable(string getsWhere, List<String> keys, List<object> values);

		DataTable GetTable(string getsWhere, int top);

		DataTable GetTable(string getsWhere, string orders);

		DataTable GetTable(List<long> getTableID, string orders);

		DataTable GetTable(string getsWhere, int top, string orders);

		bool Edit(long EidtID, DataTable editTable); //编辑ID

		bool Edit(String editWhere, DataTable editTable); //编辑ID

		long Edit(List<long> editIds, DataTable editTable);// 编辑多ID

		bool Add(DataTable addTable);//增加表单

		long DUI(string cmd);

		object GetObject(string cmd);

		bool Del(long delID);

		long Del(List<long> delListID);

		long Del(string delWhere);
	}
}