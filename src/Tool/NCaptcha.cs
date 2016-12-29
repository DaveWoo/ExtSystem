using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;
using NModel;

namespace Tool
{
	/// <summary>
	/// 随便生成字符 SessionName="Session_Code"
	/// </summary>
	public class NCaptcha
	{
		private Random RSeed = new Random();
		public EnImage enImage { get; set; }
		private string setSessionName;

		/// <summary>
		/// 默认Session_Code
		/// </summary>
		public string SetSessionName
		{
			get
			{
				if (String.IsNullOrEmpty(this.setSessionName))
				{
					return "Session_Code";
				}
				else { return this.setSessionName; }
			}
			set
			{
				this.setSessionName = value;
			}
		}

		public string GetNCaptchaSessionVal { get { return HttpContext.Current.Session[this.SetSessionName] + ""; } }

		/// <summary>
		/// 生成数量
		/// </summary>
		public byte GenerateCount { set; get; }

		public string GenerateString { set; get; }

		private bool isSetSession;

		public bool IsSetSession
		{
			get { return isSetSession; }
			set
			{
				isSetSession = value;
				if (isSetSession)
				{
					HttpContext.Current.Session[this.SetSessionName] = (this.GenerateString + "").ToLower();
				}
			}
		}

		private byte showCode;

		protected byte ShowCode
		{
			get { return showCode; }
			set
			{
				if (value >= 1 && value <= 128)
				{
					this.showCode = value;
				}
				else if (value <= 0)
				{
				}
				else
				{
					this.showCode = 5;
				}
			}
		}

		private byte minShowCode;

		protected byte MinShowCode
		{
			get { return minShowCode; }
			set
			{
				if (this.MaxShowCode > value && value >= 1)
				{
					minShowCode = value;
				}
				else
				{
					minShowCode = 1;
				}
			}
		}

		private byte maxShowCode;

		protected byte MaxShowCode
		{
			get { return maxShowCode; }
			set
			{
				if (this.MinShowCode < value && value >= 1 && value <= 128)
				{
					this.maxShowCode = value;
				}
				else
				{
					this.maxShowCode = 128;
				}
			}
		}

		public Bitmap gBitmap { get; set; }

		protected NCaptcha()
			: this(5)
		{
		}

		/// <summary>
		/// 随机种子数
		/// </summary>
		private List<string> listRandom = new List<string>();

		/// <summary>
		/// 生成验证码
		/// </summary>
		/// <param name="showCode">生成1-128位字符 0表示随机4-8位字</param>
		protected NCaptcha(byte showCode)
		{
			this.ShowCode = showCode;
			this.GenerateString = GenerateCode(this.ShowCode);
		}

		/// <summary>
		///  随机种子数
		/// </summary>
		/// <param name="minShowCode">最小1位置</param>
		/// <param name="maxShowCode">最大128位置</param>
		/// <param name="isSetSession">是否保存Session中</param>
		/// <returns>对象</returns>
		protected NCaptcha(byte minShowCode, byte maxShowCode)
		{
			this.MinShowCode = minShowCode;
			this.MaxShowCode = maxShowCode;

			this.GenerateString = GenerateCode(RSeed.Next(this.MinShowCode, this.MaxShowCode));
		}

		private string GenerateCode(int _SeedCount)
		{
			int gSeed = _SeedCount;
			StringBuilder code_str = new StringBuilder();
			for (int i = 0; i < gSeed; i++)
			{
				string str = "";
				int isCharFlag = RSeed.Next(0, gSeed);

				if (isCharFlag == i)
				{
					str = ((char)RSeed.Next(65, 90)) + "";
				}
				else
				{
					str = RSeed.Next(1, 9) + "";
				}
				code_str.Append(str);
			}

			this.GenerateCount = (byte)gSeed;
			return code_str.ToString();
		}

		public static NCaptcha Generate(byte showCount, bool isSetSession)
		{
			NCaptcha code = new NCaptcha(showCount);
			code.IsSetSession = isSetSession;

			return code;
		}

		/// <summary>
		/// 生成随机字符串并生成图片
		/// </summary>
		/// <param name="showCount">字符串长度</param>
		/// <param name="outStream">输出流，可以为NULL</param>
		/// <param name="iformat">图片类型</param>
		/// <param name="isSetSession">是否保存Session中</param>
		/// <returns>返回对象</returns>
		public static NCaptcha GenerateAndDraw(byte showCount, System.IO.Stream outStream, System.Drawing.Imaging.ImageFormat iformat, bool isSetSession)
		{
			NCaptcha code = new NCaptcha(showCount);
			code.IsSetSession = isSetSession;

			EnFont font = new EnFont();
			font.MyFont = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold, GraphicsUnit.Point);
			font.MyColor = EnObject.listColor[0];
			EnImage img = new EnImage();
			img.ImgColor = Color.White;
			;
			img.Width = code.GenerateCount * (int)font.MyFont.Size;
			img.Height = (int)(font.MyFont.Size) + 5;

			code.gBitmap = code.gDrawing(EnImage.PaintShape.Link, img, font);
			if (code.gBitmap != null && outStream != null)
				code.gBitmap.Save(outStream, iformat);
			return code;
		}

