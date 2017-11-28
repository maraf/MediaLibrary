using Neptuo.PresentationModels;
using Neptuo.PresentationModels.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Neptuo.PresentationModels.TypeModels.Expressions;
using Neptuo.PresentationModels.UI;

namespace MediaLibrary.Views.Controls
{
    public class FieldTextBlock : TextBlock, IFieldValueProvider
    {
        public bool TryGetValue(out object value)
        {
            value = Text;
            return true;
        }

        public bool TrySetValue(object value)
        {
            Text = (string)value;
            return true;
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            FieldValueProviderCollection providers = VisualTree.FindFieldValueProviderCollection(this);
            if (providers != null)
                VisualTree.WithFieldDefinitionContainer(this, definition => providers.Add(definition.Identifier, this));
        }
    }
}
