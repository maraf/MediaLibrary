using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.Controls
{
    public class ModelPresenter : ContentControl, IModelValueProvider
    {
        protected IModelView<IWpfRenderContext> View { get; set; }

        public IModelDefinition Definition
        {
            get { return (IModelDefinition)GetValue(DefinitionProperty); }
            set { SetValue(DefinitionProperty, value); }
        }

        public static readonly DependencyProperty DefinitionProperty = DependencyProperty.Register(
            "Definition",
            typeof(IModelDefinition),
            typeof(ModelPresenter),
            new PropertyMetadata(null, OnDefinitionChanged)
        );

        private static void OnDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModelPresenter view = (ModelPresenter)d;
            view.OnDefinitionChanged();
        }

        public ModelPresenter()
        {
            IsTabStop = false;
        }

        private void OnDefinitionChanged()
        {
            TryDisposeModelView();

            if (Definition == null)
                return;

            IModelViewProviderContainer<IWpfRenderContext> container = VisualTree.FindAncestorOfType<IModelViewProviderContainer<IWpfRenderContext>>(this);
            if (container == null)
                throw Ensure.Exception.NotSupported($"Missing '{nameof(IModelViewProviderContainer<IWpfRenderContext>)}' in ancestor chain.");

            View = container.ModelViewProvider.Get(Definition);
            View.Render(new WpfContentControlRenderContext(this));
        }

        public bool TryGetValue(string identifier, out object value)
        {
            if (View != null)
                return View.TryGetValue(identifier, out value);

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            if (View != null)
                return View.TrySetValue(identifier, value);

            return false;
        }

        private void TryDisposeModelView()
        {
            if (View != null && View is IDisposable disposable)
            {
                disposable.Dispose();
                View = null;
            }
        }

        public void Dispose() => TryDisposeModelView();
    }
}
