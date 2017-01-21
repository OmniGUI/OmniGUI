// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace OmniGui.Grid
{
    /// <summary>
    ///     Holds a row definitions for a <see cref="Grid" />.
    /// </summary>
    public class RowDefinition : DefinitionBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RowDefinition" /> class.
        /// </summary>
        public RowDefinition()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RowDefinition" /> class.
        /// </summary>
        /// <param name="value">The height of the row.</param>
        /// <param name="type">The height unit of the column.</param>
        public RowDefinition(double value, GridUnitType type)
        {
            Height = new GridLength(value, type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RowDefinition" /> class.
        /// </summary>
        /// <param name="height">The height of the column.</param>
        public RowDefinition(GridLength height)
        {
            Height = height;
        }

        /// <summary>
        ///     Gets the actual calculated height of the row.
        /// </summary>
        public double ActualHeight { get; internal set; }

        /// <summary>
        ///     Gets or sets the maximum height of the row in DIPs.
        /// </summary>
        public double MaxHeight { get; set; } = double.PositiveInfinity;

        /// <summary>
        ///     Gets or sets the minimum height of the row in DIPs.
        /// </summary>
        public double MinHeight { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the height of the row.
        /// </summary>
        public GridLength Height { get; set; } = new GridLength(1, GridUnitType.Star);
    }
}