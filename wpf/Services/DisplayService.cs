// <copyright file="DisplayService.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using RemMeter.Models;
using Application = System.Windows.Application;

namespace RemMeter.Services
{
    /// <summary>
    /// A service for retrieving display information.
    /// </summary>
    public class DisplayService : IDisplayService
    {
        /// <inheritdoc/>
        public List<DisplayInfo> GetDisplayInformation()
        {
            var displays = new List<DisplayInfo>();
            var mainWindow = Application.Current.MainWindow;

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                double scaleX = 1.0;
                double scaleY = 1.0;

                if (mainWindow != null)
                {
                    try
                    {
                        var source = PresentationSource.FromVisual(mainWindow);
                        if (source?.CompositionTarget != null)
                        {
                            var matrix = source.CompositionTarget.TransformToDevice;
                            scaleX = matrix.M11;
                            scaleY = matrix.M22;
                        }
                    }
                    catch (Exception)
                    {
                        // Ignore exceptions if DPI cannot be obtained
                    }
                }

                var displayInfo = new DisplayInfo
                {
                    Left = screen.Bounds.Left,
                    Top = screen.Bounds.Top,
                    Width = screen.Bounds.Width,
                    Height = screen.Bounds.Height,
                    ScaleX = scaleX,
                    ScaleY = scaleY,
                    IsPrimary = screen.Primary,
                };

                displays.Add(displayInfo);
            }

            return displays;
        }
    }
}