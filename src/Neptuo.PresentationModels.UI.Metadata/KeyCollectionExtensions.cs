using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    public static class KeyCollectionExtensions
    {
        public static IKeyValueCollection AddLabel(this IKeyValueCollection metadata, string label)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.Add("Label", label);
        }

        public static IKeyValueCollection AddIsReadOnly(this IKeyValueCollection metadata, bool isReadOnly)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.Add("IsReadOnly", isReadOnly);
        }

        public static IKeyValueCollection AddIsAutoFocus(this IKeyValueCollection metadata, bool isAutoFocus)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.Add("IsAutoFocus", isAutoFocus);
        }

        #region Grid

        public static IKeyValueCollection AddGrid(this IKeyValueCollection metadata, int column, int row)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata
                .Add("Grid.Column", column)
                .Add("Grid.Row", row);
        }

        public static IKeyValueCollection AddGridSpan(this IKeyValueCollection metadata, int column, int row)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata
                .AddGridColumnSpan(column)
                .AddGridRowSpan(row);
        }

        public static IKeyValueCollection AddGridColumnSpan(this IKeyValueCollection metadata, int column)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.Add("Grid.ColumnSpan", column);
        }

        public static IKeyValueCollection AddGridRowSpan(this IKeyValueCollection metadata, int row)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.Add("Grid.RowSpan", row);
        }

        #endregion
    }
}
