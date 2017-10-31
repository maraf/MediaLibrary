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
        public static T FindAncestorOfType<T>(FrameworkElement element)
        {
            while (element != null)
            {
                FrameworkElement parent = element.Parent as FrameworkElement;
                if (parent == null)
                    parent = element.TemplatedParent as FrameworkElement;

                if (parent == null)
                    parent = VisualTreeHelper.GetParent(element) as FrameworkElement;

                if (parent == null)
                    break;

                if(parent is T target)
                    return target;

                element = parent;
            }

            return default(T);
        }
    }
}
