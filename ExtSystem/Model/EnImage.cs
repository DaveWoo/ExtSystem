using System.Drawing;

namespace NModel
{
	public class EnImage
	{
		public int Width { get; set; }
		public int Height { get; set; }

		public Color ImgColor { get; set; }

		public Font ImgFont { get; set; }

		public Point ImgPoint { get; set; }

		public EnImage()
		{
		}

		public EnImage(int width, int height)
		{
			this.Width = width;
			this.Height = height;
		}

		public EnImage(int width, int height, Color imgColor)
		{
			this.Width = width;
			this.Height = height;
			this.ImgColor = imgColor;
		}

		public EnImage(int width, int height, Color imgColor, Font imgFont, Point imgPoint)
		{
			this.Width = width;
			this.Height = height;
			this.ImgColor = imgColor;
			this.ImgFont = imgFont;
			this.ImgPoint = imgPoint;
		}

		/// <summary>
		/// 绘制形状
		/// </summary>
		public enum PaintShape { All, Link, Dot, Ring }
	}
}