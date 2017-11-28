using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Neptuo.PresentationModels.UI.Controls
{
    /// <summary>
    /// An extension for reading a field's metadata.
    /// This extension can be used on dependency properties and required <see cref="UserFieldPresenter.ContainerProperty"/> to be set on ancestor.
    /// </summary>
    public class UserFieldMetadataExtension : MarkupExtension
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
            if (provideValueTarget.TargetObject is FrameworkElement element && provideValueTarget.TargetProperty is DependencyProperty property)
                return FindOrBindKeyValue(element, property);

            // Template controls magic...
            return this;
        }

        private object FindOrBindKeyValue(FrameworkElement element, DependencyProperty property)
        {
            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(element, true))
            {
                FieldDefinitionContainer container = UserFieldPresenter.GetContainer(ancestor);
                if (container != null)
                {
                    if (container.Definition != null && Key != null)
                        return container.Definition.Metadata.Get(Key, default(object));

                    container.Changed += () =>
                    {
                        if (Key != null)
                        {
                            object value = container.Definition.Metadata.Get(Key, default(object));
                            element.SetValue(property, value);
                        }
                    };
                    break;
                }
            }

            return null;
        }
    }
}
