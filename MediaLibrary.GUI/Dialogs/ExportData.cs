using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MediaLibrary.Export;

namespace MediaLibrary.GUI
{
    public delegate void ExportHandler(object sender, ExportEventArgs e);

    public class ExportEventArgs : RoutedEventArgs
    {
        public ExportData Data { get; set; }

        public ExportEventArgs(RoutedEvent e)
            : base(e) { }
    }
}
