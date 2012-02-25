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
using DesktopCore;

namespace MediaLibrary.GUI
{
    /// <summary>
    /// Interaction logic for CompareDialog.xaml
    /// </summary>
    public partial class CompareDialog : Window
    {
        public CompareData Data { get; protected set; }

        public event RoutedEventHandler CompareClick
        {
            add { AddHandler(CompareDialog.CompareClickEvent, value); }
            remove { RemoveHandler(CompareDialog.CompareClickEvent, value); }
        }

        public CompareDialog(CompareData data)
        {
            InitializeComponent();

            Data = data;
            DataContext = Data;

            if (Data.Databases.Count > 1 && Data.Source == null && Data.Target == null)
            {
                Data.Source = Data.Databases[1];
                Data.Target = Data.Databases[0];
                btnCompare.Focus();
            }
            else
            {
                coxSource.Focus();
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
        }

        private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Source != null && Data.Target != null && Data.Source != Data.Target)
            {
                lblError.Visibility = Visibility.Hidden;
                RaiseEvent(new RoutedEventArgs(CompareDialog.CompareClickEvent, this));
            }
            else
            {
                lblError.Visibility = Visibility.Visible;
            }
        }

        public static readonly RoutedEvent CompareClickEvent = EventManager.RegisterRoutedEvent("CompareClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CompareDialog));
    }
}
