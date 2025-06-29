// <copyright file="IDisplayService.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System.Collections.Generic;
using RemMeter.Models;

namespace RemMeter.Services
{
    /// <summary>
    /// Defines a service for retrieving display information.
    /// </summary>
    public interface IDisplayService
    {
        /// <summary>
        /// Gets information about all available displays.
        /// </summary>
        /// <returns>A list of display information.</returns>
        List<DisplayInfo> GetDisplayInformation();
    }
}