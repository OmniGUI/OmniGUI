// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.


namespace OmniGui.Grid
{
    /// <summary>
    /// Holds a column definitions for a <see cref="Grid"/>.
    /// </summary>
    public class ColumnDefinition : DefinitionBase
    {      
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class.
        /// </summary>
        public ColumnDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class.
        /// </summary>
        /// <param name="value">The width of the column.</param>
        /// <param name="type">The width unit of the column.</param>
        public ColumnDefinition(double value, GridUnitType type)
        {
            Width = new GridLength(value, type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition"/> class.
        /// </summary>
        /// <param name="width">The width of the column.</param>
        public ColumnDefinition(GridLength width)
        {
            Width = width;
        }

        /// <summary>
        /// Gets the actual calculated width of the column.
        /// </summary>
        public double ActualWidth
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the maximum width of the column in DIPs.
        /// </summary>
        public double MaxWidth
        { get; set; }

        /// <summary>
        /// Gets or sets the minimum width of the column in DIPs.
        /// </summary>
        public double MinWidth
        { get; set; }

        /// <summary>
        /// Gets or sets the width of the column.
        /// </summary>
        public GridLength Width { get; set; }

        public double ActualHeight { get; set; }
        public GridLength Height { get; set; }
        public double MinHeight { get; set; }
        public double MaxHeight { get; set; }
    }
}