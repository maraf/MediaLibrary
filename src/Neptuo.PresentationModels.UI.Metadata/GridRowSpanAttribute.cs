using Neptuo.PresentationModels.TypeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridRowSpanAttribute : Attribute, IMetadataReader
    {
        public int Span { get; set; }

        public GridRowSpanAttribute(int span)
        {
            Ensure.Positive(span, "span");
            Span = span;
        }

        public void Apply(IMetadataBuilder builder)
        {
            builder.AddGridRowSpan(Span);
        }
    }
}
