using MediaLibrary.ViewModels.Services;
using Neptuo;
using Neptuo.Observables.Commands;
using Neptuo.PresentationModels;
using Neptuo.PresentationModels.UI;
using Neptuo.PresentationModels.UI.Controls;
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
    public partial class LibraryConfigurationWindow : ModelWindow, IModelValueProvider
    {
        private readonly IChangeTracker changeTracker;
        private readonly Library library;
        private readonly IModelDefinition modelDefinition;

        public LibraryConfigurationWindow(INavigator navigator, IChangeTracker changeTracker, Library library)
            : base(navigator, library)
        {
            Ensure.NotNull(changeTracker, "changeTracker");
            Ensure.NotNull(library, "library");

            UserModelPresenter.SetContainer(this, new ModelDefinitionContainer());
            UserModelPresenter.SetValueProviderCollection(this, new FieldValueProviderCollection());

            InitializeComponent();

            kebClose.Command = new DelegateCommand(Close);

            this.changeTracker = changeTracker;
            this.library = library;
            this.modelDefinition = library.ConfigurationDefinition;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            UserModelPresenter.GetContainer(this).Definition = modelDefinition;
            CopyModelValueProvider copy = new CopyModelValueProvider(modelDefinition, true);
            copy.Update(this, library.Configuration);
        }

        private void OnSaveClick()
        {
            changeTracker.UpdateModel(modelDefinition, library.Configuration, this);
            Close();
        }

        private void OnCloseClick() => Close();





        public bool TryGetValue(string identifier, out object value)
        {
            if (UserModelPresenter.GetValueProviderCollection(this).TryGet(identifier, out IFieldValueProvider provider))
                return provider.TryGetValue(out value);

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            if (UserModelPresenter.GetValueProviderCollection(this).TryGet(identifier, out IFieldValueProvider provider))
                return provider.TrySetValue(value);

            return false;
        }

        public void Dispose() => TryDisposeFieldValueProviders();

        private void TryDisposeFieldValueProviders()
        {
            foreach (IFieldValueProvider presenter in UserModelPresenter.GetValueProviderCollection(this))
            {
                if (presenter is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
