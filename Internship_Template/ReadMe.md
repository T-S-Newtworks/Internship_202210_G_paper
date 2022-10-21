# インターンシップテンプレ　README

インターンシップ用テンプレートプログラムのREADME

インターンシップ中の場当たり的指導による非効率な作業、成果物の具体例の提示がないゆえのインターン生の開発イメージの不足、指導者の技術習熟度の差による負担の大きさ等課題があったため本テンプレートを作成した。<br>インターン生および指導者が成果物作成までのおおよその流れをつかむこと、指導者がどのレベルであってもこれに目を通してもらい技術的仕様を理解し、負担の軽減と属人化の脱却を目的としている。



## テンプレートの機能要件概要

テンプレートとして以下の要件を実装している。

- BootStrapを用いた最低限の画面デザイン(具体的なアプリケーションをイメージし、作成)
- ログイン認証
- CRUD処理



## 技術仕様

 ### 依存するもの、言語、フレームワーク

- .NET Framework4.8
- ASP.NET MVC 5
- Entity Framework
- Oracle Managed Driver
- BootStrap4.1
- jQuery 3.4.1

### フォルダ階層

```
├─App_Start //各種.NET MVC用のコンフィグ
├─Common //ログインのフィルター機能と開発補助用の機能が入っている
├─Content //cssファイル
├─Controllers //各画面のコントローラ
├─fonts
├─image //プロフィール初回設定時のNoImageアイコンだけ入っている
├─Models
│  ├─Entity //ビューモデルのパーツとなる各モデルはここ
│  └─VM //各画面に渡すビューモデル
├─Scripts //Javascriptファイル
└─Views //各画面のビュー
```


## テンプレートプログラムの詳細

### ASP.NET MVCとは

[公式docs概要](https://learn.microsoft.com/ja-jp/aspnet/mvc/overview/older-versions-1/overview/asp-net-mvc-overview)　[公式docsチュートリアル](https://learn.microsoft.com/ja-jp/aspnet/mvc/overview/getting-started/introduction/getting-started)

MVCという名の通り**Model**,**View**,**Controller**の3つのコンポーネントで構成されている。それぞれの役割は以下

- Model : データベースの情報を取得したり、データベースの情報を格納する役割。
- View : Modelに入っているデータをもとにユーザインタフェース（要するに画面）を表示する役割
- Controller : Viewでのユーザの操作を処理し、Modelを使用してデータベースを操作し、結果を再度Viewに渡す役割。要するにModelとViewの橋渡し役。



MVCを用いた具体的な流れとしては図書館で本を調べる画面(View)上でユーザが検索ワードを入力してボタンを押すとController上でデータベースへ接続し、SQLを流し、その結果(検索結果)をModelで取得する。そのModelをもとにContorollerが検索結果画面(View)を表示するよう指示し、ユーザは検索結果一覧を見れるようになる。



### ビューモデルについて

本テンプレートではModelの部分については発展させたビューモデルというコンポーネントを用いている。これは、Modelを複数組み合わせて作ったModelのようなものである。各Modelで用いている色んな情報を一つのビューモデルでまとめることによってデータの受け渡しはビューモデル内で一元管理していることが明らかとなり、Modelの多さに混乱しないよう設計している。なお、本テンプレートでは画面1つにつき1つのビューモデルを扱うというルールで作成している。(TOP画面ならModel/VM/TOP画面.csが対応)



### JavaScriptファイルおよびCssファイルについて

基本的に各画面のScriptタグ内及びStyleタグ内でJavascript,cssを記述している。ただし、一部のもの(システムのテーマカラーや画面の幅など)はScriptフォルダ内のcommon.jsとContent/Commonフォルダ内のcommon.cssに記述している。



### 管理者モード

ログインを省略し、管理者として捜査できるよう管理者モードを用意してます。

利用する場合`web.config`内の設定を有効化してください。

```xml
 <appSettings>
    <!--開発時にログインをスキップできるよう管理者モードを追加-->
    <add key="DebugMode" value="Admin" />
  </appSettings>
```



### ログイン情報

ログイン時にセッション情報を作成し、各Controllerの呼び出し時に基底クラスBaseController内のUserプロパティに設定してあります。また、ビューモデルの基底クラスBaseViewModelにログイン情報格納用のLoginedUserプロパティを用意してあります。ActionResult毎にLoginedUserプロパティにUserプロパティの値を設定することでログイン情報の管理をしています。

[ASP.NETのセッションについて(公式)](https://learn.microsoft.com/ja-jp/previous-versions/msdn/architecture-center/cc465436(v=msdn.10))　[←より多分分かりやすいもの](https://rainbow-engine.com/asp-net-mvc-session/)

```c#
 public ActionResult Index()
        {
            TempData.Remove("model");
            ユーザー画面 model = new ユーザー画面();
            model.Users = _db.T_USER.ToList() ?? new List<T_USER>();
            model.LoginedUser = User; //コントローラのセッション情報をビューモデルに設定
            TempData["model"] = model;

            return View(model);

        }
```



### データの扱いについて

今回はDBファーストでDbContextクラスを作成しているため、DbContextに対し各種メソッドを実行することでデータの登録更新削除が行えます。そのためSQLを記述する必要はありません。データのアクセスについてはLINQを用いて行います。

```c#
                // エンティティを追加＆データソースに反映
                _db.T_USER.Add(user.TargetUser); //_dbがDbContextクラスのオブジェクト。Addメソッドで引数のデータを追加
                _db.SaveChanges(); //SaveChangesメソッドで変更を反映

```

また、本テンプレートではOracleデータベースを利用したDBファーストでのデータモデル自動生成を行っています。そのため、通常のEntityDataModelとは少し異なった手法でのモデル生成になっています。データモデルの生成、更新については以下のリンクを参考にしてください。

[本テンプレートでのデータモデル作成方法](http://192.168.100.157:10080/team-ramune/Socrates/issues/22)
