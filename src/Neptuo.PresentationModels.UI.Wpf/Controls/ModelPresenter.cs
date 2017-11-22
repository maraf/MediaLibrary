using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.Controls
{
    /// <summary>
    /// A control for presenting a model definition.
    /// A model definition is set through <see cref="DefinitionProperty"/>.
    /// 
    /// This control requires an instance of <see cref="IModelViewProvider{T}"/> to know to render the definition.
    /// It be passed in various way. A simplest is to set a property <see cref="ViewProviderProperty"/> on this control or on any of ancestors (a first one will be used).
    /// Another way is to implement an <see cref="IModelViewProvider{T}"/> or <see cref="IModelViewProviderContainer{T}"/> in ancestors (a closest will be used).
    /// If none is satisfied, none is rendered.
    /// </summary>
    public class ModelPresenter : ContentControl, IModelValueProvider
    {
        protected IModelView<IRenderContext> View { get; set; }

        public IModelDefinition Definition
        {
            get { return (IModelDefinition)GetValue(DefinitionProperty); }
            set { SetValue(DefinitionProperty, value); }
        }

        /// <summary>
        /// A property holding instance of model definition to work with.
        /// </summary>
        public static readonly DependencyProperty DefinitionProperty = DependencyProperty.Register(
            "Definition",
            typeof(IModelDefinition),
            typeof(ModelPresenter),
            new PropertyMetadata(null, OnDefinitionChanged)
        );

        private static void OnDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ModelPresenter view = (ModelPresenter)d;
            view.OnChanged();
        }

        public static IModelViewProvider<IRenderContext> GetViewProvider(DependencyObject obj)
        {
            return (IModelViewProvider<IRenderContext>)obj.GetValue(ViewProviderProperty);
        }

        public static void SetViewProvider(DependencyObject obj, IModelViewProvider<IRenderContext> value)
        {
            obj.SetValue(ViewProviderProperty, value);
        }

        /// <summary>
        /// A property holding a provider of model views.
        /// </summary>
        public static readonly DependencyProperty ViewProviderProperty = DependencyProperty.RegisterAttached(
            "ViewProvider",
            typeof(IModelViewProvider<IRenderContext>),
            typeof(ModelPresenter),
            new PropertyMetadata(null, OnViewProviderChanged)
        );

        private static void OnViewProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ModelPresenter view)
                view.OnChanged();
        }

        public ModelPresenter()
        {
            IsTabStop = false;
        }

        private void OnChanged()
        {
            TryDisposeModelView();

            if (Definition == null)
                return;

            if (TryGetViewProvider(out IModelViewProvider<IRenderContext> viewProvider))
            {
                View = viewProvider.Get(Definition);
                View.Render(new ContentControlRenderContext(this));
            }
        }

        private bool TryGetViewProvider(out IModelViewProvider<IRenderContext> viewProvider)
        {
            viewProvider = GetViewProvider(this);
            if (viewProvider != null)
                return true;

            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(this))
            {
                viewProvider = GetViewProvider(ancestor);
                if (viewProvider != null)
                    return true;

                if (ancestor is IModelViewProviderContainer<IRenderContext> container)
                {
                    viewProvider = container.ViewProvider;
                    return true;
                }

                if (ancestor is IModelViewProvider<IRenderContext> provider)
                {
                    viewProvider = provider;
                    return true;
                }
            }

            return false;
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
