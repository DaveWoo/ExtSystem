using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Tool
{
	public class NWeb
	{
		/// <summary>
		/// 获取HTML源码信息(Porschev)
		/// </summary>
		/// <param name="url">获取地址</param>
		/// <returns>HTML源码</returns>
		public static string GetHtml(string url)
		{
			string strResult;
			try
			{
				using (var client = new WebClient())
				{
					strResult = client.DownloadString(url);
				}
			}
			catch (Exception ee)
			{
				strResult = ee.Message;
			}
			return strResult;
		}

		/// <summary>
		/// 得到真实IP以及所在地详细信息（Porschev）
		/// </summary>
		/// <returns></returns>
		public static string GetIpDetails()
		{
			//设置获取IP地址和国家源码的网址
			string url = "http://www.ip138.com/ips8.asp";
			string regStr = "(?<=<td\\s*align=\\\"center\\\">)[^<]*?(?=<br/><br/></td>)";
			//IP正则
			string ipRegStr = "((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)";
			//IP地址
			string ip = string.Empty;
			//国家
			string country = string.Empty;
			//省市
			string adr = string.Empty;
			//得到网页源码
			string html = GetHtml(url);
			;
			Regex reg = new Regex(regStr, RegexOptions.None);
			Match ma = reg.Match(html);
			html = ma.Value;
			Regex ipReg = new Regex(ipRegStr, RegexOptions.None);
			ma = ipReg.Match(html);
			//得到IP
			ip = ma.Value;
			int index = html.LastIndexOf(":") + 1;
			//得到国家
			country = html.Substring(index);
			adr = GetAddresByIp(ip);
			return "IP：" + ip + " 国家：" + country + " 省市：" + adr;
		}

		/// <summary>
		/// 通过IP得到IP所在地省市（Porschev）
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static string GetAddresByIp(string ip)
		{
			try
			{
				string url = "";
				url = "http://api.map.baidu.com/location/ip?ak=Db65kGLt5yMoGCGSWX7ddvT3";

				string html = GetHtml(url);
				Regex reg = new Regex(@"""city"":"".{0,}?""", RegexOptions.Multiline);

				Match news = reg.Match(html);

				string[] ll = news.Value.Split(':');
				//encodeURI
				//HttpContext.Current.Server.HtmlEncode(ll[1]);

				return JsonConvert.DeserializeObject(ll[1]) + "";
			}
			catch { return "无网络"; }
		}

		public static string NGet(string url)
		{
			using (var client = new WebClient())
			{
				return client.DownloadString(url);
			}
		}

		public static string NPost(string url, Dictionary<string, string> dictKY)
		{
			if (dictKY == null)
				return "";
			string PostString = "";
			for (int idict = 0; idict < dictKY.Count; idict++)
			{
				string[] arrKey = dictKY.Keys.ToArray();
				string[] arrVal = dictKY.Values.ToArray();
				string key = arrKey[idict];
				string val = arrVal[idict];
				if (dictKY.Count - 1 <= idict)
				{
					PostString += key + "=" + val;
				}
				else
				{
					PostString += key + "=" + val + "&";
				}
			}

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

			request.Method = "POST";  //设置POST请求模式

			//将参数字符串转换成字节数组。

			byte[] PostData = System.Text.Encoding.UTF8.GetBytes(PostString);

			request.ContentType = "application/x-www-form-urlencoded";  //设置ContntType ，这句很重要，否则无法传递参数

			request.ContentLength = PostData.Length;                  //设置请求内容大小，当然就设置成我们的参数字节数据大小。

			Stream requestStream = request.GetRequestStream();        //获取请求流

			requestStream.Write(PostData, 0, PostData.Length);        //将参数字节数组写入到请求流里

			requestStream.Close();                                    //关闭请求流

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();        //执行请求，获取响应对象

			Stream stream = response.GetResponseStream();                            //获取响应流

			StreamReader sr = new StreamReader(stream);                              //创建流读取对象

			string responseHTML = sr.ReadToEnd();                      //读取响应流
			response.Close();

			return responseHTML;
		}
	}
}