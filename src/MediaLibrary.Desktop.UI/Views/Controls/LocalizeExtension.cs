using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MediaLibrary.Views.Controls
{
    public class LocalizeExtension : MarkupExtension
    {
        [ConstructorArgument("text")]
        public string Text { get; set; }

        public LocalizeExtension(string text)
        {
            Text = text;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Text;
        }
    }
}
