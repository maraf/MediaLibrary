using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A field value getter.
    /// </summary>
    public interface IFieldValueGetter
    {
        /// <summary>
        /// Tries to get current value of the field.
        /// </summary>
        /// <param name="value">A current field value.</param>
        /// <returns><c>true</c> if value was provided; <c>false</c> otherwise.</returns>
        bool TryGetValue(out object value);
    }
}
