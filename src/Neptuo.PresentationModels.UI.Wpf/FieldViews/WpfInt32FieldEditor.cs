using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class WpfInt32FieldEditor : FieldView<int?, IWpfRenderContext>
    {
        private TextBox textBox;

        public WpfInt32FieldEditor(IFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        protected override void RenderInternal(IWpfRenderContext context, int? defaultValue)
        {
            textBox = new TextBox();
            TrySetValueInternal(defaultValue);
            context.Add(textBox);

            if (FieldDefinition.Metadata.TryGet("IsReadOnly", out bool isReadOnly))
                textBox.IsReadOnly = isReadOnly;

            if (FieldDefinition.Metadata.TryGet("IsAutoFocus", out bool isAutoFocus) && isAutoFocus)
                textBox.Loaded += (sender, e) => textBox.Focus();
        }

        protected override bool TryGetValueInternal(out int? value)
        {
            if (Int32.TryParse(textBox.Text, out int rawValue))
            {
                value = rawValue;
                return true;
            }

            if (FieldDefinition.FieldType == typeof(int?))
            {
                value = null;
                return true;
            }

            value = Int32.MinValue;
            return false;
        }

        protected override bool TrySetValueInternal(int? value)
        {
            if (value == null)
                textBox.Text = String.Empty;
            else
                textBox.Text = value.ToString();

            return true;
        }
    }
}
