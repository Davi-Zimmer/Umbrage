public class Kaneeth: Enemy {
    K_Perceptron Perceptron;
    K_Brain      Brain;

    public Kaneeth( Map map ): base( map ) {

        Health.Reset( 1000 );

        Health.DecreaseObserver.Add( () => {

            if( Health.Value != 0 ) return;

            Map.RemoveScene( RigidBody );

            Game.SetStatus( Game.Status.Win );

        });

        Perceptron = new( this );
        Brain      = new( this );

    }


}