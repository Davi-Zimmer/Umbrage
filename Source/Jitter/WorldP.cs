namespace Umbrage.Jitter;

using Jitter2;
using Jitter2.Dynamics;
using Jitter2.Collision.Shapes;
using System.Numerics;
using Umbrage.World.Entity;
using Raylib_cs;

public class WorldP {
    
    private World world = new();

    private List<RigidBody> items = new();

    public WorldP( Game game ) {

        RigidBody ground = world.CreateRigidBody(); 
        ground.AddShape( new BoxShape( 100, 1, 100 ) );
        ground.Position = new Vector3( 0, -1, 0 );

        RigidBody box = world.CreateRigidBody();
        
        box.AddShape( new BoxShape( 1f, 1f, 1f ) );
        box.Position = new Vector3( 0, 5f, 0 );
        box.SetMassInertia( 1.0f );

        items.Add( ground );
        items.Add( box );

    }

    public void update( float delta ) {
        
        foreach ( RigidBody box in items ) {
             Raylib.DrawCube(
                box.Position,
                1f,
                1f,
                1f,
                Color.Red
            );
        }

        world.Step( delta, true );


    }


}