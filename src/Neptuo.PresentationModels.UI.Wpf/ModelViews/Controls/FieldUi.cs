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
    public static class FieldUi
    {
        public static string GetMetadataKey(DependencyObject obj)
        {
            return (string)obj.GetValue(MetadataKeyProperty);
        }

        public static void SetMetadataKey(DependencyObject obj, string value)
        {
            obj.SetValue(MetadataKeyProperty, value);
        }

        public static readonly DependencyProperty MetadataKeyProperty = DependencyProperty.RegisterAttached(
            "MetadataKey",
            typeof(string),
            typeof(FieldUi),
            new PropertyMetadata(null, OnMetadataKeyChanged)
        );

        private static void OnMetadataKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            IFieldDefinitionContainer container = VisualTree.FindAncestorOfType<IFieldDefinitionContainer>(element) ?? throw Ensure.Exception.InvalidOperation("Missing field container.");
            string metadataKey = GetMetadataKey(d);

            if (element is ContentControl control)
                control.Content = container.FieldDefinition.Metadata.Get(metadataKey, (object)null);
            else if (element is TextBlock textBlock)
                textBlock.Text = container.FieldDefinition.Metadata.Get(metadataKey, (string)null);
            else
                throw Ensure.Exception.InvalidOperation($"Not supported target for metadata key '{metadataKey}'. Currently only supported are '{nameof(ContentControl)}' and '{(nameof(textBlock))}'");
        }
    }
}
