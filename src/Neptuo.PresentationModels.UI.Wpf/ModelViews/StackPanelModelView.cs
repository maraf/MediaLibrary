using Neptuo.PresentationModels.UI.ModelViews.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public class StackPanelModelView : ModelView<IRenderContext>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IFieldViewProvider<IRenderContext> fieldViewProvider;

        public StackPanelModelView(IModelDefinition modelDefinition, IFieldViewProvider<IRenderContext> fieldViewProvider)
            : base(modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldViewProvider, "fieldViewProvider");
            this.modelDefinition = modelDefinition;
            this.fieldViewProvider = fieldViewProvider;
        }

        protected override void RenderInternal(IRenderContext context)
        {
            StackContainer panel = new StackContainer(modelDefinition);
            context.Add(panel);

            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                StackNode node = new StackNode(fieldDefinition);
                panel.Children.Add(node);

                IFieldView<IRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldDefinition);
                AddFieldView(fieldDefinition.Identifier, fieldView);
                node.FieldView = fieldView;
            }
        }
    }
}
