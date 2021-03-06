﻿using Neptuo.PresentationModels.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.PresentationModels;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class ComboBoxFieldEditor<T> : FieldView<T, IRenderContext>
    {
        private readonly IEnumerable<T> items;
        private ComboBox control;

        public ComboBoxFieldEditor(IFieldDefinition fieldDefinition, IEnumerable<T> items)
            : base(fieldDefinition)
        {
            Ensure.NotNull(items, "items");
            this.items = items;
        }

        protected override void RenderInternal(IRenderContext context, T defaultValue)
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

            if (typeof(T) == typeof(string))
            {
                string stringValue = control.Text;
                if (String.IsNullOrEmpty(stringValue) || String.IsNullOrWhiteSpace(stringValue))
                    stringValue = null;

                value = (T)(object)stringValue;
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
