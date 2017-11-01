using System;
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
            StackPanel panel = new StackPanel();
            context.Add(panel);

            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                Label label = null;
                if (fieldDefinition.Metadata.TryGetLabel(out string labelText))
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

                IFieldView<IWpfRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldDefinition);
                AddFieldView(fieldDefinition.Identifier, fieldView);

                fieldView.Render(fieldContext);
            }
        }
    }
}
