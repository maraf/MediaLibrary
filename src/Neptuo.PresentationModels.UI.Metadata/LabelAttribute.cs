using Neptuo.PresentationModels.TypeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LabelAttribute : Attribute, IMetadataReader
    {
        public string Label { get; set; }

        public LabelAttribute(string label)
        {
            Label = label;
        }

        public void Apply(IMetadataBuilder builder)
        {
            builder.AddLabel(Label);
        }
    }
}
