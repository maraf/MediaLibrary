using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.Controls
{
    public class UserFieldPresenter : ContentControl, IFieldDefinitionContainer, IFieldValueProvider
    {
        public static FieldDefinitionContainer GetContainer(DependencyObject obj)
        {
            return (FieldDefinitionContainer)obj.GetValue(ContainerProperty);
        }

        public static void SetContainer(DependencyObject obj, FieldDefinitionContainer value)
        {
            obj.SetValue(ContainerProperty, value);
        }

        /// <summary>
        /// A property marking the target as a field definition container.
        /// This property must be set before childrens (<see cref="UserFieldPresenter"/>, <see cref="UserFieldMetadataExtension"/>, ...) are initialized, 
        /// because they are binding to this property value and its <see cref="FieldDefinitionContainer.Changed"/> event.
        /// </summary>
        public static readonly DependencyProperty ContainerProperty = DependencyProperty.RegisterAttached(
            "Container",
            typeof(FieldDefinitionContainer),
            typeof(UserFieldPresenter),
            new PropertyMetadata(null)
        );

        public static string GetIdentifier(DependencyObject obj)
        {
            return (string)obj.GetValue(IdentifierProperty);
        }

        public static void SetIdentifier(DependencyObject obj, string value)
        {
            obj.SetValue(IdentifierProperty, value);
        }

        /// <summary>
        /// Setting this property makes the target a holder of field definition.
        /// This property sets and is used in combination with <see cref="ContainerProperty"/>.
        /// </summary>
        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.RegisterAttached(
            "Identifier",
            typeof(string),
            typeof(UserFieldPresenter),
            new PropertyMetadata(null, OnIdentifierChanged)
        );

        private static void OnIdentifierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetContainer(d) == null)
                SetContainer(d, new FieldDefinitionContainer());

            if (d is UserFieldPresenter field)
                field.BindToFieldDefinitionContainer();

            BindModelContainerChanged(d);
        }

        private static void BindModelContainerChanged(DependencyObject d)
        {
            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors((FrameworkElement)d))
            {
                ModelDefinitionContainer container = UserModelPresenter.GetContainer(ancestor);
                if (container != null)
                {
                    SetContainerDefinition(d, container.Definition);
                    container.Changed += () => SetContainerDefinition(d, container.Definition);
                    break;
                }
            }
        }

        private static void SetContainerDefinition(DependencyObject d, IModelDefinition modelDefinition)
        {
            FieldDefinitionContainer container = GetContainer(d);
            string identifier = GetIdentifier(d);
            if (container != null)
            {
                if (identifier != null && modelDefinition != null)
                    container.Definition = modelDefinition.Fields.FirstOrDefault(f => f.Identifier == identifier);
                else
                    container.Definition = null;
            }
        }

        public static IFieldViewProvider<IRenderContext> GetViewProvider(DependencyObject obj)
        {
            return (IFieldViewProvider<IRenderContext>)obj.GetValue(ViewProviderProperty);
        }

        public static void SetViewProvider(DependencyObject obj, IFieldViewProvider<IRenderContext> value)
        {
            obj.SetValue(ViewProviderProperty, value);
        }

        /// <summary>
        /// A property holding a provider of field views.
        /// </summary>
        public static readonly DependencyProperty ViewProviderProperty = DependencyProperty.RegisterAttached(
            "ViewProvider",
            typeof(IFieldViewProvider<IRenderContext>),
            typeof(UserFieldPresenter),
            new PropertyMetadata(null, OnViewProviderChanged)
        );

        private static void OnViewProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UserFieldPresenter view)
                view.OnChanged(null);
        }

        public IFieldDefinition Definition { get; private set; }

        protected IFieldView<IRenderContext> View { get; set; }

        public UserFieldPresenter()
        {
            IsTabStop = false;
            Content = "";
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            BindToFieldDefinitionContainer();
        }

        private bool isAttached;

        private void BindToFieldDefinitionContainer()
        {
            if (isAttached)
                return;

            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(this, true))
            {
                FieldDefinitionContainer container = GetContainer(ancestor);
                if (container != null)
                {
                    isAttached = true;

                    container.Changed += () =>
                    {
                        Console.WriteLine(ancestor.Name);
                        OnChanged(container.Definition);
                    };

                    if (container.Definition != null)
                        OnChanged(container.Definition);
                }
            }
        }

        private void OnChanged(IFieldDefinition definition)
        {
            TryDisposeFieldView();

            Definition = definition;
            TryRegisterSelf();

            if (VisualTree.TryGetFieldViewProvider(this, out IFieldViewProvider<IRenderContext> viewProvider) && VisualTree.TryGetModelDefinition(this, out IModelDefinition modelDefinition))
            {
                View = viewProvider.Get(modelDefinition, definition);
                View.Render(new ContentControlRenderContext(this));
            }
        }

        private bool isAdded;

        private void TryRegisterSelf()
        {
            if (!isAdded)
            {
                FieldValueProviderCollection collection = VisualTree.FindFieldValueProviderCollection(this);
                if (collection != null)
                {
                    collection.Add(Definition.Identifier, this);
                    isAdded = true;
                }
            }
        }

        public bool TryGetValue(out object value)
        {
            if (View != null)
                return View.TryGetValue(out value);

            value = null;
            return false;
        }

        public bool TrySetValue(object value)
        {
            if (View != null)
                return View.TrySetValue(value);

            return false;
        }

        private void TryDisposeFieldView()
        {
            if (View != null && View is IDisposable disposable)
            {
                disposable.Dispose();
                View = null;
            }
        }

        public void Dispose() => TryDisposeFieldView();
    }
}
