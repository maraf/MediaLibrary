using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.Controls
{
    public class ModelView : ContentControl, IModelValueProvider
    {
        protected IModelView<IWpfRenderContext> ModelView { get; set; }

        public IModelDefinition Definition
        {
            get { return (IModelDefinition)GetValue(DefinitionProperty); }
            set { SetValue(DefinitionProperty, value); }
        }

        public static readonly DependencyProperty DefinitionProperty = DependencyProperty.Register(
            "Definition",
            typeof(IModelDefinition),
            typeof(ModelView),
            new PropertyMetadata(null, OnDefinitionChanged)
        );

        private static void OnDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModelView view = (ModelView)d;
            view.OnDefinitionChanged();
        }

        public ModelView()
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

            ModelView = container.ModelViewProvider.Get(Definition);
            ModelView.Render(new WpfContentControlRenderContext(this));
        }

        public bool TryGetValue(string identifier, out object value)
        {
            if (ModelView != null)
                return ModelView.TryGetValue(identifier, out value);

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            if (ModelView != null)
                return ModelView.TrySetValue(identifier, value);

            return false;
        }

        private void TryDisposeModelView()
        {
            if (ModelView != null && ModelView is IDisposable disposable)
            {
                disposable.Dispose();
                ModelView = null;
            }
        }

        public void Dispose() => TryDisposeModelView();
    }
}
