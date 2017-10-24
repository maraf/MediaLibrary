using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MediaLibrary.Views.Controls
{
    public class SystemColorProvider
    {
        public static Color ColorizationColor()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\DWM");
            int value = (int)registry.GetValue("ColorizationColor");
            System.Drawing.Color color = System.Drawing.Color.FromArgb(value);

            return Color.FromArgb(Byte.MaxValue, color.R, color.G, color.B);
        }
    }
}
