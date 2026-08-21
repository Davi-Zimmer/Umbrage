namespace Umbrage.Components;

public class Health {

    public List< Action > DeathEvents { get; private set; } = new();
    public List< Action > HurtEvents { get; private set; } = new();
    public int Current { get; private set; } = 10;
    public int Max = 10;

    public Health( int amount, int maxLife = 0 ) {
        
        Current = amount;
        
        if( maxLife <= 0 ) Max = amount;

    }


    public void Heal( int amount ) { Current = Math.Min( Max, amount ); }

    public void ResetToMax() { Current = Max; }

    public void TakeDamage( int amount ) {
        
        Current = Math.Max( 0, Current - amount );

        ExecuteHurtEvents();

        if( Current == 0 ) ExecuteDeathEvents();

    }

    public void ExecuteDeathEvents() {

        foreach( Action action in DeathEvents ) action();

    }

    public void ExecuteHurtEvents() {
        
        foreach( Action action in HurtEvents ) action();
        
    }

}