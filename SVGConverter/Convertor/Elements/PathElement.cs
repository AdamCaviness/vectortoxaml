using SVGConverter.Services;
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

			var fill = SvgElementService.GetBrushFromSvgStyleAttribute(element.Attribute("style"));
			if (fill == null)
				fill = new SolidColorBrush(Colors.Black);

			if (fill != Brushes.Transparent)
				result.Fill = fill;

			return result;
		}

		protected override ICollection<Path> ConvertObjectToPaths(Path objectToConvert)
		{
			return new List<Path> {objectToConvert};
		}
	}
}