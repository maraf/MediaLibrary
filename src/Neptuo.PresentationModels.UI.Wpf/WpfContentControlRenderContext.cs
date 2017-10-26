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
    /// An implementation of <see cref="IWpfRenderContext"/> which uses <see cref="ContentControl"/> to support single children.
    /// </summary>
    public class WpfContentControlRenderContext : IWpfRenderContext
    {
        private readonly ContentControl parent;

        /// <summary>
        /// Creates a new instance for <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent">A content control to add single control to.</param>
        public WpfContentControlRenderContext(ContentControl parent)
        {
            Ensure.NotNull(parent, "parent");
            this.parent = parent;
        }

        public void Add(object control)
        {
            parent.Content = control;
        }
    }
}
