public class Bar {
    
    public int Value { get; private set; } = 0;
    public int Max = 0;
    public bool Changable = true;

    public Observer DecreaseObserver = new();
    public Observer IncreaseObserver = new();
    
    public Bar( int amount, int maxlife = 0 ) {
        
        Max = maxlife == 0 ? amount : maxlife; 
        
        Value = amount;

    }

    public void Set( int amount ) { Value = amount; }

    public void Fill() { Value = Max; }

    public void Increase( int amount ) {
        
        if( !Changable ) return;

        Value = Clamp( Value + amount );

        IncreaseObserver.Trigger();

    }

    public void Decrease( int amount ) {

        if( !Changable ) return;

        Value = Clamp( Value - amount );

        DecreaseObserver.Trigger();

    }

    private int Clamp( int i ) {
        
        return Math.Max( Math.Min( i, Max ), 0 );

    }

}