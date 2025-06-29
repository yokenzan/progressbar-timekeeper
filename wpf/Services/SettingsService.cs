// <copyright file="SettingsService.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System;
using RemMeter.Helpers;
using RemMeter.Models;

namespace RemMeter.Services
{
    /// <summary>
    /// A service for managing user settings.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <inheritdoc/>
        public UserSettings LoadSettings()
        {
            var settings = new UserSettings();
            try
            {
                var appSettings = Properties.Settings.Default;
                settings.RememberLastSettings = appSettings.RememberLastSettings;

                if (settings.RememberLastSettings)
                {
                    settings.LastTimerDuration = appSettings.LastTimerDuration;
                    settings.LastSelectedPosition = PositionMapper.ParsePosition(appSettings.LastSelectedPosition);
                    settings.LastSelectedDisplayIndex = appSettings.LastSelectedDisplayIndex;
                }
            }
            catch (Exception)
            {
                // Return default settings if loading fails
                return new UserSettings();
            }

            return settings;
        }

        /// <inheritdoc/>
        public void SaveSettings(UserSettings settings)
        {
            try
            {
                var appSettings = Properties.Settings.Default;
                appSettings.RememberLastSettings = settings.RememberLastSettings;

                if (settings.RememberLastSettings)
                {
                    appSettings.LastTimerDuration = settings.LastTimerDuration;
                    appSettings.LastSelectedPosition = settings.LastSelectedPosition.ToString();
                    appSettings.LastSelectedDisplayIndex = settings.LastSelectedDisplayIndex;
                }

                appSettings.Save();
            }
            catch (Exception)
            {
                // Silently fail on save error
            }
        }
    }
}