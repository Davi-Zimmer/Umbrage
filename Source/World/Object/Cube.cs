namespace Umbrage.World.Object;

using System.Numerics;
using Raylib_cs;

public class Cube: PhysicalObject {

    public Cube( Game game ) : base( game ) {
        
    }

    public void draw() {
        Vector3 size = getSize();
        Raylib.DrawCube( getPosition(), size.X, size.Y, size.Z, getColor() );     

    }

    public override void update( float delta ) {
        
        draw();

        multiplyMovement( .9f, .9f, .9f );

        if( useGravity() ) {
            
            position += getMomentum();
            
            WorldPhysics.AddGravity( this, delta );
            
        }

        
    }

}