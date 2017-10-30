﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public class WpfStackPanelModelView : ModelView<IWpfRenderContext>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IFieldViewProvider<IWpfRenderContext> fieldViewProvider;

        public WpfStackPanelModelView(IModelDefinition modelDefinition, IFieldViewProvider<IWpfRenderContext> fieldViewProvider)
            : base(modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldViewProvider, "fieldViewProvider");
            this.modelDefinition = modelDefinition;
            this.fieldViewProvider = fieldViewProvider;
        }

        protected override void RenderInternal(IWpfRenderContext context)
        {
            StackPanel grid = new StackPanel();

            context.Add(grid);

            IWpfRenderContext fieldContext = new WpfPanelRenderContext(grid);
            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                IFieldView<IWpfRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldDefinition);
                AddFieldView(fieldDefinition.Identifier, fieldView);

                if (fieldDefinition.Metadata.TryGet("Label", out string label))
                    fieldContext.Add(new TextBlock() { Text = label });

                fieldView.Render(fieldContext);
            }
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            //foreach (IFieldView<IWpfRenderContext> fieldView in fieldViews.Values)
            //{
            //    if (fieldView is IDisposable disposable)
            //        disposable.Dispose();
            //}
        }
    }
}