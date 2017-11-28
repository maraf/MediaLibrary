using Neptuo.PresentationModels;
using Neptuo.PresentationModels.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

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

            IUserFieldPresenterRegister register = VisualTree.FindAncestorOfType<IUserFieldPresenterRegister>(this);
            FieldDefinitionContainer container = VisualTree.EnumerateAncestors(this, true).Select(a => UserFieldPresenter.GetContainer(a)).FirstOrDefault(c => c != null);
            if (register != null && container != null)
            {
                if (container.Definition != null)
                {
                    register.Add(container.Definition.Identifier, this);
                }
                else
                {
                    container.Changed += () =>
                    {
                        if (container.Definition != null)
                            register.Add(container.Definition.Identifier, this);
                    };
                }
            }
        }
    }
}
