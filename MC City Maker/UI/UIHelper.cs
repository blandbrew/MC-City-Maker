using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MC_City_Maker.UI
{
    /// <summary>
    /// Helper methods for UI-related tasks.
    /// </summary>
    public static class UIHelper
    {
        public static T FindChild<T>(this FrameworkElement obj, string name)
        {
            DependencyObject dep = obj as DependencyObject;
            T ret = default(T);

            if (dep != null)
            {
                int childcount = VisualTreeHelper.GetChildrenCount(dep);
                for (int i = 0; i < childcount; i++)
                {
                    DependencyObject childDep = VisualTreeHelper.GetChild(dep, i);
                    FrameworkElement child = childDep as FrameworkElement;

                    if (child.GetType() == typeof(T) && child.Name == name)
                    {
                        ret = (T)Convert.ChangeType(child, typeof(T));
                        break;
                    }

                    ret = child.FindChild<T>(name);
                    if (ret != null)
                        break;
                }
            }
            return ret;
        }

        public static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }

}
