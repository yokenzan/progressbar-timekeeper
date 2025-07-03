# RemMeter Cross-Platform Specification

## 1. Overview

This document provides the complete functional and technical specification for RemMeter, a visual presentation timer. This specification contains all details necessary to create a consistent implementation across different native desktop platforms (Windows, macOS, and Linux), ensuring identical behavior and user experience.

RemMeter provides a minimalistic, screen-edge timer bar that visually represents the passage of a set amount of time. The application is designed to be unobtrusive during presentations, meetings, or other focused work sessions while providing clear visual feedback about remaining time.

## 2. Core Functionality

### 2.1. Timer Mechanism

#### 2.1.1. Timer Duration
- **Supported Range**: 1 second to 9999 minutes (166.65 hours)
- **Input Methods**:
  - Direct numeric input as total seconds (e.g., "90" = 90 seconds)
  - Time format parsing (e.g., "1:30" = 90 seconds, "90:00" = 90 minutes)
  - Quick selection buttons for common durations

#### 2.1.2. Timer States
- **Idle**: Timer not started, configuration window active
- **Running**: Timer counting down, decrementing every second
- **Paused**: Timer temporarily halted, maintaining current remaining time
- **Completed**: Timer reached zero, notification shown

#### 2.1.3. Timer Precision
- **Update Interval**: 1 second (1000ms)
- **Accuracy**: System clock-based timing
- **Persistence**: Timer continues during position changes without losing time

### 2.2. Visual Progress Bar (Timer Bar)

#### 2.2.1. Bar Dimensions
- **Vertical Positioning (Left/Right Edge)**:
  - Width: 20 pixels (fixed)
  - Height: 80% of screen height
  - Margin from edge: 10 pixels
  - Vertical centering: (screen_height - bar_height) / 2
- **Horizontal Positioning (Top/Bottom Edge)**:
  - Height: 20 pixels (fixed)
  - Width: 80% of screen width
  - Margin from edge: 10 pixels (top), 50 pixels (bottom)
  - Horizontal centering: (screen_width - bar_width) / 2

#### 2.2.2. Progress Direction
- **Vertical bars (Left/Right)**: Fill from bottom to top
- **Horizontal bars (Top/Bottom)**: Fill from left to right
- **Fill calculation**: progress = (totalSeconds - remainingSeconds) / totalSeconds

#### 2.2.3. Visual Appearance
- **Background**: Semi-transparent black (rgba: 0, 0, 0, 0.25)
- **Border**: Semi-transparent white (rgba: 255, 255, 255, 0.5), 1px width
- **Corner radius**: 2 pixels
- **Z-order**: Always on top of all other windows

## 3. User Interface Components

### 3.1. Main Configuration Window

#### 3.1.1. Window Properties
- **Default Size**: 450 x 350 pixels
- **Position**: Center of primary screen on launch
- **Resizable**: No
- **Title**: "{AppName} v{Version}" (e.g., "RemMeter v1.2.0")

#### 3.1.2. Time Input Section
**Time Input TextBox**:
- Width: 80 pixels
- Height: 32 pixels
- Center-aligned text
- Bold font, size 16
- Default value: "1000" (10 minutes)
- Accepts: Numbers only, up to 4 digits
- Select-all behavior on focus

