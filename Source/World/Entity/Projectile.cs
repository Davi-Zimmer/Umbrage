using System.Runtime.CompilerServices;
using Jitter2.Dynamics;
using Raylib_cs;

public class Projectile: Object {
    
    Object Sender;

    public Projectile( Map map, Object sender ): base( map ) {
        
        Sender = sender;

        RigidBody.BeginCollide += OnCollide;

    }

    public override void Tick( float dt ) {
        

    }

    public override void Render() {
        
        Raylib.DrawCube( RigidBody.Position, .1f, .1f, .1f, Color.Red );

    }

    private void OnCollide( Arbiter arb ) {
        
        RigidBody target = arb.Body1 == RigidBody ? arb.Body2 : arb.Body1;

        GenericEntity? o = Map.FindObjectByRidigBody( target );

        if( o is Player ) {
            
            ((Player)o).Health.Decrease( 5 );

        }

    }

}