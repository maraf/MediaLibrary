using FontAwesome.WPF;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MediaLibrary.Views.Controls
{
    public class UiCommand : ContentControl
    {
        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.Register(
            "IconSize", 
            typeof(double), 
            typeof(UiCommand), 
            new PropertyMetadata(16d)
        );

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
            new PropertyMetadata(null)
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

        private Action executed;
        public event Action Executed
        {
            add
            {
                executed += value;

                if (executed != null)
                    Command = new ActionCommand(executed);
            }
            remove
            {
                executed -= value;

                if (executed == null && Command is ActionCommand)
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

        private class ActionCommand : Command
        {
            private readonly Action execute;

            public ActionCommand(Action execute) => this.execute = execute;
            public override bool CanExecute() => true;
            public override void Execute() => execute?.Invoke();
        }
    }
}
