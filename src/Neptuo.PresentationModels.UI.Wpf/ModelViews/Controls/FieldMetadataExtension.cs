using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Neptuo.PresentationModels.UI.ModelViews.Controls
{
    /// <summary>
    /// An extension for reading a field's metadata.
    /// </summary>
    public class FieldMetadataExtension : MarkupExtension
    {
        /// <summary>
        /// Gets or sets a metadata key to read.
        /// </summary>
        public string Key { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null)
                return null;

            IProvideValueTarget provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (provideValueTarget.TargetObject is FrameworkElement element)
                return GetKeyValue<object>(element, Key);

            return this;
        }

        private static T GetKeyValue<T>(FrameworkElement element, string key)
        {
            IFieldDefinitionContainer container = VisualTree.FindAncestorOfType<IFieldDefinitionContainer>(element)
                ?? throw Ensure.Exception.InvalidOperation("Missing field container.");

            return container.Definition.Metadata.Get(key, default(T));
        }
    }
}
