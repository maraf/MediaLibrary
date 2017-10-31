using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public class WpfGridModelView : ModelView<IWpfRenderContext>
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

            IWpfRenderContext fieldContext = new WpfPanelRenderContext(grid);
            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                IFieldView<IWpfRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldDefinition);
                AddFieldView(fieldDefinition.Identifier, fieldView);
                fieldView.Render(fieldContext);
            }
        }
    }
}
