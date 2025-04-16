using System;

namespace ChainOfResponsibilityLoggingExample
{
    // ハンドラーの共通インターフェース
    public interface IHandler
    {
        void HandleRequest(int request);
        IHandler SetNext(IHandler handler);
    }

    // Chain of Responsibility用の抽象ハンドラー
    public abstract class HandlerBase : IHandler
    {
        // nextHandler を nullable として宣言
        protected IHandler? nextHandler;

        // チェーンの次のハンドラーを設定する
        public virtual IHandler SetNext(IHandler handler)
        {
            nextHandler = handler;
            return handler;
        }

        public abstract void HandleRequest(int request);
    }

    // 数値の範囲チェックを担当する具体的なハンドラー
    public class RangeCheckHandler : HandlerBase
    {
        private int min, max;
        private string name;

        public RangeCheckHandler(int min, int max, string name)
        {
            this.min = min;
            this.max = max;
            this.name = name;
        }

        public override void HandleRequest(int request)
        {
            if (request >= min && request <= max)
            {
                Console.WriteLine($"{name} がリクエスト {request} を処理しました。");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
            else
            {
                Console.WriteLine($"リクエスト {request} に対する適切なハンドラーが見つかりませんでした。");
            }
        }
    }

    // ログ出力の処理を横断的関心事として提供するデコレーター
    public class LoggingHandlerDecorator : IHandler
    {
        private readonly IHandler innerHandler;

        public LoggingHandlerDecorator(IHandler inner)
        {
            innerHandler = inner;
        }

        public void HandleRequest(int request)
        {
            Console.WriteLine($"[LOG] リクエスト {request} を受け取りました。");
            innerHandler.HandleRequest(request);
            Console.WriteLine($"[LOG] リクエスト {request} の処理が完了しました。");
        }

        // 必要に応じて SetNext を委譲
        public IHandler SetNext(IHandler handler)
        {
            return innerHandler.SetNext(handler);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 数値チェック用のハンドラーを生成
            RangeCheckHandler handler1 = new RangeCheckHandler(0, 9, "ハンドラー1");
            RangeCheckHandler handler2 = new RangeCheckHandler(10, 19, "ハンドラー2");
            RangeCheckHandler handler3 = new RangeCheckHandler(20, 29, "ハンドラー3");

            // ハンドラーのチェーンを組み立てる（ハンドラー1 → ハンドラー2 → ハンドラー3）
            handler1.SetNext(handler2).SetNext(handler3);

            // ログ出力の処理を追加するため、チェーン全体をデコレータでラップする
            IHandler chainWithLogging = new LoggingHandlerDecorator(handler1);

            // リクエストの例
            int[] requests = { 5, 15, 25, 35 };

            foreach (int req in requests)
            {
                chainWithLogging.HandleRequest(req);
                Console.WriteLine();  // 出力を見やすくするための空行
            }
        }
    }
}
