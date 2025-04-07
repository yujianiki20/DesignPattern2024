# Visitorパターン
## パターンの理解
- compositのパターンに対して外側から再起的な処理をさせたい時
- コンクリートビジターを追加することで別の処理を追加することができる

## エフェクターシステム＋セッティング機能
前回のDecoratorにVsitorで拡張して、Vsitorによって各エフェクターの設定をしていく機能を追加しました。
前回のデコレーターでいうコンクリートコンポーネント（元の音）と、コンクリートデコレーター(エフェクター)
に対してVisitorが処理を行なっていくようになっています。

### 考えたこと・疑問
- compositに似てるならdecoratorにも適用できるか？
- vsitorが適用できた例ではあるけどvisitorが最適だったかは微妙な気がする。
- コンクリートエレメントが増えるとvisitメソッドのオーバーロードを増やし続ける必要がある
  - 例えば各エフェクターでnameフィールドを持たせて、
   visit(Effecter effecter)のオーバーロードでnameに応じて設定するようにすれば、visitメソッドは最小で済む？
- オブジェクトストラクチャ役がどれに当たるか、わかりにくくなった

## クラス図

<img width="1084" alt="image" src="https://github.com/user-attachments/assets/ac75d6d7-0903-4ba8-b377-d3b699398414" />
https://drive.google.com/file/d/1jV_OZq0DRMolaqmVcTPnx1O7VXGPvZEU/view?usp=sharing

----
#### *前回のデコレーターパターンのクラス図
<img width="959" alt="image" src="https://github.com/user-attachments/assets/3a6bad5f-36fd-4a9a-ab88-3714a1e7aedd" />
https://drive.google.com/file/d/1ehmFnMD3KmaPcUrvkTXtUSQmYeba3gdU/view?usp=sharing

# 宿題
- コンクリートビジターを増やして、訪問する順番を変えるビジターをつくる（再帰的以外もできる）
- フィルタリングや、特定のものだけ訪れる、順番がイレギュラーでもいける
- 204P再読


# 
今回のエフェクターのパターンだと直線的だから、イレギュラーな順番を考えると
「処理しないで次へ」「特定の条件で中断」「逆順」しかできないことに気がついた

コンポジットパターンで、コンポジットは複数の要素を持っているので、分岐する（処理順を制御する）動機がある。



ーーー
acceptのほうで再帰(訪問先)をコントロールすることができる
今回デコレーターにしたが、コンポジットの場合、再帰処理で分岐するルートがあるなという気がついた。
デコレーターは処理順が最後のエレメントまで直線的となる。（今回の自分のエフェクターのパターンだけ？）

本ではDirectoryエレメントがイテレーターを持っていて、次の訪問先はDirectoryのacceptが決めている。
- イテレーターをレメントの方が持ってて（accept）ビジターが利用する

-----
AcylicVisitor
- visitorと各エレメントの相互依存を解消すルパターン
- visitorにはメソッドを強制しない
- エレメントの方にだけ