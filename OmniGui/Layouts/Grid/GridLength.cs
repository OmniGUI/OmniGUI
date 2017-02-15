namespace OmniGui.Layouts.Grid
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public struct GridLength : IEquatable<GridLength>
    {
        private readonly GridUnitType type;

        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridLength"/> struct.
        /// </summary>
        /// <param name="value">The size of the GridLength.</param>
        /// <param name="type">The unit of the GridLength.</param>
        public GridLength(double value, GridUnitType type = GridUnitType.Pixel)
        {
            if (value < 0 || double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid value", nameof(value));
            }

            if (type < GridUnitType.Auto || type > GridUnitType.Star)
            {
                throw new ArgumentException("Invalid value", nameof(type));
            }

            this.type = type;
            this.value = value;
        }

        /// <summary>
        /// Gets an instance of <see cref="GridLength"/> that indicates that a row or column should
        /// auto-size to fit its content.
        /// </summary>
        public static GridLength Auto => new GridLength(0, GridUnitType.Auto);

        /// <summary>
        /// Gets the unit of the <see cref="GridLength"/>.
        /// </summary>
        public GridUnitType GridUnitType => type;

        /// <summary>
        /// Gets a value that indicates whether the <see cref="GridLength"/> has a <see cref="GridUnitType"/> of Pixel.
        /// </summary>
        public bool IsAbsolute => type == GridUnitType.Pixel;

        /// <summary>
        /// Gets a value that indicates whether the <see cref="GridLength"/> has a <see cref="GridUnitType"/> of Auto.
        /// </summary>
        public bool IsAuto => type == GridUnitType.Auto;

        /// <summary>
        /// Gets a value that indicates whether the <see cref="GridLength"/> has a <see cref="GridUnitType"/> of Star.
        /// </summary>
        public bool IsStar => type == GridUnitType.Star;

        /// <summary>
        /// Gets the length.
        /// </summary>
        public double Value => value;

        /// <summary>
        /// Compares two GridLength structures for equality.
        /// </summary>
        /// <param name="a">The first GridLength.</param>
        /// <param name="b">The second GridLength.</param>
        /// <returns>True if the structures are equal, otherwise false.</returns>
        public static bool operator ==(GridLength a, GridLength b)
        {
            return (a.IsAuto && b.IsAuto) || (a.value == b.value && a.type == b.type);
        }

        /// <summary>
        /// Compares two GridLength structures for inequality.
        /// </summary>
        /// <param name="gl1">The first GridLength.</param>
        /// <param name="gl2">The first GridLength.</param>
        /// <returns>True if the structures are unequal, otherwise false.</returns>
        public static bool operator !=(GridLength gl1, GridLength gl2)
        {
            return !(gl1 == gl2);
        }

        /// <summary>
        /// Determines whether the <see cref="GridLength"/> is equal to the specified object.
        /// </summary>
        /// <param name="o">The object with which to test equality.</param>
        /// <returns>True if the objects are equal, otherwise false.</returns>
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }

            if (!(o is GridLength))
            {
                return false;
            }

            return this == (GridLength)o;
        }

        /// <summary>
        /// Compares two GridLength structures for equality.
        /// </summary>
        /// <param name="gridLength">The structure with which to test equality.</param>
        /// <returns>True if the structures are equal, otherwise false.</returns>
        public bool Equals(GridLength gridLength)
        {
            return this == gridLength;
        }

        /// <summary>
        /// Gets a hash code for the GridLength.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode() ^ type.GetHashCode();
        }

        /// <summary>
        /// Gets a string representation of the <see cref="GridLength"/>.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            if (IsAuto)
            {
                return "Auto";
            }

            string s = value.ToString();
            return IsStar ? s + "*" : s;
        }

        /// <summary>
        /// Parses a string to return a <see cref="GridLength"/>.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="culture">The current culture.</param>
        /// <returns>The <see cref="GridLength"/>.</returns>
        public static GridLength Parse(string s, CultureInfo culture)
        {
            s = s.ToUpperInvariant();

            if (s == "AUTO")
            {
                return Auto;
            }
            else if (s.EndsWith("*"))
            {
                var valueString = s.Substring(0, s.Length - 1).Trim();
                var value = valueString.Length > 0 ? double.Parse(valueString, culture) : 1;
                return new GridLength(value, GridUnitType.Star);
            }
            else
            {
                var value = double.Parse(s, culture);
                return new GridLength(value, GridUnitType.Pixel);
            }
        }

        /// <summary>
        /// Parses a string to return a collection of <see cref="GridLength"/>s.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="culture">The current culture.</param>
        /// <returns>The <see cref="GridLength"/>.</returns>
        public static IEnumerable<GridLength> ParseLengths(string s, CultureInfo culture)
        {
            return s.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Parse(x, culture));
        }
    }
}