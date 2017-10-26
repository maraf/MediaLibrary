using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// A rendering context.
    /// </summary>
    public interface IWpfRenderContext
    {
        /// <summary>
        /// Adds a <paramref name="control"/> to actual parent.
        /// </summary>
        /// <param name="control">A control to add.</param>
        void Add(object control);
    }
}
