﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class WpfStringFieldEditor : FieldView<string, IWpfRenderContext>
    {
        private TextBox textBox;

        public WpfStringFieldEditor(IFieldDefinition fieldDefinition)
            : base(fieldDefinition)
        { }

        protected override void RenderInternal(IWpfRenderContext context, string defaultValue)
        {
            textBox = new TextBox();
            textBox.Text = defaultValue;
            context.Add(textBox);

            if (FieldDefinition.Metadata.TryGet("IsReadOnly", out bool isReadOnly))
                textBox.IsReadOnly = isReadOnly;

            if (FieldDefinition.Metadata.TryGet("IsAutoFocus", out bool isAutoFocus) && isAutoFocus)
                textBox.Focus();
        }

        protected override bool TryGetValueInternal(out string value)
        {
            if (textBox != null)
            {
                value = textBox.Text;
                return true;
            }

            value = null;
            return false;
        }

        protected override bool TrySetValueInternal(string value)
        {
            if (textBox != null)
            {
                textBox.Text = value;
                return true;
            }

            return false;
        }
    }
}