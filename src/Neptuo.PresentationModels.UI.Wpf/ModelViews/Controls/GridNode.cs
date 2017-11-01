using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Neptuo.PresentationModels.UI.ModelViews.Controls
{
    public class GridNode : StackPanel
    {
        public int Column
        {
            get => Grid.GetColumn(this);
            set => Grid.SetColumn(this, value);
        }

        public int Row
        {
            get => Grid.GetRow(this);
            set => Grid.SetRow(this, value);
        }

        public int ColumnSpan
        {
            get => Grid.GetColumnSpan(this);
            set => Grid.SetColumnSpan(this, value);
        }

        public int RowSpan
        {
            get => Grid.GetRowSpan(this);
            set => Grid.SetRowSpan(this, value);
        }
    }
}
