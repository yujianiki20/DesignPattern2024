# Singletonパターン
## パターンの用途メリットなど
アプリケーション全体で共有する設定やログなど
どこからアクセスしても同じインスタンスを共有できる

## サンプルの説明-ジャンケン戦績記録システム
じゃんけんの戦績をシングルトンのインスタンスで管理しています。
一回プレイするごとに記録が追加されていき、ログの出力も自由なタイミングでできます。

## クラス図
<img width="585" alt="image" src="https://github.com/user-attachments/assets/a8441f15-7ecb-4c0d-9f93-746631744756" />
