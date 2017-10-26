using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// An implementation of <see cref="IWpfRenderContext"/> which uses <see cref="Panel"/> to support multiple childrens.
    /// </summary>
    public class WpfPanelRenderContext : IWpfRenderContext
    {
        private readonly Panel parent;

        /// <summary>
        /// Creates a new instance for <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">A panel to add controls to.</param>
        public WpfPanelRenderContext(Panel parent)
        {
            Ensure.NotNull(parent, "parent");
            this.parent = parent;
        }

        public void Add(object control)
        {
            parent.Children.Add(control);
        }
    }
}
