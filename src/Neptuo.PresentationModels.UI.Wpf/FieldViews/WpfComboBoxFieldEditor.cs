using Neptuo.PresentationModels.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.PresentationModels;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class WpfComboBoxFieldEditor<T> : FieldView<T, IWpfRenderContext>
    {
        private readonly IEnumerable<T> items;
        private ComboBox control;

        public WpfComboBoxFieldEditor(IFieldDefinition fieldDefinition, IEnumerable<T> items)
            : base(fieldDefinition)
        {
            Ensure.NotNull(items, "items");
            this.items = items;
        }

        protected override void RenderInternal(IWpfRenderContext context, T defaultValue)
        {
            control = new ComboBox();
            control.IsEditable = true;
            control.SelectedItem = defaultValue;

            foreach (T item in items)
                control.Items.Add(item);

            context.Add(control);
        }

        protected override bool TryGetValueInternal(out T value)
        {
            if (control.SelectedItem is T item)
            {
                value = item;
                return true;
            }

            if (typeof(T) == typeof(string) && !String.IsNullOrEmpty(control.Text))
            {
                value = (T)(object)control.Text;
                return true;
            }

            value = default(T);
            return false;
        }

        protected override bool TrySetValueInternal(T value)
        {
            control.SelectedItem = value;
            return true;
        }
    }
}
