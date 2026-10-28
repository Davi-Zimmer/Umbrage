public class Bar {
    
    public virtual int Value { get; protected set; } = 0;
    public virtual int Max { get; set; } = 0;
    public bool Changable = true;

    public Observer DecreaseObserver = new();
    public Observer IncreaseObserver = new();
    public Observer FilledObserver   = new();
    public Observer ZeroObserver     = new();
    
    public Bar( int amount, int maxlife = 0 ) {
        
        //Max = maxlife == 0 ? amount : maxlife; 
        // Value = amount;
        Reset( amount, maxlife );

    }

    public void Set( int amount ) { Value = amount; }

    public void Reset( int amount, int maxlife = 0 ) {
        Max = maxlife == 0 ? amount : maxlife; 
        Value = amount;
    }

    public void Zero() { Value = 0; }

    public void Fill() { Value = Max; }

    public void Increase( int amount ) {
        
        if( !Changable ) return;

        Value = Clamp( Value + amount, Max );

        IncreaseObserver.Trigger();

        if( Value == Max ) FilledObserver.Trigger();

    }

    public void Change( int i ) {

        if( i < 0 ) Decrease( i );
        else        Increase( i );

    }

    public void Decrease( int amount ) {

        if( !Changable ) return;

        Value = Clamp( Value - amount, Max );

        DecreaseObserver.Trigger();

        if( Value == 0 ) ZeroObserver.Trigger();

    }

    public static int Clamp( int i, int max ) {
        
        return Math.Max( Math.Min( i, max ), 0 );

    }

}


public class BarF {
    
    public virtual float Value { get; protected set; } = 0;
    public virtual float Max { get; set; } = 0;
    public bool Changable = true;

    public Observer DecreaseObserver = new();
    public Observer IncreaseObserver = new();
    public Observer FilledObserver   = new();
    public Observer ZeroObserver     = new();

    public BarF( float amount, float maxlife = 0 ) {
        
        Max = maxlife == 0 ? amount : maxlife; 
        
        Value = amount;

        Reset( amount, maxlife );

    }

    public void Set( float amount ) { Value = amount; }
    
    public void Reset( float amount, float maxlife = 0 ) {
        Max = maxlife == 0 ? amount : maxlife;
        Value = amount;
    }

    public void Zero() { Value = 0; }
    public void Fill() { Value = Max; }

    public void Increase( float amount ) {
        
        if( !Changable ) return;

        Value = Clamp( Value + amount, Max );

        IncreaseObserver.Trigger();

        if( Value == Max ) FilledObserver.Trigger();

    }
    public void Change( float i ) {

        if( i < 0 ) Decrease( i );
        else        Increase( i );

    }

    public void Decrease( float amount ) {

        if( !Changable ) return;

        Value = Clamp( Value - amount, Max );

        DecreaseObserver.Trigger();

    }

    public static float Clamp( float i, float max ) {
        
        return MathF.Max( MathF.Min( i, max ), 0 );

    }

}