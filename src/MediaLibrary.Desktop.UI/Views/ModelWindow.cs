using Neptuo;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.UI;
using Neptuo.PresentationModels.UI.FieldViews;
using Neptuo.PresentationModels.UI.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaLibrary.Views
{
    public abstract class ModelWindow : Window, IModelViewProviderContainer<IWpfRenderContext>, IModelViewProvider<IWpfRenderContext>, IFieldViewProvider<IWpfRenderContext>
    {
        private readonly Library library;

        public IModelViewProvider<IWpfRenderContext> ModelViewProvider => this;

        public ModelWindow(Library library)
        {
            Ensure.NotNull(library, "library");
            this.library = library;
        }

        public bool TryGet(IModelDefinition modelDefinition, out IModelView<IWpfRenderContext> modelView)
        {
            modelView = new WpfStackPanelModelView(modelDefinition, this);
            return true;
        }

        public bool TryGet(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, out IFieldView<IWpfRenderContext> fieldView)
        {
            fieldView = null;

            if (fieldDefinition.FieldType == typeof(DateTime))
                fieldView = new WpfDateFieldEditor(fieldDefinition);
            else if (fieldDefinition.FieldType == typeof(string))
                fieldView = new WpfStringFieldEditor(fieldDefinition);
            else if (fieldDefinition.FieldType == typeof(int) || fieldDefinition.FieldType == typeof(int?))
                fieldView = new WpfInt32FieldEditor(fieldDefinition);

            return fieldView != null;
        }
    }
}
