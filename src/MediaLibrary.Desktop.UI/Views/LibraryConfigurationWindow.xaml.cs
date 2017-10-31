using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables.Commands;
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaLibrary.Views
{
    public partial class LibraryConfigurationWindow : ModelWindow
    {
        private readonly Library library;
        private readonly IModelDefinition modelDefinition;

        public LibraryConfigurationWindow(Library library)
            : base(library)
        {
            Ensure.NotNull(library, "library");

            InitializeComponent();

            kebClose.Command = new DelegateCommand(Close);

            this.library = library;
            this.modelDefinition = library.ConfigurationDefinition;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            ModelView.Definition = modelDefinition;
            CopyModelValueProvider copy = new CopyModelValueProvider(modelDefinition, true);
            copy.Update(ModelView, library.Configuration);
        }

        private void OnSaveClick()
        {
            CopyModelValueProvider copy = new CopyModelValueProvider(modelDefinition, true);
            copy.Update(library.Configuration, ModelView);

            Close();
        }

        private void OnCloseClick() => Close();

        #region Presentation Models

        #endregion
    }
}
