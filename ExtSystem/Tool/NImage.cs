using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web;
using NModel;

namespace Tool
{
	public class NImage
	{
		public static Bitmap DrawText(string Text, Color TextColor, EnImage enImg, Font font)
		{
			Bitmap gBitmap = new Bitmap(enImg.Width, enImg.Height);
			Graphics gp = Graphics.FromImage(gBitmap);

			SizeF sf = gp.MeasureString(Text, font);
			gp.Clear(enImg.ImgColor);
			gp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			//设置高质量,低速度呈现平滑程度
			//gp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			var x = enImg.Width / 2 - sf.Width / 2;
			var y = enImg.Height / 2 - sf.Height / 2;
			//30/2  6

			gp.DrawString(Text, font, new SolidBrush(TextColor), new PointF(x, y));
			gp.Save();

			return gBitmap;
		}

		public static void DeleteFolder(string dir)
		{
			if (Directory.Exists(dir)) //如果存在这个文件夹删除之
			{
				foreach (string d in Directory.GetFileSystemEntries(dir))
				{
					if (File.Exists(d))

						File.Delete(d); //直接删除其中的文件
					else

						DeleteFolder(d); //递归删除子文件夹
				}

				Directory.Delete(dir); //删除已空文件夹
			}
		}

		public static bool DelFile(string path)
		{
			string allPath = HttpContext.Current.Server.MapPath(path);
			DeleteFolder(allPath);

			return true;
		}

		public static bool DelImage(string delList)
		{
			string allPath = HttpContext.Current.Server.MapPath(delList);

			if (File.Exists(allPath))
			{
				File.Delete(allPath);
				return true;
			}

			return false;
		}

		public static long DelImages(params string[] delList)
		{
			int longDel = 0;
			foreach (string path in delList)
			{
				string allPath = HttpContext.Current.Server.MapPath(path);

				if (File.Exists(allPath))
				{
					File.Delete(allPath);
					longDel++;
				}
			}

			return longDel;
		}

		/// <summary>
		/// 生成缩略图
		/// </summary>
		/// <param name="originalImagePath">源图路径（物理路径）</param>
		/// <param name="thumbnailPath">缩略图路径（物理路径）</param>
		/// <param name="width">缩略图宽度</param>
		/// <param name="height">缩略图高度</param>
		/// <param name="mode">生成缩略图的方式</param>
		public static void MakeThumbnail(System.Drawing.Image _originalImage, string thumbnailPath, int width, int height, string mode)
		{
			System.Drawing.Image originalImage = _originalImage;

			int towidth = width;
			int toheight = height;

			int x = 0;
			int y = 0;
			int ow = originalImage.Width;
			int oh = originalImage.Height;

			switch (mode)
			{
				case "HW"://指定高宽缩放（可能变形）
					break;
				case "W"://指定宽，高按比例
					toheight = originalImage.Height * width / originalImage.Width;
					break;
				case "H"://指定高，宽按比例
					towidth = originalImage.Width * height / originalImage.Height;
					break;
				case "Cut"://指定高宽裁减（不变形）
					if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
					{
						oh = originalImage.Height;
						ow = originalImage.Height * towidth / toheight;
						y = 0;
						x = (originalImage.Width - ow) / 2;
					}
					else
					{
						ow = originalImage.Width;
						oh = originalImage.Width * height / towidth;
						x = 0;
						y = (originalImage.Height - oh) / 2;
					}
					break;
				default:
					break;
			}

			//新建一个bmp图片
			System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

			//新建一个画板
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

			//设置高质量插值法
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			//设置高质量,低速度呈现平滑程度
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//清空画布并以透明背景色填充
			g.Clear(System.Drawing.Color.Transparent);

			//在指定位置并且按指定大小绘制原图片的指定部分
			g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
			new System.Drawing.Rectangle(x, y, ow, oh),
			System.Drawing.GraphicsUnit.Pixel);

			try
			{
				string mine = System.IO.Path.GetExtension(thumbnailPath);
				switch (mine.ToLower())
				{
					case ".gif":
						bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif);

						break;
					case ".Png":
						bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

						break;

					case ".png":
						bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

						break;
					case ".jpg":
						bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);

						break;

					case ".bmp":
						bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp);

						break;

