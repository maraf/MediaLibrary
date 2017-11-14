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
    public class MainLeftField : TextBlock
    {
        public Movie Model
        {
            get { return (Movie)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model",
            typeof(Movie),
            typeof(MainLeftField),
            new PropertyMetadata(null, OnModelChanged)
        );

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MainLeftField control = (MainLeftField)d;

            Movie model = control.Model;
            if (model == null)
            {
                control.Text = null;
                return;
            }

            IFieldDefinition fieldDefinition = model.Library.MovieDefinition.Fields.FirstOrDefault(f => f.Metadata.Get("Main.Left", false));
            if (fieldDefinition == null)
            {
                control.Text = null;
                return;
            }

            control.Text = model.GetValueOrDefault(fieldDefinition.Identifier, string.Empty);
        }
    }
}
