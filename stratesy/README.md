# Strategyパターン
## 用途.メリット
処理の中心になるコンテキスト役からアルゴリズムの部分だけ切り離して、交換可能にできるのがメリット
アルゴリズムの部分が追加や変化する可能性が高い場合に使える

## 万華鏡パターンメーカーシステム
二次元配列で15x15のように作ったグリッドに対し0と1を設定して、
1マスごとに反転させる、3連ますごとに反転させる　という処理をしてグリッドの模様パターンを操作します。
どういうパターンで反転させるかをコンクリートストラテジーで実装し、パターンの追加が容易にできるようにしています。

## クラス図

<img width="734" alt="image" src="https://github.com/user-attachments/assets/16b4ee83-e1be-4ac4-979e-e697dac90bc0" />

drow.io
https://drive.google.com/file/d/1B_b9uTVWDSpK8x_1wxcv4S1CapBDKxKd/view?usp=sharing
