using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace MediaLibrary.Views.Controls
{
    public class FieldPresenter : ItemsControl
    {
        protected List<IFieldDefinition> FieldDefinitions { get; private set; }

        public Movie Model
        {
            get { return (Movie)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(Movie),
            typeof(FieldPresenter),
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
            typeof(FieldPresenter),
            new PropertyMetadata(null, OnMetadataKeyChanged)
        );

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FieldPresenter control = (FieldPresenter)d;
            control.OnModelChanged((Movie)e.OldValue, (Movie)e.NewValue);
        }

        private static void OnMetadataKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FieldPresenter control = (FieldPresenter)d;
            control.Reload(control.Model);
        }

        private void OnModelChanged(Movie oldValue, Movie newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= OnModelPropertyChanged;
            
            if(newValue != null)
                newValue.PropertyChanged += OnModelPropertyChanged;

            Reload(newValue);
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (FieldDefinitions.Any(f => f.Identifier == e.PropertyName))
                BindItemsSource();
        }

        private void Reload(Movie model)
        {
            if (model == null)
                return;

            IEnumerable<IFieldDefinition> fields = model.Library.MovieDefinition.Fields;
            if (MetadataKey != null)
                fields = fields.Where(f => f.Metadata.Get(MetadataKey, false));

            FieldDefinitions = fields.ToList();
            BindItemsSource();
        }

        private void BindItemsSource()
        {
            ItemsSource = FieldDefinitions
                .Select(f => Model.GetValueOrDefault(f.Identifier, (object)null))
                .Where(v => v != null);
        }
    }
}
