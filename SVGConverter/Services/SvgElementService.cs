using System;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;

namespace SVGConverter.Services
{
	public static class SvgElementService
	{
		public static SolidColorBrush GetBrushFromSvgStyleAttribute(XAttribute styleAttribute)
		{
			if (string.IsNullOrEmpty(styleAttribute?.Value))
				return null;

			var styleValue = styleAttribute.Value;
			var rgbString = styleValue.Split(':')[1].TrimEnd(';');
			if (rgbString.Contains("none") ||
				!rgbString.Contains("rgb") ||
				!rgbString.Contains("(") ||
				!rgbString.Contains(")"))
				return null;

			var color = Colors.Transparent;
			var startIndex = rgbString.IndexOf('(') + 1;
			var length = rgbString.IndexOf(')') - startIndex;

			rgbString = rgbString.Substring(startIndex, length);
			var items = rgbString.Split(',').Select(k => int.Parse(k)).ToArray();
			if (items.Length == 3)
				color = Color.FromArgb(255, (byte)items[0], (byte)items[1], (byte)items[2]);
			else
				color = Color.FromArgb((byte)items[0], (byte)items[1], (byte)items[2], (byte)items[3]);

			return new SolidColorBrush(color);
		}
	}
}