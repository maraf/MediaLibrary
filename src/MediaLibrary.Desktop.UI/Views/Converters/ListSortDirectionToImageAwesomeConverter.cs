using FontAwesome.WPF;
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
    public class ListSortDirectionToImageAwesomeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ListSortDirection direction)
            {
                switch (direction)
                {
                    case ListSortDirection.Ascending:
                        return FontAwesomeIcon.AngleDown;
                    case ListSortDirection.Descending:
                        return FontAwesomeIcon.AngleUp;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
