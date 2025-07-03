# 残り目盛り | RemMeter

<img src="./images/logo.png" alt="RemMeter Logo" style="display: block; margin: 0 auto; max-width: 180px; height: auto;">

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

<img src="./images/main-configuration-window.png" alt="RemMeter Screenshot" style="display: block; margin: 0 auto; max-width: 70%;">

### タイマーバー

| UI | 進捗率(経過時間) |
|-----|-----|
| <img src="./images/timer-bar-0_59.png" alt="RemMeter Screenshot" style="max-width: 70%;"> | 0～59% |
| <img src="./images/timer-bar-60_79.png" alt="RemMeter Screenshot" style="max-width: 70%;"> | 60～79% |
| <img src="./images/timer-bar-80_100.png" alt="RemMeter Screenshot" style="max-width: 70%;"> | 80～100% |
| <img src="./images/timer-bar-paused.png" alt="RemMeter Screenshot" style="max-width: 70%;"> | 一時停止中 |

### ホバー時のコントロールパネル

<img src="./images/hover-control-panel.png" alt="RemMeter Screenshot" style="display: block; margin: 0 auto;">

拡大

<img src="./images/hover-control-panel-zoomed.png" alt="RemMeter Screenshot" style="display: block; margin: 0 auto; max-width: 60%;">

### カウント中のバーの表示位置変更

<img src="./images/position-move-panel.png" alt="RemMeter Screenshot" style="display: block; margin: 0 auto;">

拡大

<img src="./images/position-move-panel-zoomed.png" alt="RemMeter Screenshot" style="display: block; margin: 0 auto; max-width: 60%;">

### タイムアップ通知

<img src="./images/time-up-notification.png" alt="RemMeter Screenshot" style="display: block; margin: 0 auto;">

### 画面全体における表示イメージ

下端(水平表示)

<img src="./images/full-screen-image-timer-bar-horizontal.png" alt="RemMeter Screenshot" style="border: 1px solid black; display: block; margin: 0 auto;">

左端(垂直表示)

<img src="./images/full-screen-image-timer-bar-vertical.png" alt="RemMeter Screenshot" style="border: 1px solid black; display: block; margin: 0 auto;">

## ライセンス

MIT License