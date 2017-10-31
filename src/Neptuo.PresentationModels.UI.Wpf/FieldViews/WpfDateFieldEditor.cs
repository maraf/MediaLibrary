using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class WpfDateFieldEditor : FieldView<DateTime?, IWpfRenderContext>
    {
        private TextBox textBox;

        public WpfDateFieldEditor(IFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        protected override void RenderInternal(IWpfRenderContext context, DateTime? defaultValue)
        {
            textBox = new TextBox();
            TrySetValueInternal(defaultValue);
            context.Add(textBox);

            if (FieldDefinition.Metadata.TryGet("IsReadOnly", out bool isReadOnly))
                textBox.IsReadOnly = isReadOnly;

            if (FieldDefinition.Metadata.TryGet("IsAutoFocus", out bool isAutoFocus) && isAutoFocus)
                textBox.Loaded += (sender, e) => textBox.Focus();
        }

        protected override bool TryGetValueInternal(out DateTime? value)
        {
            if (DateTime.TryParse(textBox.Text, out DateTime rawValue))
            {
                value = rawValue;
                return true;
            }

            if (FieldDefinition.FieldType == typeof(DateTime?))
            {
                value = null;
                return true;
            }

            value = DateTime.MinValue;
            return false;
        }

        protected override bool TrySetValueInternal(DateTime? value)
        {
            if (value == null)
                textBox.Text = String.Empty;
            else
                textBox.Text = value.ToString();
            return true;
        }
    }
}
