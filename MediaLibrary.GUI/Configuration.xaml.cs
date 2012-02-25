using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MediaLibrary.Core;
using System.Globalization;
using System.Threading;
using DesktopCore;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
        public IManager Manager { get; set; }

        public Configuration(IManager manager)
        {
            InitializeComponent();

            Manager = manager;
            DataContext = Manager;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;
            if (expander != null)
            {
                if (expCommon != null && expander != expCommon)
                    expCommon.IsExpanded = false;
            }
        }

        private void btnApplyLanguage_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo culture = CultureInfo.GetCultureInfo((coxLanguages.SelectedItem as Language).Code);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Resource.Load("Resources/Resources");
            Resource.ReProvideAll();
        }
    }
}
