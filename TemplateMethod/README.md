# TemplateMethodパターン
## パターンの用途メリットなど
全体のフローが固定だけど具体的な処理が場合によって全く違う場合に使える
## サンプルの説明-照明管理システム
時間によって照明のオンオフと朝昼晩に割り当てられた照明の処理を行うシステムです。
照明のスケジュールが抽象クラスによって固定されていて、具体的な照明のセッティングはサブクラスに任せています。
## クラス図
<img width="580" alt="image" src="https://github.com/user-attachments/assets/2d7a9d28-14a3-474c-88a2-632c5057a1e1" />


## 改良版 クラス図 
<img width="711" alt="image" src="https://github.com/user-attachments/assets/fdbb7ee1-11f9-45c8-a264-b4809bbdb6a7" />