					case ".icon":
						bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Icon);

						break;
				}

				//以jpg格式保存缩略图
			}
			catch (System.Exception e)
			{
				throw e;
			}
			finally
			{
				originalImage.Dispose();
				bitmap.Dispose();
				g.Dispose();
			}
		}

		/// <summary>
		/// 生成缩略图
		/// </summary>
		/// <param name="originalImagePath">源图路径（物理路径）</param>
		/// <param name="thumbnailPath">缩略图路径（物理路径）</param>
		/// <param name="width">缩略图宽度</param>
		/// <param name="height">缩略图高度</param>
		/// <param name="mode">生成缩略图的方式</param>
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
		{
			System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

			int towidth = width;
			int toheight = height;

			int x = 0;
			int y = 0;
			int ow = originalImage.Width;
			int oh = originalImage.Height;

			switch (mode)
			{
				case "HW"://指定高宽缩放（可能变形）
					break;
				case "W"://指定宽，高按比例
					toheight = originalImage.Height * width / originalImage.Width;
					break;
				case "H"://指定高，宽按比例
					towidth = originalImage.Width * height / originalImage.Height;
					break;
				case "Cut"://指定高宽裁减（不变形）
					if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
					{
						oh = originalImage.Height;
						ow = originalImage.Height * towidth / toheight;
						y = 0;
						x = (originalImage.Width - ow) / 2;
					}
					else
					{
						ow = originalImage.Width;
						oh = originalImage.Width * height / towidth;
						x = 0;
						y = (originalImage.Height - oh) / 2;
					}
					break;
				default:
					break;
			}

			//新建一个bmp图片
			System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

			//新建一个画板
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

			//设置高质量插值法
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

			//设置高质量,低速度呈现平滑程度
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//清空画布并以透明背景色填充
			g.Clear(System.Drawing.Color.Transparent);

			//在指定位置并且按指定大小绘制原图片的指定部分
			g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
			new System.Drawing.Rectangle(x, y, ow, oh),
			System.Drawing.GraphicsUnit.Pixel);

			try
			{
				//以jpg格式保存缩略图
				bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);
			}
			catch (System.Exception e)
			{
				throw e;
			}
			finally
			{
				originalImage.Dispose();
				bitmap.Dispose();
				g.Dispose();
			}
		}

		/// <summary>
		/// 在图片上增加文字水印
		/// </summary>
		/// <param name="Path">原服务器图片路径</param>
		/// <param name="Path_sy">生成的带文字水印的图片路径</param>
		public static void AddShuiYinWord(string Path, string Path_sy)
		{
			string addText = "爱智旮旯";
			System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
			g.DrawImage(image, 0, 0, image.Width, image.Height);
			System.Drawing.Font f = new System.Drawing.Font("Verdana", 16);
			System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);

			g.DrawString(addText, f, b, 15, 15);
			g.Dispose();

			image.Save(Path_sy);
			image.Dispose();
		}

		/// <summary>
		/// 在图片上生成图片水印
		/// </summary>
		/// <param name="Path">原服务器图片路径</param>
		/// <param name="Path_syp">生成的带图片水印的图片路径</param>
		/// <param name="Path_sypf">水印图片路径</param>
		public static void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
		{
			System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
			System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
			g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
			g.Dispose();

			image.Save(Path_syp);
			image.Dispose();
		}

		/// <summary>
		/// 获取一个图片按等比例缩小后的大小。
		/// </summary>
		/// <param name="maxWidth">需要缩小到的宽度</param>
		/// <param name="maxHeight">需要缩小到的高度</param>
		/// <param name="imageOriginalWidth">图片的原始宽度</param>
		/// <param name="imageOriginalHeight">图片的原始高度</param>
		/// <returns>返回图片按等比例缩小后的实际大小</returns>
		public static Size GetNewSize(int maxWidth, int maxHeight, int imageOriginalWidth, int imageOriginalHeight)
		{
			double w = 0.0;
			double h = 0.0;
			double sw = Convert.ToDouble(imageOriginalWidth);
			double sh = Convert.ToDouble(imageOriginalHeight);
			double mw = Convert.ToDouble(maxWidth);
			double mh = Convert.ToDouble(maxHeight);

			if (sw < mw && sh < mh)
			{
				w = sw;
				h = sh;
			}
			else if ((sw / sh) > (mw / mh))
			{
				w = maxWidth;
				h = (w * sh) / sw;
			}
			else
			{
				h = maxHeight;
				w = (h * sw) / sh;
			}

			return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
		}

		/// <summary>
		/// 对给定的一个图片（Image对象）生成一个指定大小的缩略图。
		/// </summary>
		/// <param name="originalImage">原始图片</param>
		/// <param name="thumMaxWidth">缩略图的宽度</param>
		/// <param name="thumMaxHeight">缩略图的高度</param>
		/// <returns>返回缩略图的Image对象</returns>
		public static System.Drawing.Image GetThumbNailImage(System.Drawing.Image originalImage, int thumMaxWidth, int thumMaxHeight)
		{
			Size thumRealSize = Size.Empty;
			System.Drawing.Image newImage = originalImage;
			Graphics graphics = null;

			try
			{
				thumRealSize = GetNewSize(thumMaxWidth, thumMaxHeight, originalImage.Width, originalImage.Height);
				newImage = new Bitmap(thumRealSize.Width, thumRealSize.Height);
				graphics = Graphics.FromImage(newImage);

				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;

				graphics.Clear(Color.Transparent);

				graphics.DrawImage(originalImage, new Rectangle(0, 0, thumRealSize.Width, thumRealSize.Height), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
			}
			catch { }
			finally
			{
				if (graphics != null)
				{
					graphics.Dispose();
					graphics = null;
				}
			}

			return newImage;
		}

		/// <summary>
		/// 对给定的一个图片文件生成一个指定大小的缩略图。
		/// </summary>
		/// <param name="originalImage">图片的物理文件地址</param>
		/// <param name="thumMaxWidth">缩略图的宽度</param>
		/// <param name="thumMaxHeight">缩略图的高度</param>
		/// <returns>返回缩略图的Image对象</returns>
		public static System.Drawing.Image GetThumbNailImage(string imageFile, int thumMaxWidth, int thumMaxHeight)
		{
			System.Drawing.Image originalImage = null;
			System.Drawing.Image newImage = null;

			try
			{
				originalImage = System.Drawing.Image.FromFile(imageFile);
				newImage = GetThumbNailImage(originalImage, thumMaxWidth, thumMaxHeight);
			}
			catch { }
			finally
			{
				if (originalImage != null)
				{
					originalImage.Dispose();
					originalImage = null;
				}
			}

			return newImage;
		}

		/// <summary>
		/// 对给定的一个图片文件生成一个指定大小的缩略图，并将缩略图保存到指定位置。
		/// </summary>
		/// <param name="originalImageFile">图片的物理文件地址</param>
		/// <param name="thumbNailImageFile">缩略图的物理文件地址</param>
		/// <param name="thumMaxWidth">缩略图的宽度</param>
		/// <param name="thumMaxHeight">缩略图的高度</param>
		public static void MakeThumbNail(string originalImageFile, string thumbNailImageFile, int thumMaxWidth, int thumMaxHeight)
		{
			System.Drawing.Image newImage = GetThumbNailImage(originalImageFile, thumMaxWidth, thumMaxHeight);
			try
			{
				newImage.Save(thumbNailImageFile, ImageFormat.Png);
			}
			catch
			{ }
			finally
			{
				newImage.Dispose();
				newImage = null;
			}
		}

		/// <summary>
		/// 将一个图片的内存流调整为指定大小，并返回调整后的内存流。
		/// </summary>
		/// <param name="originalImageStream">原始图片的内存流</param>
		/// <param name="newWidth">新图片的宽度</param>
		/// <param name="newHeight">新图片的高度</param>
		/// <returns>返回调整后的图片的内存流</returns>
		public static MemoryStream ResizeImage(Stream originalImageStream, int newWidth, int newHeight)
		{
			MemoryStream newImageStream = null;

			System.Drawing.Image newImage = GetThumbNailImage(System.Drawing.Image.FromStream(originalImageStream), newWidth, newHeight);
			if (newImage != null)
			{
				newImageStream = new MemoryStream();
				newImage.Save(newImageStream, ImageFormat.Png);
			}

			return newImageStream;
		}

		/// <summary>
		/// 将一个内存流保存为磁盘文件。
		/// </summary>
		/// <param name="stream">内存流</param>
		/// <param name="newFile">目标磁盘文件地址</param>
		public static void SaveStreamToFile(Stream stream, string newFile)
		{
			if (stream == null || stream.Length == 0 || string.IsNullOrEmpty(newFile))
			{
				return;
			}

			byte[] buffer = new byte[stream.Length];
			stream.Position = 0;
			stream.Read(buffer, 0, buffer.Length);
			FileStream fileStream = new FileStream(newFile, FileMode.OpenOrCreate, FileAccess.Write);
			fileStream.Write(buffer, 0, buffer.Length);
			fileStream.Flush();
			fileStream.Close();
			fileStream.Dispose();
		}

		/// <summary>
		/// 对一个指定的图片加上图片水印效果。
		/// </summary>
		/// <param name="imageFile">图片文件地址</param>
		/// <param name="waterImage">水印图片（Image对象）</param>
		public static void CreateImageWaterMark(string imageFile, System.Drawing.Image waterImage)
		{
			if (string.IsNullOrEmpty(imageFile) || !File.Exists(imageFile) || waterImage == null)
			{
				return;
			}

			System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imageFile);

			if (originalImage.Width - 10 < waterImage.Width || originalImage.Height - 10 < waterImage.Height)
			{
				return;
			}

			Graphics graphics = Graphics.FromImage(originalImage);

			int x = originalImage.Width - waterImage.Width - 10;
			int y = originalImage.Height - waterImage.Height - 10;
			int width = waterImage.Width;
			int height = waterImage.Height;

			graphics.DrawImage(waterImage, new Rectangle(x, y, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
			graphics.Dispose();

			MemoryStream stream = new MemoryStream();
			originalImage.Save(stream, ImageFormat.Png);
			originalImage.Dispose();

			System.Drawing.Image imageWithWater = System.Drawing.Image.FromStream(stream);

			imageWithWater.Save(imageFile);
			imageWithWater.Dispose();
		}

		/// <summary>
		/// 对一个指定的图片加上文字水印效果。
		/// </summary>
		/// <param name="imageFile">图片文件地址</param>
		/// <param name="waterText">水印文字内容</param>
		public static void CreateTextWaterMark(string imageFile, string waterText)
		{
			if (string.IsNullOrEmpty(imageFile) || string.IsNullOrEmpty(waterText) || !File.Exists(imageFile))
			{
				return;
			}

			System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imageFile);

			Graphics graphics = Graphics.FromImage(originalImage);

			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

			SolidBrush brush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
			Font waterTextFont = new Font("Arial", 16, FontStyle.Regular);
			SizeF waterTextSize = graphics.MeasureString(waterText, waterTextFont);

			float x = (float)originalImage.Width - waterTextSize.Width - 10F;
			float y = (float)originalImage.Height - waterTextSize.Height - 10F;

			graphics.DrawString(waterText, waterTextFont, brush, x, y);

			graphics.Dispose();
			brush.Dispose();

			MemoryStream stream = new MemoryStream();
			originalImage.Save(stream, ImageFormat.Png);
			originalImage.Dispose();

			System.Drawing.Image imageWithWater = System.Drawing.Image.FromStream(stream);

			imageWithWater.Save(imageFile);
			imageWithWater.Dispose();
		}

		/// <summary>
		/// 判断上传组件是否包含内容。
		/// </summary>
		/// <param name="fileUpload">ASP.NET 2.0标准上传组件</param>
		/// <returns>如果数据有效，则返回True，否则返回False</returns>
		public static bool IsAttachmentValid(System.Web.UI.WebControls.FileUpload fileUpload)
		{
			if (fileUpload != null &&
				fileUpload.PostedFile != null &&
				!string.IsNullOrEmpty(fileUpload.PostedFile.FileName) &&
				fileUpload.PostedFile.ContentLength > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 图片缩略
		/// </summary>
		/// <param name="SourceImage"></param>
		/// <param name="TargetWidth"></param>
		/// <param name="TargetHeight"></param>
		/// <returns></returns>
		///

		public static Image ZoomPicture(Image SourceImage, int TargetWidth, int TargetHeight)
		{
			int IntWidth; //新的图片宽
			int IntHeight; //新的图片高
			try
			{
				System.Drawing.Imaging.ImageFormat format = SourceImage.RawFormat;
				System.Drawing.Bitmap SaveImage = new System.Drawing.Bitmap(TargetWidth, TargetHeight);
				Graphics g = Graphics.FromImage(SaveImage);

				Color c = Color.FromArgb(000, 244, 23, 45);
				g.Clear(c);

				//计算缩放图片的大小

				if (SourceImage.Width > TargetWidth && SourceImage.Height <= TargetHeight)//宽度比目的图片宽度大，长度比目的图片长度小
				{
					IntWidth = TargetWidth;
					IntHeight = (IntWidth * SourceImage.Height) / SourceImage.Width;
				}
				else if (SourceImage.Width <= TargetWidth && SourceImage.Height > TargetHeight)//宽度比目的图片宽度小，长度比目的图片长度大
				{
					IntHeight = TargetHeight;
					IntWidth = (IntHeight * SourceImage.Width) / SourceImage.Height;
				}
				else if (SourceImage.Width <= TargetWidth && SourceImage.Height <= TargetHeight) //长宽比目的图片长宽都小
				{
					IntHeight = SourceImage.Width;
					IntWidth = SourceImage.Height;
				}
				else//长宽比目的图片的长宽都大
				{
					IntWidth = TargetWidth;
					IntHeight = (IntWidth * SourceImage.Height) / SourceImage.Width;
					if (IntHeight > TargetHeight)//重新计算
					{
						IntHeight = TargetHeight;
						IntWidth = (IntHeight * SourceImage.Width) / SourceImage.Height;
					}
				}

				g.DrawImage(SourceImage, (TargetWidth - IntWidth) / 2, (TargetHeight - IntHeight) / 2, IntWidth, IntHeight);
				SourceImage.Dispose();

				return SaveImage;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}