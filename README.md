# 残り目盛り | RemMeter

![RemMeter Logo](./images/logo.png)

プレゼンテーション等の際に、直感的に残り時間を把握できるお役立ちタイムキーパーです。

画面端に細長く表示されるため、プレゼンの資料投影中でも邪魔になりません。

たぶん、操作説明書なしに使えると思います。

## 特色

- 視覚的なアナログ進捗表示
- 上下左右から選べる表示位置
- マルチディスプレー、DPI対応
- 一時停止・再開
- タイムアップ通知
- 常に最前面に表示

## WPF版 (Windows専用)

### システム要件
- .NET 8.0 SDK以上
- Windows 10/11
- マルチディスプレー環境（オプション）

### ダウンロード

[Releases](https://github.com/yokenzan/rem-meter/releases)よりダウンロードできます。

#### Framework-dependent版（サイズ小）
- `RemMeter-framework-dependent-win-x64.exe` - 64bit Windows用
- `RemMeter-framework-dependent-win-x86.exe` - 32bit Windows用

> [!NOTE]
> .NET 8.0 Desktop Runtimeが必要です。未インストールの場合、アプリ実行時に自動でダウンロードページに案内されます。
> [.NET 8.0 Desktop Runtime ダウンロード](https://dotnet.microsoft.com/download/dotnet/8.0)

#### Self-contained版（サイズ大 .NET 8.0 Desktop Runtime不要）
- `RemMeter-self-contained-win-x64.exe` - 64bit Windows用
- `RemMeter-self-contained-win-x86.exe` - 32bit Windows用

### ビルド・実行

```bash
cd wpf
dotnet build
```

```bash
cd wpf
dotnet run
```

```bash
cd wpf
# 64bit版(Framework-dependent)
dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
# 32bit版(Framework-dependent)
dotnet publish -c Release -r win-x86 --self-contained false -p:PublishSingleFile=true
# 64bit版(Self-contained)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
# 32bit版(Self-contained)
dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=true
```

## スクリーンショット

### 初期画面

![RemMeter Screenshot](./images/main-configuration-window.png)

### タイマーバー

| UI | 進捗率(経過時間) |
|-----|-----|
| ![RemMeter Screenshot](./images/timer-bar-0_59.png) | 0～59% |
| ![RemMeter Screenshot](./images/timer-bar-60_79.png) | 60～79% |
| ![RemMeter Screenshot](./images/timer-bar-80_100.png) | 80～100% |
| ![RemMeter Screenshot](./images/timer-bar-paused.png) | 一時停止中 |

### ホバー時のコントロールパネル

![RemMeter Screenshot](./images/hover-control-panel.png)

### カウント中のバーの表示位置変更

![RemMeter Screenshot](./images/position-move-panel.png)

### タイムアップ通知

![RemMeter Screenshot](./images/time-up-notification.png)

### 画面全体における表示イメージ

下端(水平表示)

![RemMeter Screenshot](./images/full-screen-image-timer-bar-horizontal.png)

左端(垂直表示)

![RemMeter Screenshot](./images/full-screen-image-timer-bar-vertical.png)

## ライセンス

MIT License