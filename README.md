# DesignPattern2024
## MydesignSample
- iteratorパターン

## Adapter
- Adapterパターンの課題
- クラス図
  - https://app.diagrams.net/?libs=general;uml#G1AAmrgoQMTmcSansnPvxwQsqxXEBgGfWm#%7B%22pageId%22%3A%22C5RBs43oDa-KdzZeNtuy%22%7D
<img width="788" alt="image" src="https://github.com/user-attachments/assets/e0316c9e-47d5-47ff-b15f-93a56b100e1f" />

### Adapterパターンの理解
単位や書式の変換のようなケースに適用できる。
アダプターを扱う側からは古いシステムを意識せずに使える。

### タイマー管理システム
秒数をカウントダウンして数字を返すだけのタイマーのクラスを利用して、x時x分x秒のフォーマットで残り時間を表示してカウントダウンするシステムです。

### adapterパターンに適したadapteeに修正
<img width="808" alt="image" src="https://github.com/user-attachments/assets/03bc3b27-9f10-4f53-99b0-4eee4fc3c90c" />

adaptee役のTimerクラスのメソッドに引数を必要とするよう変更して、Mainのコードは変えずにadapterの修正だけで同じ動作をできるようにしました。
カウトダウンだけでなく、カウントアップができるadapterも追加しました。
