using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Views.Converters
{
    public class StringNullOrEmptyConverter : BoolConverter
    {
        protected override bool ValueAsBool(object value)
        {
            string stringValue = value as string;
            return String.IsNullOrEmpty(stringValue);
        }
    }
}
