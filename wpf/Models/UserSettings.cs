// <copyright file="UserSettings.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

namespace RemMeter.Models
{
    /// <summary>
    /// Represents the user's saved settings for the application.
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// Gets or sets the last used timer duration.
        /// </summary>
        public string LastTimerDuration { get; set; } = "10:00";

        /// <summary>
        /// Gets or sets the last selected timer position.
        /// </summary>
        public TimerPosition LastSelectedPosition { get; set; } = TimerPosition.Right;

        /// <summary>
        /// Gets or sets the index of the last selected display.
        /// </summary>
        public int LastSelectedDisplayIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember the user's last settings.
        /// </summary>
        public bool RememberLastSettings { get; set; } = true;
    }
}