		/// <summary>
		/// 生成随机字符串并生成jpeg图片
		/// </summary>
		/// <param name="showCount">字符串长度</param>
		/// <param name="outStream">输出流，可以为NULL</param>
		/// <param name="enImg">生成图片属性</param>
		/// <param name="isSetSession">是否保存Session中</param>
		/// <returns>返回对象</returns>
		public static NCaptcha GenerateAndDraw(byte showCount, System.IO.Stream outStream, EnImage enImg, bool isSetSession)
		{
			NCaptcha code = new NCaptcha(showCount);
			code.IsSetSession = isSetSession;

			EnFont font = new EnFont();
			font.MyFont = enImg.ImgFont;
			font.MyColor = enImg.ImgColor;
			EnImage img = new EnImage();
			img.ImgColor = Color.White;
			;
			img.Width = enImg.Width;
			img.Height = enImg.Height;

			code.gBitmap = code.gDrawing(EnImage.PaintShape.Link, img, font);
			if (code.gBitmap != null && outStream != null)
				code.gBitmap.Save(outStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			return code;
		}

		public static NCaptcha GenerateAndDraw(byte showCount, System.IO.Stream outStream, EnImage enImg, string SetSessionName)
		{
			NCaptcha code = new NCaptcha(showCount);
			code.SetSessionName = SetSessionName;
			code.IsSetSession = true;

			EnFont font = new EnFont();
			font.MyFont = enImg.ImgFont;
			font.MyColor = enImg.ImgColor;
			EnImage img = new EnImage();
			img.ImgColor = Color.White;
			;
			img.Width = enImg.Width;
			img.Height = enImg.Height;

			code.gBitmap = code.gDrawing(EnImage.PaintShape.Link, img, font);
			if (code.gBitmap != null && outStream != null)
				code.gBitmap.Save(outStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			return code;
		}

		private Bitmap gDrawing(EnImage.PaintShape ps, EnImage enImg, EnFont font)
		{
			Bitmap gBitmap = new Bitmap(enImg.Width, enImg.Height);
			Graphics gp = Graphics.FromImage(gBitmap);

			gp.Clear(enImg.ImgColor);
			if (!string.IsNullOrEmpty(this.GenerateString))
			{
				List<EnFont> listFont = new List<EnFont>();

				for (int i = 0; i < this.GenerateString.Length; i++)
				{
					EnFont enFont = new EnFont();
					float y = float.Parse("" + ((enImg.Height / 2 - font.MyFont.Size / 2)));

					//RSeed.Next((int)font.MyFont.Size, gBitmap.Height - (int)(font.MyFont.Size));

					float x = i * font.MyFont.Size;

					enFont.MyColor = EnObject.listColor[0];
					SolidBrush shadowBrush = new SolidBrush(font.MyColor);
					y = RSeed.Next((int)y, (int)font.MyFont.Size / 2);

					enFont.MyPointF = new PointF(x, y);

					listFont.Add(enFont);
					gp.DrawString(this.GenerateString.Substring(i, 1), font.MyFont, shadowBrush, enFont.MyPointF);
				}

				switch (ps)
				{
					case EnImage.PaintShape.Link:

						gp.DrawLine(new Pen(font.MyColor, 1), 0, RSeed.Next((int)(enImg.Height / 2), (int)(enImg.Height * 0.55)), enImg.Width, RSeed.Next((int)(enImg.Height / 3), (int)(enImg.Height * 0.8)));

						gp.DrawLine(new Pen(font.MyColor, 1), 0, RSeed.Next((int)(enImg.Height / 2), (int)(enImg.Height * 0.50)), enImg.Width, RSeed.Next((int)(enImg.Height / 3), (int)(enImg.Height * 0.82)));

						break;
					case EnImage.PaintShape.Dot:

						break;
					case EnImage.PaintShape.Ring:

						break;
					default:

						break;
				}

				gp.Save();
			}

			return gBitmap;
		}

		/// <summary>
		/// 生成5位置随机字符串
		/// </summary>
		/// <param name="isSetSession">是否保存Session中</param>
		/// <returns>对象</returns>
		public static NCaptcha Generate(bool isSetSession)
		{
			NCaptcha code = new NCaptcha();
			code.IsSetSession = isSetSession;

			return code;
		}

		/// <summary>
		/// 生成code
		/// </summary>
		/// <param name="sessionName"> 保存sessionName</param>
		/// <returns></returns>
		public static NCaptcha Generate(string sessionName)
		{
			NCaptcha code = new NCaptcha();
			code.SetSessionName = sessionName;
			code.IsSetSession = true;

			return code;
		}

		/// <summary>
		///  随机种子数
		/// </summary>
		/// <param name="minShowCode">最小1位置</param>
		/// <param name="maxShowCode">最大128位置</param>
		/// <param name="isSetSession">是否保存Session中</param>
		/// <returns>对象</returns>
		public static NCaptcha Generate(byte minShowCode, byte maxShowCode, bool isSetSession)
		{
			NCaptcha code = new NCaptcha(minShowCode, maxShowCode);
			code.IsSetSession = isSetSession;
			return code;
		}
	}
}