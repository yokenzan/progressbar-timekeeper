// <copyright file="ISettingsService.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using RemMeter.Models;

namespace RemMeter.Services
{
    /// <summary>
    /// Defines a service for managing user settings.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Loads the user settings from the persistent storage.
        /// </summary>
        /// <returns>The loaded user settings.</returns>
        UserSettings LoadSettings();

        /// <summary>
        /// Saves the user settings to the persistent storage.
        /// </summary>
        /// <param name="settings">The user settings to save.</param>
        void SaveSettings(UserSettings settings);
    }
}