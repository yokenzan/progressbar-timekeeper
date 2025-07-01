# RemMeter Cross-Platform Specification

## 1. Overview

This document outlines the functional requirements for RemMeter, a visual presentation timer. The goal is to create a consistent user experience across different native desktop platforms (including Windows, macOS, and Linux).

The application provides a minimalistic, screen-edge timer bar that visually represents the passage of a set amount of time, designed to be unobtrusive during presentations or other focused work.

## 2. Core Functionality

### 2.1. Timer Mechanism

- The application shall allow a user to set a specific duration for the timer.
- Once started, the timer shall count down from the set duration to zero.
- The application must provide functionality to start, pause, resume, and stop the timer.

### 2.2. Visual Progress Bar (The Timer Bar)

- When the timer is active, a thin visual bar shall be displayed on the screen.
- This bar shall graphically represent the elapsed time. The length of the bar should grow progressively to fill its maximum extent as the timer approaches zero.
- The orientation of the progress fill depends on the bar's position on the screen:
    - **Vertical Positioning (Left/Right Edge):** The bar shall fill from bottom to top.
    - **Horizontal Positioning (Top/Bottom Edge):** The bar shall fill from left to right.

## 3. User Interface (UI) and User Experience (UX)

### 3.1. Main Configuration Window

This is the primary interface for setting up the timer. It must contain the following controls:

- **Time Input:**
    - Numeric input fields to specify the timer duration in minutes and seconds.
    - A set of preset buttons for common durations (e.g., 5 min, 15 min, 30 min) for quick setup.
- **Display Selection:**
    - A dropdown menu or similar selection control to list all connected displays/monitors.
    - The user must be able to select which display the timer bar will appear on.
    - The primary display should be clearly identifiable.
- **Position Selection:**
    - A dropdown menu or similar selection control to specify the position of the timer bar on the selected display.
    - The available options must be: `Top Edge`, `Bottom Edge`, `Left Edge`, `Right Edge`.
- **Start Button:**
    - A button to start the timer and display the timer bar.
    - Once the timer is started, this window may be closed without affecting the timer.

### 3.2. Timer Bar Window

- The timer bar itself must be displayed in a borderless, minimalistic window.
- **Always on Top:** The timer bar window must always be rendered on top of all other application windows on the desktop.
- **Hover Controls:**
    - When the user's cursor hovers over the active timer bar, a small control panel shall appear.
    - This panel must provide buttons for:
        - **Pause/Resume:** Toggles the timer's countdown.
        - **Stop:** Terminates the timer and dismisses the timer bar.
- The hover control panel should disappear when the cursor moves away from the timer bar area.

### 3.3. Dynamic Position Change During Timer Operation

- **Functionality**: Users shall be able to change the position of the timer bar while the timer is running.
- **Timer State**: The timer's countdown must continue uninterrupted during the position change process.
- **Trigger**: A "Change Position" button shall be added to the hover control panel.
- **Process**:
    - Clicking the "Change Position" button shall enter a "position selection mode."
    - In this mode, icons representing the four possible positions (`Top Edge`, `Bottom Edge`, `Left Edge`, `Right Edge`) shall be displayed.
    - Clicking one of these icons shall immediately move the timer bar to the selected position and exit the selection mode.
- **Cancellation**: The position selection mode can be cancelled by one of the following actions, which will hide the position icons:
    - Pressing the `Esc` key.
    - Clicking the "Change Position" button again.
    - Clicking anywhere on the screen outside of the position selection icons.
- **Persistence**: The newly selected position shall be saved as the default for subsequent application launches.

## 4. Visual and Audio Cues

### 4.1. Color Progression

The color of the timer bar must change dynamically to indicate the urgency of the remaining time.

- **Phase 1 (Start - 59% elapsed):** A standard color (e.g., Green).
- **Phase 2 (60% - 79% elapsed):** A warning color (e.g., Orange).
- **Phase 3 (80% - 100% elapsed):** A critical color (e.g., Red).

### 4.2. Blinking Effect

- During Phase 3 (critical color), the timer bar shall blink continuously to draw the user's attention.
- The blinking should be a smooth transition between a lower and full opacity.

### 4.3. Time-Up Notification

- When the timer reaches zero, the application shall generate a prominent system notification.
- This could be a modal dialog box or a native OS notification (e.g., toast notification).
- The notification must clearly state that the time is up.

## 5. Settings and Persistence

- The application must save the user's last-used settings.
- The settings to be persisted include:
    - Timer duration (minutes and seconds).
    - Selected display.
    - Selected position.
- These settings shall be automatically loaded and applied the next time the application is launched.

## 6. System Integration

### 6.1. Application Versioning
- The application's version number must be accessible and viewable within the app, for example, in the title of the main configuration window.

## 7. Localization (i18n)

- All user-facing strings in the UI must be localizable.
- The application should be prepared to support multiple languages.
- Initial target languages are:
    - English (en-US)
    - Japanese (ja-JP)
    - Simplified Chinese (zh-CN)
    - Traditional Chinese (zh-TW) 