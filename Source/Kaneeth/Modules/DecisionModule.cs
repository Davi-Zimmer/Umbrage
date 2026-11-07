public class DecisionModule {
    
    public Kaneeth Kaneeth;

    public DecisionModule( Kaneeth kaneeth ) {
        Kaneeth = kaneeth;
    }

    public List<Decision> Decisions = new();
    public float SwitchThreshold = 10f;
    public Decision Current;

    public void Add( Decision decision ) { Decisions.Add( decision ); }

    public void Tick( float delta ){

        Decision bestDecision = null!;

        float bestScore = float.MinValue;

        foreach( Decision decision in Decisions ) {
            
            float score = decision.Evaluate();

            if( score > bestScore ) {
                bestScore = score;
                bestDecision = decision;
            }

        }

        if( Current == null || !Current.CanContinue() || bestDecision != Current ) {
            Current = bestDecision;
        } else {
            float currentScore = Current.Evaluate();

            bool canContinue = Current.CanContinue();

            bool shouldSwitch = bestDecision != null 
            && bestDecision != Current
            && bestScore > currentScore + SwitchThreshold;


            if( !canContinue || shouldSwitch ) {
                Current = bestDecision!;
            }

        }

        Current?.Execute();

    }

}


