using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public partial class WpfGridModelView : ModelView<IWpfRenderContext>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IFieldViewProvider<IWpfRenderContext> fieldViewProvider;

        public WpfGridModelView(IModelDefinition modelDefinition, IFieldViewProvider<IWpfRenderContext> fieldViewProvider)
            : base(modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldViewProvider, "fieldViewProvider");
            this.modelDefinition = modelDefinition;
            this.fieldViewProvider = fieldViewProvider;
        }

        protected override void RenderInternal(IWpfRenderContext context)
        {
            Grid grid = new Grid();

            context.Add(grid);

            List<(int column, int row, int with, int height, IFieldDefinition definition)> fieldPositions = new List<(int column, int row, int with, int height, IFieldDefinition definition)>();

            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                int column = fieldDefinition.Metadata.Get("Grid.Column", 0);
                int row = fieldDefinition.Metadata.Get("Grid.Row", 0);

                fieldPositions.Add((column, row, 0, 0, fieldDefinition));
            }

            int columns = fieldPositions.Select(f => f.column).Max() + 1;
            int rows = fieldPositions.Select(f => f.row).Max() + 1;

            for (int i = 0; i < columns; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < rows; i++)
                grid.RowDefinitions.Add(new RowDefinition());

            foreach (var fieldPosition in fieldPositions)
            {
                StackPanel panel = new StackPanel();
                grid.Children.Add(panel);

                panel.SetValue(Grid.ColumnProperty, fieldPosition.column);
                panel.SetValue(Grid.RowProperty, fieldPosition.row);

                if (fieldPosition.definition.Metadata.TryGet("Grid.ColumnSpan", out int columnSpan))
                    panel.SetValue(Grid.ColumnSpanProperty, columnSpan);

                if (fieldPosition.definition.Metadata.TryGet("Grid.RowSpan", out int rowSpan))
                    panel.SetValue(Grid.ColumnSpanProperty, rowSpan);

                Label label = null;
                if (fieldPosition.definition.Metadata.TryGet("Label", out string labelText))
                {
                    label = new Label() { Content = labelText };
                    panel.Children.Add(label);
                }

                WpfPanelRenderContext fieldContext = new WpfPanelRenderContext(panel);
                fieldContext.Added += element =>
                {
                    if (label != null)
                        label.Target = element;
                };

                IFieldView<IWpfRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldPosition.definition);
                AddFieldView(fieldPosition.definition.Identifier, fieldView);

                fieldView.Render(fieldContext);
            }
        }
    }
}
