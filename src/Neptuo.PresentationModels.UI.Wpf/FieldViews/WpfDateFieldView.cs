using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class WpfDateFieldView : FieldView<DateTime, IWpfRenderContext>
    {
        private TextBox textBox;

        public WpfDateFieldView(IFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        protected override void RenderInternal(IWpfRenderContext context, DateTime defaultValue)
        {
            textBox = new TextBox();
            TrySetValueInternal(defaultValue);

            if (FieldDefinition.Metadata.TryGet("IsReadOnly", out bool isReadOnly))
                textBox.IsReadOnly = isReadOnly;

            context.Add(textBox);
        }

        protected override bool TryGetValueInternal(out DateTime value)
        {
            if (textBox != null && DateTime.TryParse(textBox.Text, out value))
                return true;

            value = DateTime.MinValue;
            return false;
        }

        protected override bool TrySetValueInternal(DateTime value)
        {
            if (textBox != null)
            {
                textBox.Text = value.ToString();
                return true;
            }

            return false;
        }
    }
}
