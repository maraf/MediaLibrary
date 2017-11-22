using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// An implementation of <see cref="IRenderContext"/> which uses <see cref="Panel"/> to support multiple childrens.
    /// </summary>
    public class PanelRenderContext : IRenderContext
    {
        private readonly Panel parent;

        /// <summary>
        /// An event raised when new element is added to the container.
        /// </summary>
        public event Action<UIElement> Added;

        /// <summary>
        /// Creates a new instance for <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">A panel to add controls to.</param>
        public PanelRenderContext(Panel parent)
        {
            Ensure.NotNull(parent, "parent");
            this.parent = parent;
        }

        public void Add(object control)
        {
            UIElement element = (UIElement)control;
            parent.Children.Add(element);

            Added?.Invoke(element);
        }
    }
}
