using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using VectorToXamlConvertor.Convertor.Attributes;

namespace VectorToXamlConvertor.Convertor.Elements
{
	class PathElement : SvgElementBase<Path, Shape>
	{
		public PathElement(XElement element)
			: base(element)
		{

		}

		public override Path GetBaseObject(XElement element)
		{
			var pathObject = new Path();
			var pathGeometryCollection = element.Elements("PathGeometry");
			var result = pathObject;

			foreach (var xElement in pathGeometryCollection)
			{
				var attribute = xElement.Attribute("Figures");

				var figuresAttribute = new FiguresAttribute(attribute.Value);
				result = figuresAttribute.TryApplyAttribute<Path>(result);
			}

			var style = element.Attribute("style");
			if (!string.IsNullOrEmpty(style?.Value))
			{
				var styleValue = style.Value;
				var rgbString = styleValue.Split(':')[1].TrimEnd(';');
				if (rgbString.Contains("none") ||
					!rgbString.Contains("rgb") ||
					!rgbString.Contains("(") ||
					!rgbString.Contains(")"))
					return result;

				var color = Colors.Transparent;

				try
				{
					var startIndex = rgbString.IndexOf('(') + 1;
					var length = rgbString.IndexOf(')') - startIndex;

					rgbString = rgbString.Substring(startIndex, length);
					var items = rgbString.Split(',').Select(k => int.Parse(k)).ToArray();
					if (items.Length == 3)
						color = Color.FromArgb(255, (byte)items[0], (byte)items[1], (byte)items[2]);
					else
						color = Color.FromArgb((byte)items[0], (byte)items[1], (byte)items[2], (byte)items[3]);

					if (color != Colors.Transparent)
						result.Fill = new SolidColorBrush(color);
				}
				catch (Exception ex)
				{
					
				}
			}

			return result;
		}

		protected override ICollection<Path> ConvertObjectToPaths(Path objectToConvert)
		{
			return new List<Path> {objectToConvert};
		}
	}
}