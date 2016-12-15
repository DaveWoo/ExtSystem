using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Tool
{
	public class NFactory
	{
		public static string NohtmlStrFormat(string str, int n, string endStr)
		{
			str = NoHTML(str);
			string temp = string.Empty;
			if (System.Text.Encoding.Default.GetByteCount(str) <= n)//如果长度比需要的长度n小,返回原字符串
			{
				return str;
			}
			else
			{
				int t = 0;
				char[] q = str.ToCharArray();
				for (int i = 0; i < q.Length && t < n; i++)
				{
					if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)//是否汉字
					{
						temp += q[i];
						t += 2;
					}
					else
					{
						temp += q[i];
						t++;
					}
				}
				return (temp + endStr);
			}
		}

		/**/

		/// <summary>

		/// 去除HTML标记

		/// </summary>

		/// <param name="NoHTML">包括HTML的源码 </param>

		/// <returns>已经去除后的文字</returns>

		public static string NoHTML(string Htmlstring)
		{
			//删除脚本

			Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",

			RegexOptions.IgnoreCase);

			//删除HTML

			Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",

			RegexOptions.IgnoreCase);

			Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",

			RegexOptions.IgnoreCase);

			Htmlstring.Replace("<", "");

			Htmlstring.Replace(">", "");

			Htmlstring.Replace("\r\n", "");

			Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

			return Htmlstring;
		}

		public static string GetString(string str, int length, string endStr)
		{
			int i = 0, j = 0;
			foreach (char chr in str)
			{
				if ((int)chr > 127)
				{
					i += 2;
				}
				else
				{
					i++;
				}
				if (i > length)
				{
					str = str.Substring(0, j) + endStr;
					break;
				}
				j++;
			}
			return str;
		}

		public static string StringFormat(string str, int n, string endStr)
		{
			///
			///格式化字符串长度，超出部分显示省略号,区分汉字跟字母。汉字2个字节，字母数字一个字节
			///
			string temp = string.Empty;
			if (System.Text.Encoding.Default.GetByteCount(str) <= n)//如果长度比需要的长度n小,返回原字符串
			{
				return str;
			}
			else
			{
				int t = 0;
				char[] q = str.ToCharArray();
				for (int i = 0; i < q.Length && t < n; i++)
				{
					if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)//是否汉字
					{
						temp += q[i];
						t += 2;
					}
					else
					{
						temp += q[i];
						t++;
					}
				}
				return (temp + endStr);
			}
		}

		public static string _GetString(string str, int length, string endStr)
		{
			int i = 0, j = 0;
			foreach (char chr in str)
			{
				if ((int)chr > 127)
				{
					i += 2;
				}
				else
				{
					i++;
				}
				if (i > length)
				{
					str = str.Substring(0, j) + endStr;
					break;
				}
				j++;
			}
			return str;
		}

		public static string _StringFormat(string str, int n, string endStr)
		{
			///
			///格式化字符串长度，超出部分显示省略号,区分汉字跟字母。汉字2个字节，字母数字一个字节
			///
			string temp = string.Empty;
			if (System.Text.Encoding.Default.GetByteCount(str) <= n)//如果长度比需要的长度n小,返回原字符串
			{
				return str;
			}
			else
			{
				int t = 0;
				char[] q = str.ToCharArray();
				for (int i = 0; i < q.Length && t < n; i++)
				{
					if ((int)q[i] >= 0x4E00 && (int)q[i] <= 0x9FA5)//是否汉字
					{
						temp += q[i];
						t += 2;
					}
					else
					{
						temp += q[i];
						t++;
					}
				}
				return (temp + endStr);
			}
		}

		/// <summary>
		/// 填充对象列表：用DataTable填充实体类
		/// </summary>
		public static List<N> FillModel<N>(DataTable dt)
		{
			if (dt == null || dt.Rows.Count == 0)
			{
				return null;
			}
			List<N> modelList = new List<N>();
			foreach (DataRow dr in dt.Rows)
			{
				N model = (N)Activator.CreateInstance(typeof(N));
				//  N model = (N)Activator.CreateInstance();
				for (int i = 0; i < dr.Table.Columns.Count; i++)
				{
					PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
					if (propertyInfo != null && dr[i] != DBNull.Value)

						propertyInfo.SetValue(model, NTool.HConvertByType(dr[i].ToString(), propertyInfo.PropertyType), null);
				}

				modelList.Add(model);
			}
			return modelList;
		}

		/// <summary>
		/// 填充对象：用DataRow填充实体类
		/// </summary>
		public static N FillModel<N>(DataRow dr)
		{
			if (dr == null)
			{
				return default(N);
			}

			//T model = (T)Activator.CreateInstance(typeof(T));
			N model = (N)Activator.CreateInstance(typeof(N));

			for (int i = 0; i < dr.Table.Columns.Count; i++)
			{
				PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
				if (propertyInfo != null && dr[i] != DBNull.Value)
					propertyInfo.SetValue(model, dr[i], null);
			}
			return model;
		}

		public static DataTable CreateData<N>(N model)
		{
			DataTable dataTable = new DataTable(typeof(N).Name);
			foreach (PropertyInfo propertyInfo in typeof(N).GetProperties())
			{
				dataTable.Columns.Add(new DataColumn(propertyInfo.Name, propertyInfo.PropertyType));
			}
			return dataTable;
		}

		/// <summary>
		/// 实体类转换成DataTable
		/// </summary>
		/// <param name="modelList">实体类列表</param>
		/// <returns></returns>
		public static DataTable FillDataTable<N>(List<N> modelList)
		{
			if (modelList == null || modelList.Count == 0)
			{
				return null;
			}
			DataTable dt = CreateData(modelList[0]);

			foreach (N model in modelList)
			{
				DataRow dataRow = dt.NewRow();
				foreach (PropertyInfo propertyInfo in typeof(N).GetProperties())
				{
					if (propertyInfo != null && propertyInfo.GetValue(model, null) != DBNull.Value)
						dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
				}
				dt.Rows.Add(dataRow);
			}
			return dt;
		}
	}
}