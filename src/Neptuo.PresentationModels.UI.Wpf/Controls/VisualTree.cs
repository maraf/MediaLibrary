using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Neptuo.PresentationModels.UI.Controls
{
    public static class VisualTree
    {
        public static T FindAncestorOfType<T>(FrameworkElement element, bool isElementIncluded = false)
        {
            foreach (FrameworkElement parent in EnumerateAncestors(element, isElementIncluded))
            {
                if (parent is T target)
                    return target;
            }

            return default;
        }

        public static IEnumerable<FrameworkElement> EnumerateAncestors(FrameworkElement element, bool isElementIncluded = false)
        {
            if (element != null && isElementIncluded)
                yield return element;

            while (element != null)
            {
                FrameworkElement parent = element.Parent as FrameworkElement;
                if (parent == null)
                    parent = element.TemplatedParent as FrameworkElement;

                if (parent == null)
                    parent = VisualTreeHelper.GetParent(element) as FrameworkElement;

                if (parent == null)
                    break;

                yield return parent;
                element = parent;
            }
        }



        public static bool TryGetModelDefinition(FrameworkElement element, out IModelDefinition definition)
        {
            ModelDefinitionContainer container = UserModelPresenter.GetContainer(element);
            if (container != null)
            {
                definition = container.Definition;
                return definition != null;
            }

            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(element))
            {
                container = UserModelPresenter.GetContainer(ancestor);
                if (container != null)
                {
                    definition = container.Definition;
                    return definition != null;
                }
            }

            definition = null;
            return false;
        }



        public static bool TryGetModelViewProvider(FrameworkElement element, out IModelViewProvider<IRenderContext> viewProvider)
        {
            viewProvider = ModelPresenter.GetViewProvider(element);
            if (viewProvider != null)
                return true;

            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(element))
            {
                viewProvider = ModelPresenter.GetViewProvider(ancestor);
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

        public static bool TryGetFieldViewProvider(FrameworkElement element, out IFieldViewProvider<IRenderContext> viewProvider)
        {
            viewProvider = UserFieldPresenter.GetViewProvider(element);
            if (viewProvider != null)
                return true;

            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(element))
            {
                viewProvider = UserFieldPresenter.GetViewProvider(ancestor);
                if (viewProvider != null)
                    return true;

                if (ancestor is IFieldViewProviderContainer<IRenderContext> container)
                {
                    viewProvider = container.ViewProvider;
                    return true;
                }

                if (ancestor is IFieldViewProvider<IRenderContext> provider)
                {
                    viewProvider = provider;
                    return true;
                }
            }

            return false;
        }


        public static FieldValueProviderCollection FindFieldValueProviderCollection(FrameworkElement element)
        {
            foreach (FrameworkElement ancestor in VisualTree.EnumerateAncestors(element))
            {
                FieldValueProviderCollection collection = UserModelPresenter.GetValueProviderCollection(ancestor);
                if (collection != null)
                    return collection;
            }

            return null;
        }


        public static void WithFieldDefinitionContainer(FrameworkElement element, Action<IFieldDefinition> handler)
        {
            FieldDefinitionContainer container = VisualTree.EnumerateAncestors(element, true).Select(a => UserFieldPresenter.GetContainer(a)).FirstOrDefault(c => c != null);
            if (container != null)
            {
                if (container.Definition != null)
                {
                    handler(container.Definition);
                }
                else
                {
                    container.Changed += () =>
                    {
                        if (container.Definition != null)
                            handler(container.Definition);
                    };
                }
            }
        }
    }
}
