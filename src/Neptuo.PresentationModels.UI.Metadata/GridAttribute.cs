using Neptuo.PresentationModels.TypeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridAttribute : Attribute, IMetadataReader
    {
        private int? column;
        private int? row;

        public int Column
        {
            get { return column ?? 0; }
            set { column = value; }
        }

        public int Row
        {
            get { return row ?? 0; }
            set { row = value; }
        }

        public void Apply(IMetadataBuilder builder)
        {
            if (column != null)
                builder.AddGridColumn(column.Value);

            if (row != null)
                builder.AddGridRow(row.Value);
        }
    }
}
