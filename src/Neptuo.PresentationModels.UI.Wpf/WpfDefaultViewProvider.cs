using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// A global model and field view provider.
    /// </summary>
    public static class WpfDefaultViewProvider
    {
        private static IModelViewProvider<IWpfRenderContext> modelViewProvider;
        private static IFieldViewProvider<IWpfRenderContext> fieldViewProvider;

        /// <summary>
        /// Gets a default model view provider or throw <see cref="InvalidOperationException"/> if not initialized.
        /// </summary>
        public static IModelViewProvider<IWpfRenderContext> ModelViewProvider => modelViewProvider ?? throw Ensure.Exception.InvalidOperation("Missing default model view provider.");

        /// <summary>
        /// Gets a default field view provider or throw <see cref="InvalidOperationException"/> if not initialized.
        /// </summary>
        public static IFieldViewProvider<IWpfRenderContext> FieldViewProvider => fieldViewProvider ?? throw Ensure.Exception.InvalidOperation("Missing default field view provider.");

        /// <summary>
        /// Sets a default providers to <paramref name="modelViewProvider"/> and <paramref name="fieldViewProvider"/>.
        /// </summary>
        /// <param name="modelViewProvider">A new default model view provider.</param>
        /// <param name="fieldViewProvider">A new default field view provider.</param>
        public static void Set(IModelViewProvider<IWpfRenderContext> modelViewProvider, IFieldViewProvider<IWpfRenderContext> fieldViewProvider)
        {
            Ensure.NotNull(modelViewProvider, "modelViewProvider");
            Ensure.NotNull(fieldViewProvider, "fieldViewProvider");
            WpfDefaultViewProvider.modelViewProvider = modelViewProvider;
            WpfDefaultViewProvider.fieldViewProvider = fieldViewProvider;
        }
    }
}
