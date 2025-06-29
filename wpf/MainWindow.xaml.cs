// <copyright file="MainWindow.xaml.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using RemMeter.Helpers;
using RemMeter.Models;
using RemMeter.Services;
using RemMeter.Validation;
using RemMeter.ViewModels;

namespace RemMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            Logger.Info("MainWindow constructor started");

            this.InitializeComponent();
            var displayService = new DisplayService();
            var settingsService = new SettingsService();
            var viewModel = new MainWindowViewModel(displayService, settingsService);
            this.DataContext = viewModel;

            Logger.Debug("InitializeComponent completed");

            // Set window title with version
            this.Title = VersionHelper.GetTitleWithVersion();

            // Save settings when window is closing
            this.Closing += (s, e) =>
            {
                viewModel.SaveSettings();
            };

            Logger.Info("MainWindow constructor completed successfully");
        }
    }
}
