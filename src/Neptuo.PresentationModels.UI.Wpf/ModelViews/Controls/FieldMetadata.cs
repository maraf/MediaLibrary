using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews.Controls
{
    /// <summary>
    /// Attached properties to manipulate field's data.
    /// </summary>
    public static class FieldMetadata
    {
        public static string GetKey(DependencyObject obj)
        {
            return (string)obj.GetValue(KeyProperty);
        }

        public static void SetKey(DependencyObject obj, string value)
        {
            obj.SetValue(KeyProperty, value);
        }

        /// <summary>
        /// A property that tries to fill a target with value of metadata associated to a key.
        /// </summary>
        public static readonly DependencyProperty KeyProperty = DependencyProperty.RegisterAttached(
            "Key",
            typeof(string),
            typeof(FieldMetadata),
            new PropertyMetadata(null, OnKeyChanged)
        );

        private static void OnKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            string key = GetKey(d);

            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(element, true))
            {
                FieldDefinitionContainer container = UserFieldPresenter.GetContainer(ancestor);
                if (container != null)
                {
                    if (container.Definition != null)
                        SetValue(element, container.Definition, key);

                    container.Changed += () => SetValue(element, container.Definition, key);
                    break;
                }
            }
        }

        private static void SetValue(FrameworkElement element, IFieldDefinition definition, string key)
        {
            if (element is ContentControl control)
                control.Content = definition.Metadata.Get(key, default(object));
            else if (element is TextBlock textBlock)
                textBlock.Text = definition.Metadata.Get(key, String.Empty);
            else
                throw Ensure.Exception.InvalidOperation($"Not supported target for metadata key '{key}'. Currently only supported are '{nameof(ContentControl)}' and '{(nameof(textBlock))}'");
        }
    }
}
