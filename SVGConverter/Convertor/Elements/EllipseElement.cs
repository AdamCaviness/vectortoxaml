using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using SVGConverter.Convertor.Elements;
using SVGConverter.Services;

namespace VectorToXamlConvertor.Convertor.Elements
{
	class EllipseElement : SvgGeometryElement<SvgGenericShapeAdaptor, Shape>
	{
		public EllipseElement(XElement element) : base(element)
		{
		}

		public override SvgGenericShapeAdaptor GetBaseObject(XElement element)
		{
			var shapeAdaptor = new SvgGenericShapeAdaptor();
			var fill = SvgElementService.GetBrushFromSvgStyleAttribute(element.Attribute("style"));
			if (fill != null && fill != Brushes.Transparent)
				shapeAdaptor.Fill = fill;

			return shapeAdaptor;
		}
		
		protected override Geometry GetGeometry(SvgGenericShapeAdaptor objectToConvert)
		{
			var geometry = new EllipseGeometry(new Point(objectToConvert.X, objectToConvert.Y),
				objectToConvert.RadiusX, objectToConvert.RadiusY);

			return geometry;
		}
	}
}
