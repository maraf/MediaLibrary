using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Base implementation of <see cref="IModelView{T}"/> based on field views.
    /// </summary>
    public abstract class ModelView<T> : DisposableBase, IModelView<T>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly Dictionary<string, IFieldView<T>> fieldViews;
        private bool isRendered;

        /// <summary>
        /// Gets a collection of field views.
        /// </summary>
        protected IReadOnlyCollection<IFieldView<T>> FieldViews
        {
            get { return fieldViews.Values; }
        }

        /// <summary>
        /// Creates new instance for model defined by <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        public ModelView(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            this.modelDefinition = modelDefinition;
            this.fieldViews = new Dictionary<string, IFieldView<T>>();
        }

        /// <summary>
        /// Registers field view for field.
        /// </summary>
        /// <param name="fieldIdentifier">Field identifier..</param>
        /// <param name="fieldView">Field view.</param>
        protected void AddFieldView(string fieldIdentifier, IFieldView<T> fieldView)
        {
            Ensure.NotNullOrEmpty(fieldIdentifier, "fieldIdentifier");
            Ensure.NotNull(fieldView, "fieldView");
            fieldViews[fieldIdentifier] = fieldView;
        }

        #region IModelView

        public bool TryGetValue(string identifier, out object value)
        {
            if (isRendered)
            {
                IFieldView<T> fieldView;
                if (fieldViews.TryGetValue(identifier, out fieldView))
                    return fieldView.TryGetValue(out value);
            }

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            if (isRendered)
            {
                IFieldView<T> fieldView;
                if (fieldViews.TryGetValue(identifier, out fieldView))
                    return fieldView.TrySetValue(value);
            }

            return false;
        }

        public void Render(T target)
        {
            if (!isRendered)
            {
                isRendered = true;
                RenderInternal(target);
            }
        }

        /// <summary>
        /// Called once to render view.
        /// </summary>
        /// <param name="target">Rendering context.</param>
        protected abstract void RenderInternal(T target);

        #endregion

        /// <summary>
        /// Disposes all <see cref="FieldViews"/> that implements <see cref="IDisposable"/>.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            foreach (IFieldView<T> fieldView in FieldViews)
            {
                if (fieldView is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}
