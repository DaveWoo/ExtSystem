using System;
using System.Security.Cryptography;
using System.Text;

namespace Tool
{
	public class NMd5
	{
		public static string GetMD5(string Sourcein)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] MD5Source = System.Text.Encoding.UTF8.GetBytes(Sourcein);
			byte[] MD5Out = MD5CSP.ComputeHash(MD5Source);

			return Convert.ToBase64String(MD5Out);
		}

		/// <summary>
		/// 获取MD5得值，没有转换成Base64的
		/// </summary>
		/// <param name="sDataIn">需要加密的字符串</param>
		/// <param name="move">偏移量</param>
		/// <returns>sDataIn加密后的字符串</returns>
		public static string GetMD5(string sDataIn, string move)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] byt, bytHash;
			byt = System.Text.Encoding.UTF8.GetBytes(move + sDataIn);
			bytHash = md5.ComputeHash(byt);
			md5.Clear();
			string sTemp = "";
			for (int i = 0; i < bytHash.Length; i++)
			{
				sTemp += bytHash[i].ToString("x").PadLeft(2, '0');
			}
			return sTemp;
		}

		public static string GetMd5Hash(string input)
		{
			MD5 md5Hash = MD5.Create();

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		public static bool MD5ComparePWD(string pw1, string MD5PW)
		{
			if (string.IsNullOrEmpty(pw1) || string.IsNullOrEmpty(MD5PW))
			{
				return false;
			}
			if (GetMd5Hash(pw1).ToLower().Equals(MD5PW.ToLower()))
			{
				return true;
			}

			return false;
		}
	}
}