**Time Display**:
- Shows formatted time (MM:SS or H:MM:SS)
- Updates in real-time as input changes
- Font size: 20, bold
- Color: Red (#FF0000)
- Minimum width: 60 pixels

**Quick Time Buttons**:
- Button dimensions: 55 x 30 pixels
- Available presets: 1, 5, 10, 15, 30, 60 minutes
- Labels: Localized (e.g., "1min" for English, "1分" for Japanese)
- Margin between buttons: 2 pixels

#### 3.1.3. Display Selection
**Display ComboBox**:
- Minimum width: 200 pixels
- Maximum width: 250 pixels
- Format: "Display {number} - {width}x{height}" 
- Primary display: "Display {number} - {width}x{height} (Primary)"
- Default selection: Primary display
- Sorted by: Display index

#### 3.1.4. Position Selection
**Position Labels**:
- Clickable text elements
- Options: Right, Left, Top, Bottom
- Normal state: Regular font, padding 8x4 pixels
- Selected state: Bold, underlined, red color (#FF0000)
- Margin between labels: 10 pixels (except last: 0)
- Default selection: Right

#### 3.1.5. Settings Persistence
**Remember Settings Checkbox**:
- Label: "Remember my settings"
- Default: Checked
- Saves: Timer duration, selected display, selected position
- Storage: User application settings

#### 3.1.6. Start Button
- Dimensions: 100 x 40 pixels
- Font size: 16
- Background: Green (#4CAF50)
- Text color: White
- Label: "Start" (localized)

### 3.2. Timer Bar Window

#### 3.2.1. Window Properties
- **Style**: Borderless, transparent background
- **Always on top**: Yes
- **Show in taskbar**: No
- **Mouse interaction**: Triggers control panel on hover

#### 3.2.2. Progress Bar Colors
**Color Phases**:
1. **Green Phase** (0-59% elapsed): #4CAF50
2. **Orange Phase** (60-79% elapsed): #FFA500
3. **Red Phase** (80-100% elapsed): #FF0000
4. **Paused State**: Dark slate blue (#483D8B)

**Blinking Animation** (Red phase only):
- Type: Opacity animation
- Range: 0.3 to 1.0
- Duration: 500ms per cycle
- Pattern: Continuous fade in/out

#### 3.2.3. Control Panel (Hover UI)
**Panel Properties**:
- Background: Semi-transparent white (rgba: 224, 255, 255, 255)
- Border: 1px solid #CCCCCC
- Corner radius: 5 pixels
- Padding: 5 pixels
- Expanded dimensions:
  - Vertical bars: 120 x 140 pixels
  - Horizontal bars: 140 x 120 pixels

**Time Display**:
- Format: "M:SS" or "MM:SS"
- Font size: 18, bold
- Color: Red (#FF0000)
- Margin: 10 pixels
- Updates every second

**Control Buttons**:
- Dimensions: 75 x 30 pixels each
- Labels: "Pause"/"Resume", "Stop", "Move" (localized)
- Layout adaptation:
  - Vertical bars: Buttons stacked vertically (5px bottom margin)
  - Horizontal bars: Buttons arranged horizontally (5px right margin)

### 3.3. Position Change Feature

#### 3.3.1. Move Button Behavior
- Triggers position selection popup
- Popup placement: Centered on Move button
- Background: White with control panel styling

#### 3.3.2. Position Selection Popup
**Layout**: 3x3 grid
- Center cell: Cancel button (×)
- Surrounding cells: Direction arrows

**Direction Buttons**:
- Dimensions: 50 x 50 pixels
- Font size: 24
- Symbols: ↑ (Top), ↓ (Bottom), ← (Left), → (Right), × (Cancel)
- Background: Transparent
- Current position button: Hidden
- Tooltips: Localized (e.g., "Move to top edge")

#### 3.3.3. Position Change Behavior
- Timer continues running during position change
- Window immediately moves to new position
- Progress bar redraws for new orientation
- New position saved to settings (if enabled)
- Popup dismissal: On selection or cancel

## 4. Behavioral Specifications

### 4.1. Input Validation

#### 4.1.1. Time Input Validation
- **Empty input**: Default to 10:00 (600 seconds)
- **Zero or negative**: Show error "Please enter a positive number"
- **Out of range** (>9999): Show error "Maximum allowed time is 9999 minutes"
- **Invalid format**: Show error "Please enter time in seconds or MM:SS format"

#### 4.1.2. Display Validation
- **No display selected**: Show error "Please select a display"
- **Display disconnected**: Fallback to primary display

### 4.2. Window Interactions

#### 4.2.1. Main Window
- **On Start**: Hide main window, show timer window
- **On Close**: Save settings (if enabled), terminate application

#### 4.2.2. Timer Window
- **On Stop**: Close timer window, show main window
- **On Complete**: Show notification, close timer window, show main window
- **Hover behavior**: 
  - Enter: Expand to show control panel
  - Leave: Collapse to bar-only view

### 4.3. Notifications

#### 4.3.1. Time-Up Notification
- **Title**: "Timer" (localized)
- **Message**: "{M} minutes {S} seconds have elapsed!" (localized)
- **Type**: System notification (native OS notification)
- **Duration**: 5 seconds
- **Fallback**: Modal dialog if system notifications unavailable

## 5. Multi-Monitor Support

### 5.1. Display Detection
- **Enumeration**: All connected displays
- **Information captured**:
  - Physical bounds (x, y, width, height)
  - Primary display flag
  - DPI scale factors

### 5.2. DPI Awareness
- **Required**: Full per-monitor DPI awareness
- **Coordinate conversion**: Physical pixels to logical units
- **Scaling**: All UI elements must scale with display DPI
- **Formula**: logical_value = physical_value / dpi_scale

### 5.3. Display Changes
- **Hot-plug support**: Not required (user must restart)
- **Resolution changes**: Maintain relative position
- **Primary display changes**: Continue on selected display

## 6. Performance Requirements

### 6.1. Resource Usage
- **Memory**: < 50MB typical usage
- **CPU**: < 1% during normal operation
- **Timer accuracy**: ±100ms over 1 hour

### 6.2. Responsiveness
- **UI response time**: < 100ms for all interactions
- **Window positioning**: < 50ms
- **Hover detection**: < 200ms

## 7. Localization Requirements

### 7.1. Supported Languages
- English (en-US) - Default
- Japanese (ja-JP)
- Simplified Chinese (zh-CN)
- Traditional Chinese (zh-TW)

### 7.2. Localizable Elements
All user-facing text must be externalized:
- Window titles
- Button labels
- Position names
- Error messages
- Notification text
- Tooltips

### 7.3. Text Encoding
- **Required**: UTF-8 support throughout
- **Font**: System default with fallback chain

## 8. Data Persistence

### 8.1. Settings Storage
**Location**: User application settings (platform-specific)
**Format**: Key-value pairs
**Keys**:
- `RememberLastSettings`: boolean
- `LastTimerDuration`: string (e.g., "600")
- `LastSelectedPosition`: string ("Right", "Left", "Top", "Bottom")
- `LastSelectedDisplayIndex`: integer

### 8.2. Settings Behavior
- **Load**: On application start (if RememberLastSettings = true)
- **Save**: On timer start (if RememberLastSettings = true)
- **Position changes**: Immediately persisted during timer operation

## 9. Technical Architecture

### 9.1. Application Structure
- **Main Window**: Configuration and setup
- **Timer Window**: Timer display and controls
- **Shared Components**:
  - Timer state management
  - Display enumeration
  - Settings persistence
  - Localization resources

### 9.2. Timer Implementation
- **Timer type**: UI thread timer (1-second interval)
- **State tracking**: Total seconds, remaining seconds, pause state
- **Color calculation**: Based on elapsed percentage
- **Animation**: Separate animation timer for blinking

### 9.3. Error Handling
- **Display enumeration failure**: Create default 1920x1080 display
- **Settings corruption**: Use application defaults
- **Window positioning errors**: Center on primary display
- **Resource loading failures**: Fall back to English strings

## 10. Platform-Specific Considerations

### 10.1. Window Management
- **Always-on-top**: Must work with fullscreen applications
- **Transparency**: Requires compositor support
- **Multi-desktop**: Follow active desktop/workspace

### 10.2. System Integration
- **Notifications**: Use native notification system
- **DPI scaling**: Follow system DPI settings
- **Theme**: Independent of system theme

## 11. Accessibility

### 11.1. Keyboard Navigation
- **Tab order**: Logical flow through all controls
- **Enter key**: Activate focused button
- **Escape key**: Cancel dialogs/popups

### 11.2. Screen Readers
- **Labels**: All controls must have accessible names
- **State changes**: Announce timer state changes
- **Notifications**: Accessible notification content

## 12. Version Information

### 12.1. Version Display
- **Format**: "RemMeter v{Major}.{Minor}.{Patch}"
- **Location**: Main window title
- **Source**: Build configuration

This specification represents the complete implementation details of RemMeter as of version 1.0.0. Any platform-specific implementation should adhere to these specifications to ensure consistent behavior across all supported platforms.