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
        #region Label

        public static IMetadataBuilder AddLabel(this IMetadataBuilder builder, string value)
        {
            return builder.Add("Label", value);
        }

        public static bool TryGetLabel(this IReadOnlyKeyValueCollection metadata, out string value)
        {
            return metadata.TryGet("Label", out value);
        }

        public static string GetLabel(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<string>("Label");
        }

        public static string GetLabel(this IReadOnlyKeyValueCollection metadata, string defaultValue)
        {
            return metadata.Get("Label", defaultValue);
        }

        #endregion

        #region IsReadOnly

        public static IMetadataBuilder AddIsReadOnly(this IMetadataBuilder builder, bool value)
        {
            return builder.Add("IsReadOnly", value);
        }

        public static bool TryGetIsReadOnly(this IReadOnlyKeyValueCollection metadata, out bool value)
        {
            return metadata.TryGet("IsReadOnly", out value);
        }

        public static bool GetIsReadOnly(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<bool>("IsReadOnly");
        }

        public static bool GetIsReadOnly(this IReadOnlyKeyValueCollection metadata, bool defaultValue)
        {
            return metadata.Get("IsReadOnly", defaultValue);
        }

        #endregion

        #region IsAutoFocus

        public static IMetadataBuilder AddIsAutoFocus(this IMetadataBuilder builder, bool value)
        {
            return builder.Add("IsAutoFocus", value);
        }

        public static bool TryGetIsAutoFocus(this IReadOnlyKeyValueCollection metadata, out bool value)
        {
            return metadata.TryGet("IsAutoFocus", out value);
        }

        public static bool GetIsAutoFocus(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<bool>("IsAutoFocus");
        }

        public static bool GetIsAutoFocus(this IReadOnlyKeyValueCollection metadata, bool defaultValue)
        {
            return metadata.Get("IsAutoFocus", defaultValue);
        }

        #endregion

        #region Grid

        public static IMetadataBuilder AddGrid(this IMetadataBuilder builder, int column, int row)
        {
            Ensure.NotNull(builder, "builder");
            return builder
                .AddGridColumn(column)
                .AddGridRow(row);
        }

        public static IMetadataBuilder AddGridSpan(this IMetadataBuilder builder, int column, int row)
        {
            Ensure.NotNull(builder, "builder");
            return builder
                .AddGridColumnSpan(column)
                .AddGridRowSpan(row);
        }

        public static IMetadataBuilder AddGridColumn(this IMetadataBuilder builder, int value)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add("Grid.Column", value);
        }

        public static bool TryGetGridColumn(this IReadOnlyKeyValueCollection metadata, out int value)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.TryGet("Grid.Column", out value);
        }

        public static int GetGridColumn(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int>("Grid.Column");
        }

        public static int GetGridColumn(this IReadOnlyKeyValueCollection metadata, int defaultValue)
        {
            return metadata.Get("Grid.Column", defaultValue);
        }

        public static IMetadataBuilder AddGridRow(this IMetadataBuilder builder, int value)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add("Grid.Row", value);
        }

        public static bool TryGetGridRow(this IReadOnlyKeyValueCollection metadata, out int value)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.TryGet("Grid.Row", out value);
        }

        public static int GetGridRow(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int>("Grid.Row");
        }

        public static int GetGridRow(this IReadOnlyKeyValueCollection metadata, int defaultValue)
        {
            return metadata.Get("Grid.Row", defaultValue);
        }

        public static IMetadataBuilder AddGridColumnSpan(this IMetadataBuilder builder, int value)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add("Grid.ColumnSpan", value);
        }

        public static bool TryGetGridColumnSpan(this IReadOnlyKeyValueCollection metadata, out int value)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.TryGet("Grid.ColumnSpan", out value);
        }

        public static int GetGridColumnSpan(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int>("Grid.ColumnSpan");
        }

        public static int GetGridColumnSpan(this IReadOnlyKeyValueCollection metadata, int defaultValue)
        {
            return metadata.Get("Grid.ColumnSpan", defaultValue);
        }

        public static IMetadataBuilder AddGridRowSpan(this IMetadataBuilder builder, int value)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add("Grid.RowSpan", value);
        }

        public static bool TryGetGridRowSpan(this IReadOnlyKeyValueCollection metadata, out int value)
        {
            Ensure.NotNull(metadata, "metadata");
            return metadata.TryGet("Grid.RowSpan", out value);
        }

        public static int GetGridRowSpan(this IReadOnlyKeyValueCollection metadata)
        {
            return metadata.Get<int>("Grid.RowSpan");
        }

        public static int GetGridRowSpan(this IReadOnlyKeyValueCollection metadata, int defaultValue)
        {
            return metadata.Get("Grid.RowSpan", defaultValue);
        }

        #endregion
    }
}
