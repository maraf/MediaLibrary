using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaLibrary.Core;
using System.Windows;

namespace MediaLibrary.Controls
{
    public class CommandEventArgs : RoutedEventArgs
    {
        public MediaLibrary MediaLibrary { get; set; }

        public Database Database { get; set; }

        public Commands Command { get; set; }

        public CommandEventArgs(RoutedEvent e, object sender)
            : base(e, sender)
        { }
    }

    public enum Commands
    {
        CopyMovie, PasteMovie
    }
}
