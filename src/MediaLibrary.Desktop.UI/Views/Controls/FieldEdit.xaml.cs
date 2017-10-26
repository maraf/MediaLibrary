using Neptuo.Collections.Specialized;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaLibrary.Views.Controls
{
    public partial class FieldEdit : StackPanel
    {
        private TextBox textBox;

        public IFieldDefinition Definition
        {
            get { return (IFieldDefinition)GetValue(DefinitionProperty); }
            set { SetValue(DefinitionProperty, value); }
        }

        public static readonly DependencyProperty DefinitionProperty = DependencyProperty.Register(
            "Definition",
            typeof(IFieldDefinition),
            typeof(FieldEdit),
            new PropertyMetadata(null, OnFieldDefinitionChanged)
        );

        private static void OnFieldDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FieldEdit control = (FieldEdit)d;
            control.OnFieldDefinitionChanged();
        }

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(FieldEdit),
            new PropertyMetadata(null, OnValueChanged)
        );

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FieldEdit control = (FieldEdit)d;
            control.OnValueChanged();
        }

        public FieldEdit()
        {
            InitializeComponent();
            Background = null;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AutoFocus();
        }

        private void OnFieldDefinitionChanged()
        {
            if (Definition != null)
            {
                Label.Content = Definition.Metadata.Get("Label", Definition.Identifier);
                Editor.Content = textBox = new TextBox();
                textBox.TextChanged += OnTextBoxTextChanged;

                if (Definition.Metadata.TryGet("IsReadOnly", out bool isReadOnly) && isReadOnly)
                    textBox.IsEnabled = false;

                OnValueChanged();
                AutoFocus();
            }
            else
            {
                Label.Content = null;
                Editor.Content = null;
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            Value = textBox.Text;
        }

        private void OnValueChanged()
        {
            textBox.Text = Value?.ToString();
        }

        private void AutoFocus()
        {
            if (Definition != null && Definition.Metadata.TryGet("AutoFocus", out bool autoFocus) && autoFocus)
                textBox.Focus();
        }
    }
}
