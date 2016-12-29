using System.Drawing;

namespace NModel
{
	public class EnFont
	{
		public Font MyFont { get; set; }
		public Color MyColor { get; set; }

		public Point MyPoint { get; set; }
		public PointF MyPointF { get; set; }

		public EnFont(Font MyFont, Color MyColor, Point MyPoint)
		{
			this.MyFont = MyFont;
			this.MyPoint = MyPoint;
			this.MyColor = MyColor;
		}

		public EnFont() { }

		public EnFont(Font MyFont, Color MyColor, PointF MyPointF)
		{
			this.MyFont = MyFont;
			this.MyPointF = MyPointF;
			this.MyColor = MyColor;
		}
	}
}