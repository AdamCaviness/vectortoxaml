using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VectorToXamlConvertor.ValueConverters
{
	[ValueConversion(typeof(bool), typeof(Visibility))]
	public class BooleanVisibilityConverter : MarkupConverter
	{
		public BooleanVisibilityConverter()
		{
			FalseVisibility = Visibility.Collapsed;
		}

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = Negated ^ System.Convert.ToBoolean(value); //Negate the value when negateValue is true using XOR
			return val ? Visibility.Visible : FalseVisibility;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((Visibility)value == Visibility.Visible)
				return true ^ Negated;
			else
				return false ^ Negated;
		}

		public bool Negated { get; set; }
		public Visibility FalseVisibility { get; set; }
	}
}