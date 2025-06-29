// <copyright file="MainWindowViewModel.cs" company="RemMeter">
// Copyright (c) 2025 RemMeter. Licensed under the MIT License.
// </copyright>

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using RemMeter.Commands;
using RemMeter.Models;
using RemMeter.Services;
using RemMeter.Validation;

namespace RemMeter.ViewModels
{
    /// <summary>
    /// The ViewModel for the <see cref="MainWindow"/>.
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IDisplayService displayService;
        private readonly ISettingsService settingsService;
        private DisplayInfo? selectedDisplay;
        private string timeInput = "10:00";
        private TimerPosition selectedPosition = TimerPosition.Right;
        private bool rememberLastSettings = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="displayService">The display service to use.</param>
        /// <param name="settingsService">The settings service to use.</param>
        public MainWindowViewModel(IDisplayService displayService, ISettingsService settingsService)
        {
            this.displayService = displayService;
            this.settingsService = settingsService;
            this.Displays = new ObservableCollection<DisplayInfo>();
            this.QuickTimeLabels = new List<string> { "1", "5", "10", "15", "30", "60" };

            this.StartTimerCommand = new RelayCommand(this.StartTimer, this.CanStartTimer);
            this.SetQuickTimeCommand = new RelayCommand(this.SetQuickTime);

            this.LoadDisplays();
            this.LoadSettings();
        }

        /// <summary>
        /// Gets the command to start the timer.
        /// </summary>
        public ICommand StartTimerCommand { get; }

        /// <summary>
        /// Gets the command to set a quick time.
        /// </summary>
        public ICommand SetQuickTimeCommand { get; }

        /// <summary>
        /// Gets the labels for the quick time buttons.
        /// </summary>
        public List<string> QuickTimeLabels { get; }

        /// <summary>
        /// Gets the collection of available displays.
        /// </summary>
        public ObservableCollection<DisplayInfo> Displays { get; }

        /// <summary>
        /// Gets or sets the currently selected display.
        /// </summary>
        public DisplayInfo? SelectedDisplay
        {
            get => this.selectedDisplay;
            set
            {
                this.selectedDisplay = value;
                this.OnPropertyChanged();
                (this.StartTimerCommand as RelayCommand)?.CanExecute(null);
            }
        }

        /// <summary>
        /// Gets or sets the time input string.
        /// </summary>
        public string TimeInput
        {
            get => this.timeInput;
            set
            {
                this.timeInput = value;
                this.OnPropertyChanged();
                (this.StartTimerCommand as RelayCommand)?.CanExecute(null);
            }
        }

        /// <summary>
        /// Gets or sets the selected timer position.
        /// </summary>
        public TimerPosition SelectedPosition
        {
            get => this.selectedPosition;
            set
            {
                this.selectedPosition = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to remember the user's last settings.
        /// </summary>
        public bool RememberLastSettings
        {
            get => this.rememberLastSettings;
            set
            {
                this.rememberLastSettings = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Saves the current settings.
        /// </summary>
        public void SaveSettings()
        {
            var settings = new UserSettings
            {
                RememberLastSettings = this.RememberLastSettings,
                LastTimerDuration = this.TimeInput,
                LastSelectedPosition = this.SelectedPosition,
                LastSelectedDisplayIndex = this.Displays.IndexOf(this.SelectedDisplay ?? new DisplayInfo()),
            };

            this.settingsService.SaveSettings(settings);
        }

        private void SetQuickTime(object? parameter)
        {
            if (parameter is string minutesString && int.TryParse(minutesString, out int minutes))
            {
                this.TimeInput = $"{minutes}:00";
            }
        }

        private void LoadDisplays()
        {
            var displayInfos = this.displayService.GetDisplayInformation();
            this.Displays.Clear();
            foreach (var display in displayInfos)
            {
                this.Displays.Add(display);
            }

            this.SelectedDisplay = this.Displays.FirstOrDefault(d => d.IsPrimary) ?? this.Displays.FirstOrDefault();
        }

        private void LoadSettings()
        {
            var settings = this.settingsService.LoadSettings();

            this.RememberLastSettings = settings.RememberLastSettings;

            if (this.RememberLastSettings)
            {
                this.TimeInput = settings.LastTimerDuration;
                this.SelectedPosition = settings.LastSelectedPosition;

                if (settings.LastSelectedDisplayIndex >= 0 && settings.LastSelectedDisplayIndex < this.Displays.Count)
                {
                    this.SelectedDisplay = this.Displays[settings.LastSelectedDisplayIndex];
                }
            }
        }

        private void StartTimer(object? parameter)
        {
            var (minutes, seconds) = TimeInputValidator.ParseTimeInput(this.TimeInput);
            int totalSeconds = (minutes * 60) + seconds;

            if (this.SelectedDisplay != null)
            {
                var timerWindow = new TimerWindow(totalSeconds, this.SelectedPosition, this.SelectedDisplay);
                timerWindow.Show();
            }
        }

        private bool CanStartTimer(object? parameter)
        {
            var validationResult = TimerInputValidator.ValidateTimerSetup(
                this.TimeInput,
                this.SelectedDisplay,
                this.SelectedPosition.ToString());

            return validationResult.IsValid;
        }
    }
}