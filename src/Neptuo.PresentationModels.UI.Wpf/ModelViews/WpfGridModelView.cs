using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews
{
    public class WpfGridModelView : Grid, IModelView<IWpfRenderContext>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly IFieldViewProvider<IWpfRenderContext> fieldViewProvider;
        private readonly Dictionary<string, IFieldView<IWpfRenderContext>> fieldViews = new Dictionary<string, IFieldView<IWpfRenderContext>>();

        public WpfGridModelView(IModelDefinition modelDefinition, IFieldViewProvider<IWpfRenderContext> fieldViewProvider)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            Ensure.NotNull(fieldViewProvider, "fieldViewProvider");
            this.modelDefinition = modelDefinition;
            this.fieldViewProvider = fieldViewProvider;
        }

        public void Render(IWpfRenderContext context)
        {
            context.Add(this);

            IWpfRenderContext fieldContext = new WpfPanelRenderContext(this);
            foreach (IFieldDefinition fieldDefinition in modelDefinition.Fields)
            {
                IFieldView<IWpfRenderContext> fieldView = fieldViewProvider.Get(modelDefinition, fieldDefinition);
                fieldViews[fieldDefinition.Identifier] = fieldView;
                fieldView.Render(fieldContext);
            }
        }

        public bool TryGetValue(string identifier, out object value)
        {
            if (fieldViews.TryGetValue(identifier, out var fieldView))
                return fieldView.TryGetValue(out value);

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            if (fieldViews.TryGetValue(identifier, out var fieldView))
                return fieldView.TrySetValue(value);

            return false;
        }

        public void Dispose()
        {
            foreach (IFieldView<IWpfRenderContext> fieldView in fieldViews.Values)
            {
                if (fieldView is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
