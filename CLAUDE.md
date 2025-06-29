# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

RemMeter (formerly seekbar-like-timer) is a WPF desktop application for Windows that provides a visual timer for presentations. It displays a progress bar on the edge of the screen that changes color as time progresses.

**Tech Stack**: C# (.NET 8.0), WPF, Windows Forms interop
**Target**: Windows 10/11 only

## Common Development Commands

All commands should be run from the `wpf/` directory:

### Build & Run
```bash
make run          # Build and run (Release)
make run-debug    # Build and run (Debug, with logging)
make build        # Build only (Release)
make debug        # Build only (Debug)
```

### Publishing
```bash
make publish      # Create single executable (self-contained, win-x64)
make publish-debug # Create debug executable with logging
```

### Code Quality
```bash
make format       # Format code (IMPORTANT: Run before committing)
make test         # Run tests (infrastructure ready, no tests yet)
```

### Direct dotnet commands (alternative)
```bash
dotnet build RemMeter.csproj
dotnet run --project RemMeter.csproj
dotnet format style RemMeter.csproj    # Check code style
dotnet format analyzers RemMeter.csproj # Check analyzer issues
```

## Architecture & Code Organization

### Current Pattern: Code-Behind (not MVVM)
- Business logic is in `MainWindow.xaml.cs` and `TimerWindow.xaml.cs`
- TODO.md Priority 1: Migrate to MVVM pattern for better testability

### Key Components
1. **MainWindow**: Configuration UI for timer settings
2. **TimerWindow**: The actual timer display with progress bar
3. **Helpers/**:
   - `DisplayHelper`: DPI-aware display calculations
   - `PositionMapper`: Efficient position mapping with i18n
   - `Logger`: File + Windows Event Log fallback
   - `NotificationHelper`: System tray notifications
4. **Validation/**: Input validation classes (extracted from code-behind)
5. **Models/**: `DisplayInfo`, `TimerPosition` data structures

### Important Patterns
- Uses Windows Forms `Screen` for multi-monitor support
- All styling centralized in `App.xaml`
- Full i18n support (ja-JP, en-US, zh-CN, zh-TW)
- Settings persisted via `Properties.Settings`

## CI/CD & Versioning

### Semantic Release
- Automatic versioning based on conventional commits
- Version stored in `Directory.Build.props`
- Releases created with 4 build variants:
  - Framework-dependent (x64/x86) - requires .NET Runtime
  - Self-contained (x64/x86) - includes runtime

### GitHub Actions Workflows
1. **ci.yml**: PR checks (build, code style, analyzers)
2. **build-wpf.yml**: Creates release artifacts
3. **release.yml**: Semantic versioning and GitHub releases

### Commit Message Format
Follow conventional commits for automatic versioning:
- `feat:` for new features (minor version bump)
- `fix:` for bug fixes (patch version bump)
- `feat!:` or `BREAKING CHANGE:` for breaking changes (major version bump)

## Code Style & Quality

- **StyleCop.Analyzers** enforced (warnings as errors)
- **EditorConfig** in `wpf/.editorconfig`
- Always run `make format` before committing
- No unit tests exist yet (infrastructure ready)

## Key Architectural TODOs (from TODO.md)

1. **Implement MVVM pattern** - Move logic from code-behind to ViewModels
2. **Add unit tests** - Test time parsing, timer accuracy, state management
3. **Consider dependency injection** - For better testability

## Development Tips

1. The application combines WPF and Windows Forms - be aware of namespace conflicts
2. All display calculations must be DPI-aware (use DisplayHelper)
3. Timer accuracy is critical - use DispatcherTimer carefully
4. Multi-monitor support is essential - test with multiple displays
5. Localization is required - update all resource files when adding UI text