// <copyright file="EnumToBooleanConverter.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System;
using System.Globalization;
using System.Windows.Data;
using Binding = System.Windows.Data.Binding;

namespace RemMeter.Converters
{
    /// <summary>
    /// Converts an enum value to a boolean, returning true if the enum matches a specified parameter.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            string checkValue = value.ToString() ?? string.Empty;
            string targetValue = parameter.ToString() ?? string.Empty;
            return checkValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return Binding.DoNothing;
            }

            bool useValue = (bool)value;
            if (useValue)
            {
                return Enum.Parse(targetType, parameter.ToString() ?? string.Empty);
            }

            return Binding.DoNothing;
        }
    }
}