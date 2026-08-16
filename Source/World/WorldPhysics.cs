using Umbrage.World.Object;

namespace Umbrage.World;

public abstract class WorldPhysics {
    
    public static float Gravity = -.2f;

    public static void AddGravity( PhysicalObject obj, float delta ) {
        
        if( obj.getGrounded() ) obj.applyMomentum( 0, Gravity * delta, 0 );
        
    }

}