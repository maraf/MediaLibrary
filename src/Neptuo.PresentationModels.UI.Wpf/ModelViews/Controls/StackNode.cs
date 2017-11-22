using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews.Controls
{
    [TemplatePart(Name = "PART_Value", Type = typeof(ContentControl))]
    public class StackNode : Control, IFieldDefinitionContainer
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public IFieldView<IRenderContext> FieldView { get; internal set; }

        static StackNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackNode), new FrameworkPropertyMetadata(typeof(StackNode)));
        }

        public StackNode(IFieldDefinition fieldDefinition)
        {
            Ensure.NotNull(fieldDefinition, "fieldDefinition");
            FieldDefinition = fieldDefinition;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentControl value = (ContentControl)GetTemplateChild("PART_Value");
            if (value == null)
                throw Ensure.Exception.InvalidOperation("Missing 'PART_Value'.");

            ContentControlRenderContext context = new ContentControlRenderContext(value);
            FieldView.Render(context);
        }
    }
}
