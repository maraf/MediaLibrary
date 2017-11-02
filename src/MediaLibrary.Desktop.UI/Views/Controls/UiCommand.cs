using FontAwesome.WPF;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MediaLibrary.Views.Controls
{
    public class UiCommand : Freezable, INotifyPropertyChanged
    {
        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", 
            typeof(FontAwesomeIcon), 
            typeof(UiCommand), 
            new PropertyMetadata(FontAwesomeIcon.Empire)
        );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(UiCommand),
            new PropertyMetadata(OnCommandChanged)
        );

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "CommandParameter", 
            typeof(object), 
            typeof(UiCommand), 
            new PropertyMetadata(null)
        );

        private Action execute;
        public event Action Execute
        {
            add
            {
                execute += value;

                if (execute != null)
                    Command = new ActionCommand(execute);
            }
            remove
            {
                execute -= value;

                if (execute == null && Command is ActionCommand)
                    Command = null;
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(UiCommand),
            new PropertyMetadata(null)
        );


        public string ToolTip
        {
            get { return (string)GetValue(ToolTipProperty); }
            set { SetValue(ToolTipProperty, value); }
        }

        public static readonly DependencyProperty ToolTipProperty = DependencyProperty.Register(
            "ToolTip",
            typeof(string),
            typeof(UiCommand),
            new PropertyMetadata(null)
        );

        public event PropertyChangedEventHandler PropertyChanged;

        public UiCommand()
        {
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UiCommand)d).PropertyChanged?.Invoke(d, new PropertyChangedEventArgs(e.Property.Name));
        }

        protected override Freezable CreateInstanceCore()
        {
            return new UiCommand()
            {
                Command = Command,
                Text = Text,
                ToolTip = ToolTip
            };
        }

        private class ActionCommand : Command
        {
            private readonly Action execute;

            public ActionCommand(Action execute) => this.execute = execute;
            public override bool CanExecute() => true;
            public override void Execute() => execute?.Invoke();
        }
    }
}
