using MediaLibrary.ViewModels;
using MediaLibrary.ViewModels.Services;
using MediaLibrary.Views.FieldViews;
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
        private readonly INavigator navigator;
        private readonly Library library;

        public IModelViewProvider<IWpfRenderContext> ModelViewProvider => this;

        public ModelWindow(INavigator navigator, Library library)
        {
            Ensure.NotNull(navigator, "navigator");
            Ensure.NotNull(library, "library");
            this.navigator = navigator;
            this.library = library;
        }

        public bool TryGet(IModelDefinition modelDefinition, out IModelView<IWpfRenderContext> modelView)
        {
            if (modelDefinition.Identifier == "Movie")
                modelView = new WpfGridModelView(modelDefinition, this);
            else
                modelView = new WpfStackPanelModelView(modelDefinition, this);

            return true;
        }

        public bool TryGet(IModelDefinition modelDefinition, IFieldDefinition fieldDefinition, out IFieldView<IWpfRenderContext> fieldView)
        {
            fieldView = null;

            if (modelDefinition.Identifier == nameof(Movie) && fieldDefinition.Identifier == nameof(Movie.RelatedMovieKeys))
                fieldView = new WpfControlFieldView<RelatedMoviesFieldView>(fieldDefinition, new RelatedMoviesFieldView(new RelatedMoviesViewModel(navigator, library)));
            else if (fieldDefinition.Identifier == "Country")
                fieldView = new WpfComboBoxFieldEditor<string>(fieldDefinition, GetCountries());
            else if (fieldDefinition.Identifier == "Category")
                fieldView = new WpfComboBoxFieldEditor<string>(fieldDefinition, GetCategories());
            else if (fieldDefinition.FieldType == typeof(DateTime))
                fieldView = new WpfDateFieldEditor(fieldDefinition);
            else if (fieldDefinition.FieldType == typeof(string))
                fieldView = new WpfStringFieldEditor(fieldDefinition);
            else if (fieldDefinition.FieldType == typeof(int) || fieldDefinition.FieldType == typeof(int?))
                fieldView = new WpfInt32FieldEditor(fieldDefinition);

            return fieldView != null;
        }

        private IEnumerable<string> GetCountries() => library.Movies
            .Select(m => m.GetValueOrDefault("Country", (string)null))
            .Where(c => !String.IsNullOrEmpty(c))
            .OrderBy(c => c)
            .Distinct();

        private IEnumerable<string> GetCategories() => library.Movies
            .Select(m => m.GetValueOrDefault("Category", (string)null))
            .Where(c => !String.IsNullOrEmpty(c))
            .OrderBy(c => c)
            .Distinct();
    }
}
