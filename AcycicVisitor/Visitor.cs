public abstract class Visitor {
    public abstract void visit(Sound sound);
    
    /// 汎用コントロール
    public abstract void visit(Effecter effecter);
    
    /// 個別のコントロール
    public abstract void visit(Distortion distortion);
}

// コンクリートビジター
public class ControlVisitor : Visitor {
    public override void visit(Sound sound) {
        Console.WriteLine("適用完了");
    }
    
    public override void visit(Effecter effecter) {
        Console.WriteLine("設定なし");
        if (effecter._sound is IElement next)
            next.Accept(this);
    }
    
    
    public override void visit(Distortion distortion) {
        Console.WriteLine("Distortion Gain = 10");
        distortion.Gain = 10;
        if (distortion._sound is IElement next)
            next.Accept(this);
    }
}

public class AltVisitor : Visitor {
    public override void visit(Sound sound) {
        Console.WriteLine("適用完了");
    }
    
    /// 汎用コントロールのオーバーロード、型で条件分岐
    public override void visit(Effecter effecter) {

        if (effecter is Tuner) {
            Console.WriteLine("Tuner-設定不要");
        }
        else if (effecter is Equalizer) {
            Console.WriteLine("Equalizer用設定");
        }
        else if (effecter is Fuzz) {
            Console.WriteLine("Fuzz用設定");
        }
        else if (effecter is VolumePedal) {
            Console.WriteLine("VolumePedal-設定不要");
        }
        else
        {
            Console.WriteLine("未定義のエフェクター");
        }

        if (effecter._sound is IElement next)
            next.Accept(this);
    }
    
    
    public override void visit(Distortion distortion) {
        Console.WriteLine("Distortion Gain = 10");
        distortion.Gain = 10;
        if (distortion._sound is IElement next)
            next.Accept(this);
    }
}


public interface IElement {
    void Accept(Visitor v);
}