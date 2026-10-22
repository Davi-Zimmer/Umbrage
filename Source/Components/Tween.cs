public class Tween : Bar {
    public delegate int Behaivor_();
    public delegate Behaivor_ Behaivor( Tween t );
    public delegate void CFG( Tween t );
    private int Multiplyer = 1; 

    public int Enhancer;

    public Tween( int max, int enhancer ): base( 0, max ) {
        
        Enhancer = enhancer;

    }

    public Tween Configure( CFG cfg ) {
        
        cfg( this );

        return this;
    }
    
    public void CallBehaivor( Behaivor cb ) {
        Behaivor_ b = cb( this );
        
        Change( b() );

    }

    public void Invert() { Multiplyer *= -1; }

    public static Behaivor_ Grown( Tween t ) {

        Behaivor_ b = () => t.Enhancer;

        return b;
        
    }

    public static Behaivor_ Boomerang( Tween t ) {
        
        Behaivor_ b = () => t.Enhancer * t.Multiplyer;

        return b;

    }

}

public class TweenF : BarF {
    public delegate float Behaivor_();
    public delegate Behaivor_ Behaivor( TweenF t );
    public delegate void CFG( TweenF t ); 

    public float Enhancer;

    public TweenF( float max, float enhancer ): base( 0, max ) {
        
        Enhancer = enhancer;

        CallBehaivor( TweenF.Sin );

    }
    
    public TweenF Configure( CFG cfg ) {
        
        cfg( this );

        return this;
    }
    
    public void CallBehaivor( Behaivor cb ) {
        Behaivor_ b = cb( this );
        
        Value = b();

    }

    public static Behaivor_ Sin( TweenF t ) {
        
        Behaivor_ b = () => MathF.Sin( t.Enhancer + t.Value );

        return b;

    }

    public static Behaivor_ Grown( TweenF t ) {
        
        Behaivor_ b = () => Clamp( t.Value + t.Enhancer, t.Max );

        return b;
        
    }

}