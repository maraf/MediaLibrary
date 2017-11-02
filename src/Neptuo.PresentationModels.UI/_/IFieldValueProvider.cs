using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// A field value provider (can set and get field value).
    /// </summary>
    public interface IFieldValueProvider : IFieldValueGetter, IFieldValueSetter
    {
        
    }
}
