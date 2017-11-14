using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaLibrary.Views.Controls
{
    public class Field : ItemsControl
    {
        public Movie Model
        {
            get { return (Movie)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(Movie),
            typeof(Field),
            new PropertyMetadata(null, OnModelChanged)
        );

        public string MetadataKey
        {
            get { return (string)GetValue(MetadataKeyProperty); }
            set { SetValue(MetadataKeyProperty, value); }
        }

        public static readonly DependencyProperty MetadataKeyProperty = DependencyProperty.Register(
            "MetadataKey", 
            typeof(string), 
            typeof(Field), 
            new PropertyMetadata(null, OnMetadataKeyChanged)
        );

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Field control = (Field)d;
            control.Reload();
        }

        private static void OnMetadataKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Field control = (Field)d;
            control.Reload();
        }

        private void Reload()
        {
            Items.Clear();

            if (Model == null || MetadataKey == null)
                return;

            ItemsSource = Model.Library.MovieDefinition.Fields
                .Where(f => f.Metadata.Get(MetadataKey, false))
                .Select(f => Model.GetValueOrDefault(f.Identifier, (object)null));
        }
    }
}
