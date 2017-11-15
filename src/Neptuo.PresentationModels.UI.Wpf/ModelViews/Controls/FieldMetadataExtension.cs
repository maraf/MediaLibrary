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
                return FieldMetadata.GetKeyValue<object>(element, Key);

            return this;
        }
    }
}
