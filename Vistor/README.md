# Visitorパターン
## パターンの理解
- compositのパターンに対して外側から再起的な処理をさせたい時
- コンクリートビジターを追加することで別の処理を追加することができる

## エフェクターシステム＋セッティング機能
前回のDecoratorにVsitorで拡張して、Vsitorによって各エフェクターの設定をしていく機能を追加しました。
前回のデコレーターでいうコンクリートコンポーネント（元の音）と、コンクリートデコレーター(エフェクター)
に対してVisitorが処理を行なっていくようになっています。

- compositに似てるならdecoratorにも適用できるか？
- vsitorが適用できた例ではあるけどvisitorが最適だったかは微妙な気がする。
- コンクリートエレメントが増えるとvisitメソッドのオーバーロードを増やし続ける必要がある
- オブジェクトストラクチャ役がどれに当たるか、わかりにくくなった

## クラス図

<img width="1084" alt="image" src="https://github.com/user-attachments/assets/ac75d6d7-0903-4ba8-b377-d3b699398414" />
https://drive.google.com/file/d/1jV_OZq0DRMolaqmVcTPnx1O7VXGPvZEU/view?usp=sharing

----
#### *前回のデコレーターパターンのクラス図
<img width="959" alt="image" src="https://github.com/user-attachments/assets/3a6bad5f-36fd-4a9a-ab88-3714a1e7aedd" />
https://drive.google.com/file/d/1ehmFnMD3KmaPcUrvkTXtUSQmYeba3gdU/view?usp=sharing

