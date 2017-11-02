using System;
using System.Collections.Generic;
using System.Linq;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Single field view.
    /// </summary>
    /// <typeparam name="T">Type of rendering context.</typeparam>
    public interface IFieldView<T> : IFieldValueProvider
    {
        /// <summary>
        /// Renders the view to the <paramref name="context"/>.
        /// </summary>
        /// <param name="context">Rendering context.</param>
        void Render(T context);
    }
}