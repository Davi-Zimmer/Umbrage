using System.Numerics;

public class MemoryModule {
    private Kaneeth Kaneeth;

    public Vector3 LastPlayerPosition = new();
    public int TimeSinceLastSeen = 0;
    public bool PlayerVisible = false;
    public float LastPlayerDistance = 0;

    public MemoryModule( Kaneeth kaneeth ) {
        
        Kaneeth = kaneeth;

    }

    public void Tick() {
        PerceptronModule perc = Kaneeth.Perceptron;

        UpdateVision( perc );
    
    }

    private void UpdateVision( PerceptronModule perc ) {
        
        PlayerVisible = perc.PlayerVisible;

        if( PlayerVisible ) {
            LastPlayerPosition = Game.Map.Player.RigidBody.Position;
            LastPlayerDistance = perc.PlayerDistance;
            TimeSinceLastSeen  = 0;
            
            return;
        }

        TimeSinceLastSeen++;

    }
    
}