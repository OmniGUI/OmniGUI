namespace OmniGui.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Zafiro.PropertySystem.Attached;

    public class Grid : Layout
    {
        public static readonly AttachedProperty ColumnProperty = PropertyEngine.RegisterProperty("Column", typeof(Grid),
            typeof(int), new AttachedPropertyMetadata {DefaultValue = 0});

        public static readonly AttachedProperty RowProperty = PropertyEngine.RegisterProperty("Row", typeof(Grid), typeof(int),
            new AttachedPropertyMetadata {DefaultValue = 0});

        public static readonly AttachedProperty ColumnSpanProperty = PropertyEngine.RegisterProperty("ColumnSpan", typeof(Grid),
            typeof(int), new AttachedPropertyMetadata {DefaultValue = 1});

        public static readonly AttachedProperty RowSpanProperty = PropertyEngine.RegisterProperty("RowSpan", typeof(Grid),
            typeof(int), new AttachedPropertyMetadata {DefaultValue = 1});

        private Segment[,] colMatrix;
        private Segment[,] rowMatrix;

        public ColumnDefinitions ColumnDefinitions { get; set; } = new ColumnDefinitions();
        public RowDefinitions RowDefinitions { get; set; } = new RowDefinitions();

        private void CreateMatrices(int rowCount, int colCount)
        {
            if (rowMatrix == null || colMatrix == null ||
                rowMatrix.GetLength(0) != rowCount ||
                colMatrix.GetLength(0) != colCount)
            {
                rowMatrix = new Segment[rowCount, rowCount];
                colMatrix = new Segment[colCount, colCount];
            }
            else
            {
                Array.Clear(rowMatrix, 0, rowMatrix.Length);
                Array.Clear(colMatrix, 0, colMatrix.Length);
            }
        }


        public static int GetColumn(Layout element)
        {
            return (int) element.GetValue(ColumnProperty);
        }

        public static int GetColumnSpan(Layout element)
        {
            return (int) element.GetValue(ColumnSpanProperty);
        }


        public static int GetRow(Layout element)
        {
            return (int) element.GetValue(RowProperty);
        }


        public static int GetRowSpan(Layout element)
        {
            return (int) element.GetValue(RowSpanProperty);
        }

        public static void SetColumn(Layout element, int value)
        {
            element.SetValue(ColumnProperty, value);
        }


        public static void SetColumnSpan(Layout element, int value)
        {
            element.SetValue(ColumnSpanProperty, value);
        }


        public static void SetRow(Layout element, int value)
        {
            element.SetValue(RowProperty, value);
        }


        public static void SetRowSpan(Layout element, int value)
        {
            element.SetValue(RowSpanProperty, value);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var totalSize = constraint;
            var colCount = ColumnDefinitions.Count;
            var rowCount = RowDefinitions.Count;
            double totalStarsX = 0;
            double totalStarsY = 0;
            var emptyRows = rowCount == 0;
            var emptyCols = colCount == 0;
            var hasChildren = Children.Count > 0;

            if (emptyRows)
            {
                rowCount = 1;
            }

            if (emptyCols)
            {
                colCount = 1;
            }

            CreateMatrices(rowCount, colCount);

            if (emptyRows)
            {
                rowMatrix[0, 0] = new Segment(0, 0, double.PositiveInfinity, GridUnitType.Star);
                rowMatrix[0, 0].Stars = 1.0;
                totalStarsY += 1.0;
            }
            else
            {
                for (var i = 0; i < rowCount; i++)
                {
                    var rowdef = RowDefinitions[i];
                    var height = rowdef.Height;

                    rowdef.ActualHeight = double.PositiveInfinity;
                    rowMatrix[i, i] = new Segment(0, rowdef.MinHeight, rowdef.MaxHeight, height.GridUnitType);

                    if (height.GridUnitType == GridUnitType.Pixel)
                    {
                        rowMatrix[i, i].OfferedSize = Clamp(height.Value, rowMatrix[i, i].Min, rowMatrix[i, i].Max);
                        rowMatrix[i, i].DesiredSize = rowMatrix[i, i].OfferedSize;
                        rowdef.ActualHeight = rowMatrix[i, i].OfferedSize;
                    }
                    else if (height.GridUnitType == GridUnitType.Star)
                    {
                        rowMatrix[i, i].Stars = height.Value;
                        totalStarsY += height.Value;
                    }
                    else if (height.GridUnitType == GridUnitType.Auto)
                    {
                        rowMatrix[i, i].OfferedSize = Clamp(0, rowMatrix[i, i].Min, rowMatrix[i, i].Max);
                        rowMatrix[i, i].DesiredSize = rowMatrix[i, i].OfferedSize;
                    }
                }
            }

            if (emptyCols)
            {
                colMatrix[0, 0] = new Segment(0, 0, double.PositiveInfinity, GridUnitType.Star);
                colMatrix[0, 0].Stars = 1.0;
                totalStarsX += 1.0;
            }
            else
            {
                for (var i = 0; i < colCount; i++)
                {
                    var coldef = ColumnDefinitions[i];
                    var width = coldef.Width;

                    coldef.ActualWidth = double.PositiveInfinity;
                    colMatrix[i, i] = new Segment(0, coldef.MinWidth, coldef.MaxWidth, width.GridUnitType);

                    if (width.GridUnitType == GridUnitType.Pixel)
                    {
                        colMatrix[i, i].OfferedSize = Clamp(width.Value, colMatrix[i, i].Min, colMatrix[i, i].Max);
                        colMatrix[i, i].DesiredSize = colMatrix[i, i].OfferedSize;
                        coldef.ActualWidth = colMatrix[i, i].OfferedSize;
                    }
                    else if (width.GridUnitType == GridUnitType.Star)
                    {
                        colMatrix[i, i].Stars = width.Value;
                        totalStarsX += width.Value;
                    }
                    else if (width.GridUnitType == GridUnitType.Auto)
                    {
                        colMatrix[i, i].OfferedSize = Clamp(0, colMatrix[i, i].Min, colMatrix[i, i].Max);
                        colMatrix[i, i].DesiredSize = colMatrix[i, i].OfferedSize;
                    }
                }
            }

            var sizes = new List<GridNode>();
            GridNode node;
            var separator = new GridNode(null, 0, 0, 0);
            int separatorIndex;

            sizes.Add(separator);

            // Pre-process the grid children so that we know what types of elements we have so
            // we can apply our special measuring rules.
            var gridWalker = new GridWalker(this, rowMatrix, colMatrix);

            for (var i = 0; i < 6; i++)
            {
                // These bools tell us which grid element type we should be measuring. i.e.
                // 'star/auto' means we should measure elements with a star row and auto col
                var autoAuto = i == 0;
                var starAuto = i == 1;
                var autoStar = i == 2;
                var starAutoAgain = i == 3;
                var nonStar = i == 4;
                var remainingStar = i == 5;

                if (hasChildren)
                {
                    ExpandStarCols(totalSize);
                    ExpandStarRows(totalSize);
                }

                foreach (var child in Children)
                {
                    int col, row;
                    int colspan, rowspan;
                    double childSizeX = 0;
                    double childSizeY = 0;
                    var starCol = false;
                    var starRow = false;
                    var autoCol = false;
                    var autoRow = false;

                    col = Math.Min(GetColumn(child), colCount - 1);
                    row = Math.Min(GetRow(child), rowCount - 1);
                    colspan = Math.Min(GetColumnSpan(child), colCount - col);
                    rowspan = Math.Min(GetRowSpan(child), rowCount - row);

                    for (var r = row; r < row + rowspan; r++)
                    {
                        starRow |= rowMatrix[r, r].Type == GridUnitType.Star;
                        autoRow |= rowMatrix[r, r].Type == GridUnitType.Auto;
                    }

                    for (var c = col; c < col + colspan; c++)
                    {
                        starCol |= colMatrix[c, c].Type == GridUnitType.Star;
                        autoCol |= colMatrix[c, c].Type == GridUnitType.Auto;
                    }

                    // This series of if statements checks whether or not we should measure
                    // the current element and also if we need to override the sizes
                    // passed to the Measure call.

                    // If the element has Auto rows and Auto columns and does not span Star
                    // rows/cols it should only be measured in the auto_auto phase.
                    // There are similar rules governing auto/star and star/auto elements.
                    // NOTE: star/auto elements are measured twice. The first time with
                    // an override for height, the second time without it.
                    if (autoRow && autoCol && !starRow && !starCol)
                    {
                        if (!autoAuto)
                        {
                            continue;
                        }

                        childSizeX = double.PositiveInfinity;
                        childSizeY = double.PositiveInfinity;
                    }
                    else if (starRow && autoCol && !starCol)
                    {
                        if (!(starAuto || starAutoAgain))
                        {
                            continue;
                        }

                        if (starAuto && gridWalker.HasAutoStar)
                        {
                            childSizeY = double.PositiveInfinity;
                        }

                        childSizeX = double.PositiveInfinity;
                    }
                    else if (autoRow && starCol && !starRow)
                    {
                        if (!autoStar)
                        {
                            continue;
                        }

                        childSizeY = double.PositiveInfinity;
                    }
                    else if ((autoRow || autoCol) && !(starRow || starCol))
                    {
                        if (!nonStar)
                        {
                            continue;
                        }

                        if (autoRow)
                        {
                            childSizeY = double.PositiveInfinity;
                        }

                        if (autoCol)
                        {
                            childSizeX = double.PositiveInfinity;
                        }
                    }
                    else if (!(starRow || starCol))
                    {
                        if (!nonStar)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!remainingStar)
                        {
                            continue;
                        }
                    }

                    for (var r = row; r < row + rowspan; r++)
                    {
                        childSizeY += rowMatrix[r, r].OfferedSize;
                    }

                    for (var c = col; c < col + colspan; c++)
                    {
                        childSizeX += colMatrix[c, c].OfferedSize;
                    }

                    child.Measure(new Size(childSizeX, childSizeY));
                    var desired = child.DesiredSize;

                    // Elements distribute their height based on two rules:
                    // 1) Elements with rowspan/colspan == 1 distribute their height first
                    // 2) Everything else distributes in a LIFO manner.
                    // As such, add all UIElements with rowspan/colspan == 1 after the separator in
                    // the list and everything else before it. Then to process, just keep popping
                    // elements off the end of the list.
                    if (!starAuto)
                    {
                        node = new GridNode(rowMatrix, row + rowspan - 1, row, desired.Height);
                        separatorIndex = sizes.IndexOf(separator);
                        sizes.Insert(node.Row == node.Column ? separatorIndex + 1 : separatorIndex, node);
                    }

                    node = new GridNode(colMatrix, col + colspan - 1, col, desired.Width);

                    separatorIndex = sizes.IndexOf(separator);
                    sizes.Insert(node.Row == node.Column ? separatorIndex + 1 : separatorIndex, node);
                }

                sizes.Remove(separator);

                while (sizes.Count > 0)
                {
                    node = sizes.Last();
                    node.Matrix[node.Row, node.Column].DesiredSize =
                        Math.Max(node.Matrix[node.Row, node.Column].DesiredSize, node.Size);
                    AllocateDesiredSize(rowCount, colCount);
                    sizes.Remove(node);
                }

                sizes.Add(separator);
            }

            // Once we have measured and distributed all sizes, we have to store
            // the results. Every time we want to expand the rows/cols, this will
            // be used as the baseline.
            SaveMeasureResults();

            sizes.Remove(separator);

            double gridSizeX = 0;
            double gridSizeY = 0;

            for (var c = 0; c < colCount; c++)
            {
                gridSizeX += colMatrix[c, c].DesiredSize;
            }

            for (var r = 0; r < rowCount; r++)
            {
                gridSizeY += rowMatrix[r, r].DesiredSize;
            }

            return new Size(gridSizeX, gridSizeY);
        }

        private void RestoreMeasureResults()
        {
            var rowMatrixDim = rowMatrix.GetLength(0);
            var colMatrixDim = colMatrix.GetLength(0);

            for (var i = 0; i < rowMatrixDim; i++)
            for (var j = 0; j < rowMatrixDim; j++)
            {
                rowMatrix[i, j].OfferedSize = rowMatrix[i, j].OriginalSize;
            }

            for (var i = 0; i < colMatrixDim; i++)
            for (var j = 0; j < colMatrixDim; j++)
            {
                colMatrix[i, j].OfferedSize = colMatrix[i, j].OriginalSize;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var colCount = ColumnDefinitions.Count;
            var rowCount = RowDefinitions.Count;
            var colMatrixDim = colMatrix.GetLength(0);
            var rowMatrixDim = rowMatrix.GetLength(0);

            RestoreMeasureResults();

            double totalConsumedX = 0;
            double totalConsumedY = 0;

            for (var c = 0; c < colMatrixDim; c++)
            {
                colMatrix[c, c].OfferedSize = colMatrix[c, c].DesiredSize;
                totalConsumedX += colMatrix[c, c].OfferedSize;
            }

            for (var r = 0; r < rowMatrixDim; r++)
            {
                rowMatrix[r, r].OfferedSize = rowMatrix[r, r].DesiredSize;
                totalConsumedY += rowMatrix[r, r].OfferedSize;
            }

            if (totalConsumedX != finalSize.Width)
            {
                ExpandStarCols(finalSize);
            }

            if (totalConsumedY != finalSize.Height)
            {
                ExpandStarRows(finalSize);
            }

            for (var c = 0; c < colCount; c++)
            {
                ColumnDefinitions[c].ActualWidth = colMatrix[c, c].OfferedSize;
            }

            for (var r = 0; r < rowCount; r++)
            {
                RowDefinitions[r].ActualHeight = rowMatrix[r, r].OfferedSize;
            }

            foreach (var child in Children)
            {
                var col = Math.Min(GetColumn(child), colMatrixDim - 1);
                var row = Math.Min(GetRow(child), rowMatrixDim - 1);
                var colspan = Math.Min(GetColumnSpan(child), colMatrixDim - col);
                var rowspan = Math.Min(GetRowSpan(child), rowMatrixDim - row);

                double childFinalX = 0;
                double childFinalY = 0;
                double childFinalW = 0;
                double childFinalH = 0;

                for (var c = 0; c < col; c++)
                {
                    childFinalX += colMatrix[c, c].OfferedSize;
                }

                for (var c = col; c < col + colspan; c++)
                {
                    childFinalW += colMatrix[c, c].OfferedSize;
                }

                for (var r = 0; r < row; r++)
                {
                    childFinalY += rowMatrix[r, r].OfferedSize;
                }

                for (var r = row; r < row + rowspan; r++)
                {
                    childFinalH += rowMatrix[r, r].OfferedSize;
                }

                child.Arrange(new Rect(childFinalX, childFinalY, childFinalW, childFinalH));
            }

            return finalSize;
        }

        private void AllocateDesiredSize(int rowCount, int colCount)
        {
            // First allocate the heights of the RowDefinitions, then allocate
            // the widths of the ColumnDefinitions.
            for (var i = 0; i < 2; i++)
            {
                var matrix = i == 0 ? rowMatrix : colMatrix;
                var count = i == 0 ? rowCount : colCount;

                for (var row = count - 1; row >= 0; row--)
                for (var col = row; col >= 0; col--)
                {
                    var spansStar = false;
                    for (var j = row; j >= col; j--)
                    {
                        spansStar |= matrix[j, j].Type == GridUnitType.Star;
                    }

                    // This is the amount of pixels which must be available between the grid rows
                    // at index 'col' and 'row'. i.e. if 'row' == 0 and 'col' == 2, there must
                    // be at least 'matrix [row][col].size' pixels of height allocated between
                    // all the rows in the range col -> row.
                    var current = matrix[row, col].DesiredSize;

                    // Count how many pixels have already been allocated between the grid rows
                    // in the range col -> row. The amount of pixels allocated to each grid row/column
                    // is found on the diagonal of the matrix.
                    double totalAllocated = 0;

                    for (var k = row; k >= col; k--)
                    {
                        totalAllocated += matrix[k, k].DesiredSize;
                    }

                    // If the size requirement has not been met, allocate the additional required
                    // size between 'pixel' rows, then 'star' rows, finally 'auto' rows, until all
                    // height has been assigned.
                    if (totalAllocated < current)
                    {
                        var additional = current - totalAllocated;

                        if (spansStar)
                        {
                            AssignSize(matrix, col, row, ref additional, GridUnitType.Star, true);
                        }
                        else
                        {
                            AssignSize(matrix, col, row, ref additional, GridUnitType.Pixel, true);
                            AssignSize(matrix, col, row, ref additional, GridUnitType.Auto, true);
                        }
                    }
                }
            }

            var rowMatrixDim = rowMatrix.GetLength(0);
            var colMatrixDim = colMatrix.GetLength(0);

            for (var r = 0; r < rowMatrixDim; r++)
            {
                rowMatrix[r, r].OfferedSize = rowMatrix[r, r].DesiredSize;
            }

            for (var c = 0; c < colMatrixDim; c++)
            {
                colMatrix[c, c].OfferedSize = colMatrix[c, c].DesiredSize;
            }
        }

        private void SaveMeasureResults()
        {
            var rowMatrixDim = rowMatrix.GetLength(0);
            var colMatrixDim = colMatrix.GetLength(0);

            for (var i = 0; i < rowMatrixDim; i++)
            for (var j = 0; j < rowMatrixDim; j++)
            {
                rowMatrix[i, j].OriginalSize = rowMatrix[i, j].OfferedSize;
            }

            for (var i = 0; i < colMatrixDim; i++)
            for (var j = 0; j < colMatrixDim; j++)
            {
                colMatrix[i, j].OriginalSize = colMatrix[i, j].OfferedSize;
            }
        }

        private static double Clamp(double val, double min, double max)
        {
            if (val < min)
            {
                return min;
            }
            if (val > max)
            {
                return max;
            }
            return val;
        }

        private void AssignSize(
            Segment[,] matrix,
            int start,
            int end,
            ref double size,
            GridUnitType type,
            bool desiredSize)
        {
            double count = 0;
            bool assigned;

            // Count how many segments are of the correct type. If we're measuring Star rows/cols
            // we need to count the number of stars instead.
            for (var i = start; i <= end; i++)
            {
                var segmentSize = desiredSize ? matrix[i, i].DesiredSize : matrix[i, i].OfferedSize;
                if (segmentSize < matrix[i, i].Max)
                {
                    count += type == GridUnitType.Star ? matrix[i, i].Stars : 1;
                }
            }

            do
            {
                var contribution = size / count;

                assigned = false;

                for (var i = start; i <= end; i++)
                {
                    var segmentSize = desiredSize ? matrix[i, i].DesiredSize : matrix[i, i].OfferedSize;

                    if (!(matrix[i, i].Type == type && segmentSize < matrix[i, i].Max))
                    {
                        continue;
                    }

                    var newsize = segmentSize;
                    newsize += contribution * (type == GridUnitType.Star ? matrix[i, i].Stars : 1);
                    newsize = Math.Min(newsize, matrix[i, i].Max);
                    assigned |= newsize > segmentSize;
                    size -= newsize - segmentSize;

                    if (desiredSize)
                    {
                        matrix[i, i].DesiredSize = newsize;
                    }
                    else
                    {
                        matrix[i, i].OfferedSize = newsize;
                    }
                }
            } while (assigned);
        }

        private void ExpandStarCols(Size availableSize)
        {
            var matrixCount = colMatrix.GetLength(0);
            var columnsCount = ColumnDefinitions.Count;
            var width = availableSize.Width;

            for (var i = 0; i < matrixCount; i++)
            {
                if (colMatrix[i, i].Type == GridUnitType.Star)
                {
                    colMatrix[i, i].OfferedSize = 0;
                }
                else
                {
                    width = Math.Max(width - colMatrix[i, i].OfferedSize, 0);
                }
            }

            AssignSize(colMatrix, 0, matrixCount - 1, ref width, GridUnitType.Star, false);
            width = Math.Max(0, width);

            if (columnsCount > 0)
            {
                for (var i = 0; i < matrixCount; i++)
                {
                    if (colMatrix[i, i].Type == GridUnitType.Star)
                    {
                        ColumnDefinitions[i].ActualWidth = colMatrix[i, i].OfferedSize;
                    }
                }
            }
        }

        private void ExpandStarRows(Size availableSize)
        {
            var matrixCount = rowMatrix.GetLength(0);
            var rowCount = RowDefinitions.Count;
            var height = availableSize.Height;

            // When expanding star rows, we need to zero out their height before
            // calling AssignSize. AssignSize takes care of distributing the
            // available size when there are Mins and Maxs applied.
            for (var i = 0; i < matrixCount; i++)
            {
                if (rowMatrix[i, i].Type == GridUnitType.Star)
                {
                    rowMatrix[i, i].OfferedSize = 0.0;
                }
                else
                {
                    height = Math.Max(height - rowMatrix[i, i].OfferedSize, 0);
                }
            }

            AssignSize(rowMatrix, 0, matrixCount - 1, ref height, GridUnitType.Star, false);

            if (rowCount > 0)
            {
                for (var i = 0; i < matrixCount; i++)
                {
                    if (rowMatrix[i, i].Type == GridUnitType.Star)
                    {
                        RowDefinitions[i].ActualHeight = rowMatrix[i, i].OfferedSize;
                    }
                }
            }
        }

        public override void Render(IDrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(VisualBounds, Background, null);
            foreach (var child in Children)
            {
                child.Render(drawingContext);
            }
        }

        private struct Segment
        {
            public double OriginalSize;
            public readonly double Max;
            public readonly double Min;
            public double DesiredSize;
            public double OfferedSize;
            public double Stars;
            public readonly GridUnitType Type;

            public Segment(double offeredSize, double min, double max, GridUnitType type)
            {
                OriginalSize = 0;
                Min = min;
                Max = max;
                DesiredSize = 0;
                OfferedSize = offeredSize;
                Stars = 0;
                Type = type;
            }
        }

        private struct GridNode
        {
            public readonly int Row;
            public readonly int Column;
            public readonly double Size;
            public readonly Segment[,] Matrix;

            public GridNode(Segment[,] matrix, int row, int col, double size)
            {
                Matrix = matrix;
                Row = row;
                Column = col;
                Size = size;
            }
        }

        private class GridWalker
        {
            public GridWalker(Grid grid, Segment[,] rowMatrix, Segment[,] colMatrix)
            {
                var rowMatrixDim = rowMatrix.GetLength(0);
                var colMatrixDim = colMatrix.GetLength(0);

                foreach (var child in grid.Children)
                {
                    var starCol = false;
                    var starRow = false;
                    var autoCol = false;
                    var autoRow = false;

                    var col = Math.Min(GetColumn(child), colMatrixDim - 1);
                    var row = Math.Min(GetRow(child), rowMatrixDim - 1);
                    var colspan = Math.Min(GetColumnSpan(child), colMatrixDim - 1);
                    var rowspan = Math.Min(GetRowSpan(child), rowMatrixDim - 1);

                    for (var r = row; r < row + rowspan; r++)
                    {
                        starRow |= rowMatrix[r, r].Type == GridUnitType.Star;
                        autoRow |= rowMatrix[r, r].Type == GridUnitType.Auto;
                    }

                    for (var c = col; c < col + colspan; c++)
                    {
                        starCol |= colMatrix[c, c].Type == GridUnitType.Star;
                        autoCol |= colMatrix[c, c].Type == GridUnitType.Auto;
                    }

                    HasAutoAuto |= autoRow && autoCol && !starRow && !starCol;
                    HasStarAuto |= starRow && autoCol;
                    HasAutoStar |= autoRow && starCol;
                }
            }

            public bool HasAutoAuto { get; }

            public bool HasStarAuto { get; }

            public bool HasAutoStar { get; }
        }
    }
}