using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MediaLibrary.Views.Converters
{
    public class BoolConverter : IValueConverter
    {
        [DefaultValue(true)]
        public bool Test { get; set; } = true;
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = ValueAsBool(value);
            object result = null;
            if (Test == boolValue)
                result = TrueValue;
            else
                result = FalseValue;

            if (targetType != null && result != null)
            {
                Type resultType = result.GetType();
                TypeConverter converter = TypeDescriptor.GetConverter(targetType);
                if (converter != null && converter.CanConvertFrom(resultType))
                    result = converter.ConvertFrom(null, CultureInfo.InvariantCulture, result);
            }

            return result;
        }

        protected virtual bool ValueAsBool(object value)
        {
            bool? boolValue = value as bool?;
            if (boolValue == null)
                boolValue = false;

            return boolValue.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
