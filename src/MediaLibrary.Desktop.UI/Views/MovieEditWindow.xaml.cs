using MediaLibrary.ViewModels;
using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables.Commands;
using Neptuo.PresentationModels.UI;
using Neptuo.PresentationModels.UI.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.UI.FieldViews;
using Neptuo.PresentationModels.TypeModels;

namespace MediaLibrary.Views
{
    public partial class MovieEditWindow : ModelWindow
    {
        private readonly Library library;
        private readonly IModelDefinition modelDefinition;
        private Movie model;

        public MovieEditWindow(Library library, Movie model)
        {
            Ensure.NotNull(library, "library");

            InitializeComponent();

            kebClose.Command = new DelegateCommand(Close);

            this.library = library;
            this.modelDefinition = library.MovieDefinition;
            this.model = model;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            ModelView.Definition = modelDefinition;
            if (model != null)
            {
                CopyModelValueProvider copy = new CopyModelValueProvider(modelDefinition, true);
                copy.Update(ModelView, model);
            }
            else
            {
                ModelView.TrySetValue(nameof(Movie.Added), DateTime.Now);
            }
        }

        private void OnSaveClick()
        {
            if (!ModelView.TryGetValue(nameof(Movie.Name), out object rawName) || String.IsNullOrEmpty(rawName as string))
                return;

            if (model == null)
                model = library.Create();

            CopyModelValueProvider copy = new CopyModelValueProvider(modelDefinition, true);
            copy.Update(model, ModelView);

            Close();
        }

        private void OnCloseClick() => Close();
    }
}
