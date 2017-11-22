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

            if (element is ContentControl control)
                control.Content = GetKeyValue<object>(element, key);
            else if (element is TextBlock textBlock)
                textBlock.Text = GetKeyValue<string>(element, key);
            else
                throw Ensure.Exception.InvalidOperation($"Not supported target for metadata key '{key}'. Currently only supported are '{nameof(ContentControl)}' and '{(nameof(textBlock))}'");
        }

        internal static T GetKeyValue<T>(FrameworkElement element, string key)
        {
            IFieldDefinitionContainer container = VisualTree.FindAncestorOfType<IFieldDefinitionContainer>(element) 
                ?? throw Ensure.Exception.InvalidOperation("Missing field container.");

            return container.Definition.Metadata.Get(key, default(T));
        }
    }
}
