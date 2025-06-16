// <copyright file="VersionHelper.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System.Reflection;

namespace RemMeter.Helpers
{
    /// <summary>
    /// Helper class for version information.
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// Gets the application version string.
        /// </summary>
        /// <returns>The version string.</returns>
        public static string GetVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version;
            return version != null ? $"v{version.Major}.{version.Minor}.{version.Build}" : "v1.0.0";
        }

        /// <summary>
        /// Gets the full application title with version.
        /// </summary>
        /// <returns>The application title with version.</returns>
        public static string GetTitleWithVersion()
        {
            return $"{Properties.Resources.AppTitle} {GetVersion()}";
        }
    }
}