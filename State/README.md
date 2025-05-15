# Stateパターン
## パターンの理解
- 状態が複数あり、状態が変わると振る舞いが変わるものに使う
	- 組み合わせが多い
	- 状態が増える可能性がある
- コンテキスト側からは状態に関する処理はStateに完全に任せられる
- 状態遷移はどちらに持たせてもいい

## サンプル ファミレスの配膳ロボットシステム
ファミレスの猫の配膳ロボットのフローに適応しました。
- キッチンで待機　料理を乗せスイッチを押すと客席へ
- 移動中　スイッチは無視。客席へつくと配膳モードへ
- 提供中 料理がとられてスイッチが押されるとキッチンへ戻る

### 機能
- PressSwitch()
  - ロボットにただ一つあるボタンを押した時の動作
- Complete()
  - 客席や厨房に移動するなど目的を達成する

## クラス図
<img width="833" alt="image" src="https://github.com/user-attachments/assets/dc5cb806-12f7-4e36-820e-6ef2164587c3" />

![image](https://github.com/user-attachments/assets/339c7d40-f038-487c-b7fc-cf6fdfe364ef)


### feedback
- mediator組み合わせが良さそう
- 責務が分離できていない。
- 「分割して統治せよ」←大事
- press →push
  - press 圧をかける　押し込んだとき
  - プレススイッチ　押してある間だけ通電する
  - singletonの黒魔術ーリフレクション
  - C#にも構文解読がある
  - LINQ式は黒魔術
  - ラムダ式　引数にラムダ式を渡す
  - MVVM　オブザーバー

## 宿題
- completeメソッドを引き離して、責務の分離ちゃんとする
- 状態遷移の管理を、どちらかに寄せる。

## 宿題'
- Completeメソッドを分けた
  - DoExecute()
  - DoComplete()
コンテキストのChangeState()で状態が変わると、DoExecuteが呼ばれ、状態毎の動作を処理する。DoExecute()の処理が完了するとその中でDoCompleteが呼ばれ、ChangeState()する