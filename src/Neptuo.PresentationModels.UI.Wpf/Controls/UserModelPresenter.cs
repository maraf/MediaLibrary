using Neptuo;
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
    /// A model definition is set through <see cref="ContainerProperty"/> or <see cref="Definition"/>.
    /// 
    /// This control uses its body to define model view.
    /// You can use there <see cref="UserFieldPresenter"/> to define containers of field definitions, and <see cref="UserFieldMetadataExtension"/> to show field metadata.
    /// </summary>
    /// <remarks>
    /// A property <see cref="ContainerProperty"/> should never be changed on <see cref="UserModelPresenter"/> (as it is set in constructor).
    /// </remarks>
    public class UserModelPresenter : ContentControl, IModelDefinitionContainer, IModelValueProvider, IUserFieldPresenterRegister
    {
        public static ModelDefinitionContainer GetContainer(DependencyObject obj)
        {
            return (ModelDefinitionContainer)obj.GetValue(ContainerProperty);
        }

        public static void SetContainer(DependencyObject obj, ModelDefinitionContainer value)
        {
            obj.SetValue(ContainerProperty, value);
        }

        /// <summary>
        /// A property marking target as a model definition container.
        /// This property must be set before childrens (<see cref="UserFieldPresenter"/>, <see cref="UserFieldMetadataExtension"/>, ...) are initialized, 
        /// because they are binding to this property value and its <see cref="ModelDefinitionContainer.Changed"/> event.
        /// </summary>
        public static readonly DependencyProperty ContainerProperty = DependencyProperty.RegisterAttached(
            "Container",
            typeof(ModelDefinitionContainer),
            typeof(ModelPresenter),
            new PropertyMetadata(null)
        );

        /// <summary>
        /// A shortcut for getting and setting a value of <see cref="ContainerProperty"/>.
        /// </summary>
        public IModelDefinition Definition
        {
            get { return GetContainer(this).Definition; }
            set { GetContainer(this).Definition = value; }
        }

        /// <summary>
        /// Creates a new instance with empty <see cref="Definition"/>.
        /// </summary>
        public UserModelPresenter()
        {
            SetContainer(this, new ModelDefinitionContainer());
            IsTabStop = false;
        }

        /// <summary>
        /// Creates a new instance with <paramref name="definition"/>.
        /// </summary>
        /// <param name="definition">A model definition.</param>
        public UserModelPresenter(IModelDefinition definition)
        {
            Ensure.NotNull(definition, "definition");
            Definition = definition;
        }

        /// <summary>
        /// Tries to get a value of field <paramref name="identifier"/> from registered presenter.
        /// </summary>
        /// <param name="identifier">A field identifer.</param>
        /// <param name="value">A current value.</param>
        /// <returns><c>true</c> if field presenter is registered and provided the <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public bool TryGetValue(string identifier, out object value)
        {
            if (presenters.TryGetValue(identifier, out UserFieldPresenter presenter))
                return presenter.TryGetValue(out value);

            value = null;
            return false;
        }

        /// <summary>
        /// Tries to set a value of field <paramref name="identifier"/> to registered presenter.
        /// </summary>
        /// <param name="identifier">A field identifer.</param>
        /// <param name="value">A value to set.</param>
        /// <returns><c>true</c> if field presenter is registered and accepted the <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public bool TrySetValue(string identifier, object value)
        {
            if (presenters.TryGetValue(identifier, out UserFieldPresenter presenter))
                return presenter.TrySetValue(value);

            return false;
        }

        public void Dispose() => TryDisposeFieldPresenters();

        /// <summary>
        /// Tries to dispose field presenters.
        /// </summary>
        private void TryDisposeFieldPresenters()
        {
            foreach (UserFieldPresenter presenter in presenters.Values)
                presenter.Dispose();
        }


        private Dictionary<string, UserFieldPresenter> presenters = new Dictionary<string, UserFieldPresenter>();

        void IUserFieldPresenterRegister.Add(string fieldIdentifier, UserFieldPresenter fieldPresenter)
        {
            presenters[fieldIdentifier] = fieldPresenter;
        }
    }
}
