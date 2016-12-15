using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Tool
{
	public class NStr
	{
		public static DataTable JsonToDataTable(string strJson)
		{
			//取出表名
			Regex rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
			string strName = rg.Match(strJson).Value;
			DataTable tb = null;
			string Str_Head = "";

			//去除表名
			strJson = strJson.Substring(strJson.IndexOf("[") + 1);
			strJson = strJson.Substring(0, strJson.IndexOf("]"));

			//获取数据
			rg = new Regex(@"(?<={)[^}]+(?=})");
			MatchCollection mc = rg.Matches(strJson);
			for (int i = 0; i < mc.Count; i++)
			{
				string strRow = mc[i].Value;
				string[] strRows = strRow.Split(',');

				//创建表
				if (tb == null)
				{
					tb = new DataTable();
					tb.TableName = strName;
					foreach (string str in strRows)
					{
						DataColumn dc = new DataColumn();
						string[] strCell = str.Split(':');

						Str_Head = strCell[0].ToString().Substring(0, strCell[0].ToString().Length - 1);//去除最后一位字符串
						Str_Head = Str_Head.Substring(1);//去除第一位字符串
						dc.ColumnName = Str_Head;
						//dc.ColumnName = strCell[0].ToString();

						tb.Columns.Add(dc);
					}
					tb.AcceptChanges();
				}

				//增加内容
				DataRow dr = tb.NewRow();
				for (int r = 0; r < strRows.Length; r++)
				{
					//Str_Cell = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("/", "").Substring(0, strRows[1].ToString().Length - 1);
					//Str_Cell = Str_Cell.Substring(1);
					//dr[r] = Str_Cell;
					dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("/", "");
					dr[r] = dr[r].ToString().Substring(0, Convert.ToString(dr[r]).Length - 1);//去除最后一位字符串
					dr[r] = Convert.ToString(dr[r]).Substring(1);//去除第一位字符串
				}
				tb.Rows.Add(dr);
				tb.AcceptChanges();
			}

			return tb;
		}

		public static string GetToDataStr(DateTime? begin, string format)
		{
			if (begin != null)
			{
				return begin.Value.ToString(format);
			}
			return "";
		}

		public static string GetDateTimeStr(DateTime? begin)
		{
			DateTime? dtA = begin;
			DateTime? dtB = DateTime.Now;
			TimeSpan? mm = dtB - dtA;
			try
			{
				if (mm.Value.TotalMinutes <= 59)
				{
					return string.Format("{0}分钟前", mm.Value.Minutes);
				}
				else if (mm.Value.TotalMinutes >= 60 && mm.Value.TotalMinutes <= 360)
				{
					if (mm.Value.TotalMinutes % 60 == 0)
					{
						return string.Format("{0}小时前", (int)mm.Value.TotalMinutes / 60);
					}
					else
					{
						var nh = (int)mm.Value.TotalMinutes / 60;
						var nm = (int)mm.Value.TotalMinutes - nh * 60;

						return string.Format("{0}小时{1}分钟前", nh, nm);
					}
				}
				else if ((int)mm.Value.TotalDays == 0)
				{
					return string.Format("今天");
				}
				else if ((int)mm.Value.TotalDays == 1)
				{
					return string.Format("昨天");
				}
				else if ((int)mm.Value.TotalDays >= 2)
				{
					return string.Format((int)mm.Value.TotalDays + "天前");
				}
			}
			catch (Exception ex)
			{
			}
			return "无时间";
		}

		public static string UrlStr(string key, string val, bool isChar = true)
		{
			StringBuilder sb = new StringBuilder();
			bool IsEmpty = string.IsNullOrEmpty(key);
			if (!IsEmpty)
			{
				if (isChar)
					sb.AppendFormat("&{0}={1}", key, val);
				else
					sb.AppendFormat("{0}={1}", key, val);
			}
			return sb.ToString();
		}

		public static string AppendUrl(string[] keys, string[] val)
		{
			StringBuilder sb = new StringBuilder();
			byte count = 0, ct = 0;
			foreach (string key in keys)
			{
				bool IsEmpty = string.IsNullOrEmpty(key);
				if (!IsEmpty)
				{
					if (count <= 0)
					{
						sb.AppendFormat("{0}={1}", key, val[ct]);
					}
					else
					{
						sb.AppendFormat("&{0}={1}", key, val[ct]);
					}

					count++;
				}
				else
				{
				}
				ct++;
			}
			return sb.ToString();
		}

		public static string AppendUrlHasChr(string url, Dictionary<string, string> dict)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(url);

			foreach (string key in dict.Keys)
			{
				bool IsEmpty = string.IsNullOrEmpty(dict[key]);
				if (!IsEmpty)
				{
					sb.AppendFormat("&{0}={1}", key, dict[key]);
				}
			}

			return sb.ToString();
		}

		public static string AppendUrl(string url, Dictionary<string, string> dict)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(url);
			sb.Append("?");
			byte count = 0;
			foreach (string key in dict.Keys)
			{
				bool IsEmpty = string.IsNullOrEmpty(dict[key]);
				if (!IsEmpty)
				{
					if (count <= 0)
					{
						sb.AppendFormat("{0}={1}", key, dict[key]);
					}
					else
					{
						sb.AppendFormat("&{0}={1}", key, dict[key]);
					}

					count++;
				}
			}

			return sb.ToString();
		}

		public static void SetSession(string key, object val)
		{
			HttpContext.Current.Session[key] = val;
		}

		public static bool hasSessionValue(string name)
		{
			bool has = HttpContext.Current.Session[name] != null && (HttpContext.Current.Session[name] + "").Length > 3;

			if (has)
			{
				return true;
			}

			return false;
		}

		public delegate string SetDelegateResult<T>(List<T> _menu, T t, int Level, int? rows);

		public delegate dynamic SetProcessResult<T>(T t, List<T> _menu, int Level);

		public static string JsonStr(bool isAddEnd = false, params string[] str)
		{
			StringBuilder _str = new StringBuilder();

			//  ("\"id\":\"{0}\",\"text\":\"{1}\",\"iconCls\":\"icon-add\"", _chlidModel.ID, _chlidModel.Name);

			if (NTool.IsArrNULL<string>(str) && str.Length < 5000)
			{
				if (isAddEnd)
					_str.Append("{");
				string key = "", val = "";
				for (int i = 1; i <= str.Length; i++)
				{
					//双数

					if (i % 2 == 0)
					{
						val = "" + str[i - 1];

						if (i >= str.Length)
						{
							_str.AppendFormat("\"{0}\":\"{1}\"", key, val);
						}
						else
						{
							_str.AppendFormat("\"{0}\":\"{1}\",", key, val);
						}
					}
					else
					{
						key = "" + str[i - 1];
					}
				}
				if (isAddEnd)
					_str.Append("} ");
			}

			return _str.ToString();
		}

		/// <summary>
		/// 设置真假 Tool.N_STR.SetStatusMsg(0,"启用","禁用"));
		/// </summary>
		/// <param name="status">int 型  0-1</param>
		/// <param name="YesOrNoMsg">必两个 如设：启用和禁用 </param>
		/// <returns>如设：启用和禁用 </returns>
		public static string SetStatusMsg(int? status, params string[] YesOrNoMsg)
		{
			if (YesOrNoMsg != null && YesOrNoMsg.Length > 1)
			{
				if (status == null)
				{
					return YesOrNoMsg[1];
				}
				else if (status == 1)
				{
					return YesOrNoMsg[0];
				}
				else
				{
					return YesOrNoMsg[1];
				}
			}

			return "";
		}

		/// <summary>
		/// 对字符串进行检查和替换其中的特殊字符
		/// </summary>
		/// <param name="strHtml"></param>
		/// <returns></returns>
		public static string HtmlToTxt(string strHtml)
		{
			string[] aryReg ={
                        @"<script[^>]*?>.*?</script>",
                        @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                        @"([\r\n])[\s]+",
                        @"&(quot|#34);",
                        @"&(amp|#38);",
                        @"&(lt|#60);",
                        @"&(gt|#62);",
                        @"&(nbsp|#160);",
                        @"&(iexcl|#161);",
                        @"&(cent|#162);",
                        @"&(pound|#163);",
                        @"&(copy|#169);",
                        @"&#(\d+);",
                        @"-->",
                        @"<!--.*\n"
                        };

			string newReg = aryReg[0];
			string strOutput = strHtml;
			for (int i = 0; i < aryReg.Length; i++)
			{
				Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
				strOutput = regex.Replace(strOutput, string.Empty);
			}

			strOutput.Replace("<", "");
			strOutput.Replace(">", "");
			strOutput.Replace("\r\n", "");

			return strOutput;
		}

		/// <summary>
		/// 替换html中的特殊字符
		/// </summary>
		/// <param name="theString">需要进行替换的文本。</param>
		/// <returns>替换完的文本。</returns>
		public static string HtmlEncode(string theString)
		{
			theString = theString.Replace(">", "&gt;");
			theString = theString.Replace("<", "&lt;");
			theString = theString.Replace(" ", "&nbsp;");
			theString = theString.Replace(" ", "&nbsp;");
			theString = theString.Replace("\"", "&quot;");
			theString = theString.Replace("\'", "'");
			theString = theString.Replace("\n", "<br/> ");
			return theString;
		}

		/// <summary>
		/// 恢复html中的特殊字符
		/// </summary>
		/// <param name="theString">需要恢复的文本。</param>
		/// <returns>恢复好的文本。</returns>
		public static string HtmlDiscode(string theString)
		{
			theString = theString.Replace("&gt;", ">");
			theString = theString.Replace("&lt;", "<");
			theString = theString.Replace("&nbsp;", " ");
			theString = theString.Replace("&nbsp;", " ");
			theString = theString.Replace("&quot;", "\"");
			theString = theString.Replace("'", "\'");
			theString = theString.Replace("<br/> ", "\n");
			return theString;
		}

		public static string NumCodeAdd(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return "01";
			}
			else
			{
				bool isZero = (code + "").Substring(0, 1).Equals("0");
				if (isZero)
				{
					string numcode = code.Substring(1, (code + "").Length - 1);
					code = "0" + (long.Parse(numcode) + 1);
					if (numcode.Length >= 4)
					{
					}

					return code;
				}
				else
				{
					return (long.Parse(code) + 1) + "";
				}
			}
		}

		/// 要转换的数组
		/// 转换后的数组类型
		public static Array ConvertArray(Array srcArray, System.Type elmType)
		{
			if (srcArray == null)
				throw new ArgumentNullException("srcArray");
			int len = srcArray.Length;
			Array a = Array.CreateInstance(elmType, len);
			for (int i = 0; i < len; i++)
				a.SetValue(srcArray.GetValue(i), i);
			return a;
		}

		public static string GetArrayString(object[] obj)
		{
			string splitStr = ",";
			if (obj != null)
			{
				if (obj.Length <= 1)
				{
					return obj[0] + "";
				}
				StringBuilder str = new StringBuilder();
				for (int i = 0; i < obj.Length; i++)
				{
					if (i >= obj.Length - 1)
					{
						str.AppendFormat("{0}", obj[i]);
					}
					else
					{
						str.AppendFormat("{0}{1}", obj[i], splitStr);
					}
				}

				return str.ToString();
			}

			return "";
		}

		public static string GetDictionaryString(NModel.EnObject.EDisplayType eDisplayType, Dictionary<string, string> dc, int maxShow)
		{
			string takeStr = "";

			if (!NTool.IsDcNULL<string, string>(dc))
				return "暂时没有数据";

			if (maxShow < dc.Count)
			{
				return "超出长度:" + dc.Count + "个";
			}

			StringBuilder str = new StringBuilder();

			switch (eDisplayType)
			{
				case NModel.EnObject.EDisplayType.eJson:

					str.Append("{");
					int count = 0;
					foreach (KeyValuePair<string, string> kpr in dc)
					{
						count++;

						if (dc.Count <= count)
						{
							str.AppendFormat("\"{0}\":\"{1}\"", kpr.Key, kpr.Value);
						}
						else
						{
							str.AppendFormat("\"{0}\":\"{1}\",", kpr.Key, kpr.Value);
						}
					}

					str.Append("}");

					takeStr = str.ToString();

					break;

				case NModel.EnObject.EDisplayType.eText:

					break;
				case NModel.EnObject.EDisplayType.eHtml:

					break;
				case NModel.EnObject.EDisplayType.eJs:

					break;

				case NModel.EnObject.EDisplayType.eXml:

					break;

				case NModel.EnObject.EDisplayType.eCss:

					break;
				default:
					break;
			}

			return takeStr;
		}

		public static void SetModelFieldVal(PropertyInfo[] pi, ref Object obj, string keyPostOrGet, string val)
		{
			List<PropertyInfo> kvp = pi.Where(a => a.Name.ToLower().Trim().Equals(keyPostOrGet.ToLower().Trim())).Skip(0).Take(1).ToList();

			if (kvp != null && kvp.Count > 0)
			{
				Type tp = kvp[0].PropertyType;

				if (string.IsNullOrEmpty(val))
				{
					return;
				}

				object _o = NTool.HConvertByType(val, tp);

				kvp[0].SetValue(obj, _o, null);
			}
		}

		public static bool PostForSetVal<T>(ref T SetModelVal, ref string Json, string splitStr)
		{
			NameValueCollection nvalCn = HttpContext.Current.Request.Form;
			Dictionary<string, string> dict = new Dictionary<string, string>();

			Type tpe = typeof(T);
			PropertyInfo[] pi = tpe.GetProperties();
			object obj = tpe.Assembly.CreateInstance(tpe.FullName);
			if (nvalCn != null && pi.Length >= nvalCn.Count && nvalCn.Count > 0 && nvalCn.Count <= 80)
			{
				for (int i = 0; i < nvalCn.Count; i++)
				{
					string ky = nvalCn.Keys[i];
					string vl = nvalCn[i];
					dict.Add(ky, vl);

					try
					{
						string[] ksplit = ky.Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries);

						if (ksplit != null && splitStr.Length > 1)
						{
							ky = ksplit[1];
						}

						SetModelFieldVal(pi, ref obj, ky, vl);
					}
					catch (Exception ex)
					{
					}
				}

				SetModelVal = (T)obj;
				Json = GetDictionaryString(NModel.EnObject.EDisplayType.eJson, dict, 80);
			}
			return true;
			//  return  ModelSetVal<T>(ref SetModelVal, vl);
		}

		public static string SetJsParameter(object obj, string mothodName)
		{
			Type tpe = obj.GetType();
			PropertyInfo[] pi = tpe.GetProperties();
			// object obj = tpe.Assembly.CreateInstance(tpe.FullName);
			StringBuilder str = new StringBuilder();
			str.AppendFormat("  var {0}_{1}_class = $(\".{0}_{1}\")  ", mothodName, obj.GetType().Name);

			str.AppendFormat(" var   {0}_{1}_Len= {0}_{1}_class.Length  ", mothodName, obj.GetType().Name);
			str.AppendFormat("");

			foreach (PropertyInfo pInfo in pi)
			{
				str.AppendFormat("  var {0}_{1}_class = $(.{0}_{1})  ", mothodName, obj.GetType().Name);
			}
			return "";
		}

		public static bool GetForSetVal<T>(ref T SetModelVal, ref string Json, string splitStr)
		{
			NameValueCollection nvalCn = HttpContext.Current.Request.QueryString;
			Dictionary<string, string> dict = new Dictionary<string, string>();

			Type tpe = typeof(T);
			PropertyInfo[] pi = tpe.GetProperties();
			object obj = tpe.Assembly.CreateInstance(tpe.FullName);
			if (nvalCn != null && pi.Length >= nvalCn.Count && nvalCn.Count > 0 && nvalCn.Count <= 80)
			{
				for (int i = 0; i < nvalCn.Count; i++)
				{
					string key = nvalCn.Keys[i];
					string val = nvalCn[i];

					string[] ksplit = key.Split(new string[] { splitStr }, StringSplitOptions.RemoveEmptyEntries);

					if (ksplit != null && splitStr.Length > 1)
					{
						key = ksplit[1];
					}
					dict.Add(key, val);

					SetModelFieldVal(pi, ref obj, key, val);

					//if (string.IsNullOrEmpty(splitStr))
					//{
					//    vl.Add(key, val);
					//}
					//else
					//{
					//    string[] newKeys = key.Split(new string[] { splitStr }, StringSplitOptions.None);
					//    if (newKeys != null && newKeys.Length > 1)
					//    {
					//        vl.Add(newKeys[1], val);

					//    }
					//    else
					//    {
					//        vl.Add(key, val);
					//    }

					//}
				}

				SetModelVal = (T)obj;

				Json = GetDictionaryString(NModel.EnObject.EDisplayType.eJson, dict, 80);
			}

			return true;
			// return ModelSetVal<T>(ref SetModelVal, vl);
		}

		public static string GetArrayString(string _splitChar, object[] obj)
		{
			string splitStr = _splitChar;
			if (obj != null)
			{
				if (obj.Length <= 1)
				{
					return obj[0] + "";
				}
				StringBuilder str = new StringBuilder();
				for (int i = 0; i < obj.Length; i++)
				{
					if (i >= obj.Length - 1)
					{
						str.AppendFormat("{0}", obj);
					}
					else
					{
						str.AppendFormat("{0}{1}", obj, splitStr);
					}
				}

				return str.ToString();
			}

			return "";
		}
	}
}