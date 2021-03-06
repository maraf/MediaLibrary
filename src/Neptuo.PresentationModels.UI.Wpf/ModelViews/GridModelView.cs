﻿using Neptuo;
using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels.UI.ModelViews.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public partial class GridModelView : ModelView<IRenderContext>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IFieldViewProvider<IRenderContext> fieldViewProvider;

        public GridModelView(IModelDefinition modelDefinition, IFieldViewProvider<IRenderContext> fieldViewProvider)
            : base(modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldViewProvider, "fieldViewProvider");
            this.modelDefinition = modelDefinition;
            this.fieldViewProvider = fieldViewProvider;
        }

        protected override void RenderInternal(IRenderContext context)
        {
            GridContainer grid = new GridContainer(modelDefinition);

            context.Add(grid);

            List<(int column, int row, int with, int height, IFieldDefinition definition)> fieldPositions = new List<(int column, int row, int with, int height, IFieldDefinition definition)>();

            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                int column = fieldDefinition.Metadata.GetGridColumn(0);
                int row = fieldDefinition.Metadata.GetGridRow(0);

                fieldPositions.Add((column, row, 0, 0, fieldDefinition));
            }

            int columns = fieldPositions.Select(f => f.column).Max() + 1;
            int rows = fieldPositions.Select(f => f.row).Max() + 1;

            for (int i = 0; i < columns; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < rows; i++)
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0, GridUnitType.Auto) });

            foreach (var fieldPosition in fieldPositions)
            {
                GridNode node = new GridNode(fieldPosition.definition);
                grid.Children.Add(node);

                node.Column = fieldPosition.column;
                node.Row = fieldPosition.row;

                if (fieldPosition.definition.Metadata.TryGetGridColumnSpan(out int columnSpan))
                    node.ColumnSpan = columnSpan;

                if (fieldPosition.definition.Metadata.TryGetGridRowSpan(out int rowSpan))
                    node.RowSpan = rowSpan;

                IFieldView<IRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldPosition.definition);
                AddFieldView(fieldPosition.definition.Identifier, fieldView);
                node.FieldView = fieldView;
            }
        }
    }
}
