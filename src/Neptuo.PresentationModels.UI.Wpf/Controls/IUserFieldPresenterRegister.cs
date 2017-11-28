using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Controls
{
    /// <summary>
    /// A that want's to be notified when a <see cref="IFieldValueProvider"/> is attached to the tree of objects.
    /// </summary>
    public interface IUserFieldPresenterRegister
    {
        /// <summary>
        /// Sets <paramref name="provider"/> to be a value provider of the field <paramref name="identifier"/>.
        /// </summary>
        /// <param name="identifier">A field identifier.</param>
        /// <param name="provider">A field value provider.</param>
        void Add(string identifier, IFieldValueProvider provider);
    }
}
