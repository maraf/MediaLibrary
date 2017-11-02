﻿using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Neptuo.PresentationModels.UI.FieldViews
{
    public class WpfControlFieldView<T> : FieldView<IWpfRenderContext>
        where T : UIElement, IFieldValueProvider
    {
        private readonly T control;

        public WpfControlFieldView(IFieldDefinition fieldDefinition, T control)
            : base(fieldDefinition)
        {
            Ensure.NotNull(control, "control");
            this.control = control;
        }

        public override void Render(IWpfRenderContext context) => context.Add(control);
        public override bool TryGetValue(out object value) => control.TryGetValue(out value);
        public override bool TrySetValue(object value) => control.TrySetValue(value);
    }
}
