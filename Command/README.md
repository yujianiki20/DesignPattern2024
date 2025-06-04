# パターンの理解

- コマンドを増やすにはクラスを増やす必要がある
- レシーバーも増えることが想定されてる
- コマンド＝増えることができる＝できることを増やす
- レシーバー=何が行えるかを明示してる＝内部は違えど同じinvokerから指示を受けることができる
- レシーバー＝本当の作業者
- インボーカーのはクライアントコード的、これを変更する必要はない
	- ドライバーか？
- 機能を抽象化するパターン
	- 規格化っていうかんじ
- 本当の仕事をやる作業員はinvoker
- レシーバーとコマンドを結びつけるのがclient

## 登場人物
- Command
- ConcreateCommand
- Receiver(drwoCanvas)
- client(Main)
- Invoker(Main,DrowCanvas)

# サンプル　カラーコードのレシーバーとコマンド
- 色を指定するCommandとその色を利用するreceiverの組み合わせでコンソールの背景色の変更にパターンを適用しました。
- レシーバー:コンソールの背景色を変更する
- MacroCommandクラスがあり、履歴の管理をしている
- invokerからこのレシーバーとMacroCommandを扱う
- メインでは入力からカラーコードを受け取り、invokerに渡す

# クラス図
<img width="853" alt="image" src="https://github.com/user-attachments/assets/6daedd09-3710-4353-869e-c40ceb41e1cc" />
