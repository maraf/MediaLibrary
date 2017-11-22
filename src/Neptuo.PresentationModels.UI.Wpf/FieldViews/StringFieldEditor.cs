using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class StringFieldEditor : FieldView<string, IRenderContext>
    {
        private TextBox textBox;

        public StringFieldEditor(IFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        protected override void RenderInternal(IRenderContext context, string defaultValue)
        {
            textBox = new TextBox();
            TrySetValueInternal(defaultValue);
            context.Add(textBox);

            if (FieldDefinition.Metadata.TryGet("IsReadOnly", out bool isReadOnly))
                textBox.IsReadOnly = isReadOnly;

            if (FieldDefinition.Metadata.TryGet("IsAutoFocus", out bool isAutoFocus) && isAutoFocus)
                textBox.Loaded += (sender, e) => textBox.Focus();
        }

        protected override bool TryGetValueInternal(out string value)
        {
            value = textBox.Text;
            return true;
        }

        protected override bool TrySetValueInternal(string value)
        {
            textBox.Text = value;
            return true;
        }
    }
}
