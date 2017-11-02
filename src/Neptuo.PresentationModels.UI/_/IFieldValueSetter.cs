using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A field value setter.
    /// </summary>
    public interface IFieldValueSetter
    {
        /// <summary>
        /// Tries to set a new value to the field.
        /// </summary>
        /// <param name="value">A new value to the field.</param>
        /// <returns><c>true</c> if value was set; <c>false</c> otherwise.</returns>
        bool TrySetValue(object value);
    }
}
