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
    public class GridNode : Control, IFieldDefinitionContainer
    {
        public IFieldDefinition FieldDefinition { get; private set; }
        public IFieldView<IWpfRenderContext> FieldView { get; internal set; }

        public int Column
        {
            get => Grid.GetColumn(this);
            set => Grid.SetColumn(this, value);
        }

        public int Row
        {
            get => Grid.GetRow(this);
            set => Grid.SetRow(this, value);
        }

        public int ColumnSpan
        {
            get => Grid.GetColumnSpan(this);
            set => Grid.SetColumnSpan(this, value);
        }

        public int RowSpan
        {
            get => Grid.GetRowSpan(this);
            set => Grid.SetRowSpan(this, value);
        }

        static GridNode()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridNode), new FrameworkPropertyMetadata(typeof(GridNode)));
        }

        public GridNode(IFieldDefinition fieldDefinition)
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

            WpfContentControlRenderContext context = new WpfContentControlRenderContext(value);
            FieldView.Render(context);
        }
    }
}
