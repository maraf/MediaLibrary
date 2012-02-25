using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MediaLibrary.Export
{
    public class ExportData
    {
        public string FileName { get; set; }
        public int ColumnsCount { get; set; }
        public List<string> Columns { get; set; }
        public bool OpenAfterExport { get; set; }
    }
}
