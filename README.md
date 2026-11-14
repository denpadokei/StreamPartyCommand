# StreamPartyCommand

[![Build and Release](https://github.com/denpadokei/StreamPartyCommand/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/StreamPartyCommand/actions/workflows/build-and-release.yml)

配信のチャットコマンドで Beat Saber のプレイを盛り上げる MOD です。視聴者がコマンドを送ると、ノーツがボムに変わったり、ノーツ・セイバー・ライティング・壁の色が変わります。

## 機能

- `!bomb` で曲中の通常ノーツをボムに置き換えます
- ボムを切ると、コマンドを送った視聴者の名前がテキストで表示されます
- ノーツ、セイバー、ライティング、壁の色をチャットから変更できます
- `rainbow` を指定すると色が循環します
- Chroma / Noodle 譜面では色変更が無効になります
- TwitchFX が入っている場合は色変更コマンドを無視します (ボムは動作します)
- ミスしたときに視聴者名を別のノーツへ再割り当てするか、赤字で表示するかを選べます
- ゲーム内設定画面 (BSML) からボムと各色変更の有効/無効を切り替えられます

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.40.4 |
| 依存 MOD | BSIPA ^4.3.6 / AssetBundleLoadingTools ^1.1.10 / CatCore ^1.1.0 / SiraUtil ^3.2.1 |

## インストール

1. Beat Saber を 1.40.4 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/StreamPartyCommand/releases) から `StreamPartyCommand-5.1.0-bs1.40.4.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

チャットへの接続は CatCore (旧 ChatCore) が提供する設定画面で行います。画像は ChatCore 時代の設定画面です。

![ChatCore の設定画面](https://user-images.githubusercontent.com/55026301/138883861-c55a309a-d4e2-48f2-a1f8-4042d2e28444.png)

1. CatCore の設定画面を開き、Twitch にログインします
2. Channels に配信アカウントの ID を追加します
3. SAVE SETTINGS を押します

ゲームプレイ中、受信したチャットメッセージの先頭がコマンドとして実行されます。

### コマンド一覧

| コマンド | 説明 |
| --- | --- |
| `!bomb` | 曲の途中に出てくる通常ノーツをボムノーツに置き換えます。 |
| `!setnotecolor (left color) (right color)` | ノーツの色を変更します。 |
| `!setsabercolor (left color) (right color)` | セイバーの色を変更します。 |
| `!setlightcolor (left color) (right color)` | ライティングの色を変更します。 |
| `!setwallcolor color` | 壁の色を変更します。 |

色変更は Chroma / Noodle 譜面では無効になり、TwitchFX が入っている場合も実行されません。

### 色一覧

- red
- green
- blue
- yellow
- cyan
- magenta
- white
- black
- gray
- grey
- clear
- rainbow

## 設定

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| `IsBombEnable` | bool | `true` | `!bomb` を有効にする (設定画面の "Is bomb enable.") |
| `TextViewSec` | float | `1` | ボムを切ったときに視聴者名を表示する秒数 |
| `MissTextViewSec` | float | `0.7` | ミス時に視聴者名を赤字で表示する秒数 |
| `IsSaberColorEnable` | bool | `true` | `!setsabercolor` を有効にする (設定画面の "Is saber color enable.") |
| `IsWallColorEnable` | bool | `true` | `!setwallcolor` を有効にする (設定画面の "Is wall color enable.") |
| `IsNoteColorEnable` | bool | `true` | `!setnotecolor` を有効にする (設定画面の "Is note color enable.") |
| `IsPratformColorEnable` | bool | `true` | `!setlightcolor` を有効にする (設定画面の "Is pratform color enable.") |
| `NameObjectLayer` | int | `0` | 視聴者名テキストに設定するレイヤー |
| `ReloadeIfMissCut` | bool | `true` | ミスしたとき視聴者名を別のノーツへ再割り当てする (`false` なら赤字で表示) |

設定は `UserData/StreamPartyCommand.json` に保存されます。ゲーム内設定画面からは 5 つの有効/無効のみ変更でき、それ以外の値はファイルを直接編集してください。

## ビルド

```powershell
git clone https://github.com/denpadokei/StreamPartyCommand.git
cd StreamPartyCommand
dotnet build .\StreamPartyCommand\StreamPartyCommand.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
