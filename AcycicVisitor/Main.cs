using System;

// component役
public abstract class Sound : IElement {
    public virtual void Accept(Visitor v) {
        v.visit(this);
    }
    public abstract string Process();
}

// ConcreteComponent役
public class CleanGuitar : Sound {
    public override string Process() {
        return "    クリーンギター";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

// ConcreteComponent役
public class CleanBass : Sound {
    public override string Process() {
        return "    クリーンベース";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

// Decorator役
public abstract class Effecter : Sound {
    public Sound _sound;
    public Effecter(Sound sound) {
        _sound = sound;
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

// ConcreteDecorator役(エフェクター)

public class Distortion : Effecter {
    public int Gain { get; set; } = 5;
    public Distortion(Sound sound) : base(sound) {
    }
    public override string Process() {
        return _sound.Process() + " → ディストーション ";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

public class Reverb : Effecter {
    public Reverb(Sound sound) : base(sound) { }

    public override string Process() {
        return _sound.Process() + " → リバーブ";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

public class Compresser : Effecter {
    public Compresser(Sound sound) : base(sound) { }

    public override string Process() {
        return _sound.Process() + " → コンプレッサー";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

public class VolumePedal : Effecter {
    public VolumePedal(Sound sound) : base(sound) { }

    public override string Process() {
        return _sound.Process() + " → ボリュームペダル";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

public class Equalizer : Effecter {
    public Equalizer(Sound sound) : base(sound) { }

    public override string Process() {
        return _sound.Process() + " → イコライザー";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

public class Fuzz : Effecter {
    public Fuzz(Sound sound) : base(sound) { }

    public override string Process() {
        return _sound.Process() + " → ファズ";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

public class Tuner : Effecter {
    public Tuner(Sound sound) : base(sound) { }

    public override string Process() {
        return _sound.Process() + " → チューナー";
    }
    public override void Accept(Visitor v) {
        v.visit(this);
    }
}

// メイン テスト実行
public class Program {
    public static void Main() {
        // // 無加工のエレキギター原音
        // Sound clean_guitar = new CleanGuitar();
        // Console.WriteLine(clean_guitar.Process());

        // // 無加工のベース原音
        // Sound clean_bass = new CleanBass();
        // Console.WriteLine(clean_bass.Process());


        // // エフェクターを追加 プリセット的に追加ができる
        // Console.WriteLine("# エレキギターにエフェクターを追加");

        // Sound distortion = new Distortion(new CleanGuitar());
        // Console.WriteLine(distortion.Process());

        // Sound reverb_dist = new Reverb(distortion);
        // Console.WriteLine(reverb_dist.Process());


        // // ベースにエフェクターを追加
        // Console.WriteLine("# ベースにエフェクターを追加");

        // Sound comp_bass = new Compresser(new CleanBass());
        // Console.WriteLine("# シンプルなベースプリセット");
        // Console.WriteLine(comp_bass.Process());

        
        // Console.WriteLine("#シンプルなベースにディストーションを追加");
        // Sound dist_bass = new Distortion(comp_bass);
        // Console.WriteLine(dist_bass.Process());


        //　他色々組み合わせ
        // Console.WriteLine("# 他色々組み合わせ");
        Sound all_preset = new Reverb(new Equalizer(new Fuzz(new Distortion(new CleanGuitar()))));
        // Console.WriteLine(all_preset.Process());

        // Sound all_bass_preset = new VolumePedal(new Compresser(new Equalizer(new Fuzz(new Reverb(new Distortion(new CleanBass()))))));
        // Console.WriteLine(all_bass_preset.Process());

        // // Visitorパターン
        // Console.WriteLine("# Visitorパターン");
        // Visitor visitor = new ControlVisitor();
        // all_preset.Accept(visitor);

        // AltVisitor
        Console.WriteLine("# AltVisitor");
        Visitor altVisitor = new AltVisitor();
        all_preset.Accept(altVisitor);

    }
}