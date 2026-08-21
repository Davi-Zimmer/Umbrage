using System.Numerics;
using Jitter2.Collision;
using Jitter2.LinearMath;

namespace Umbrage.Components;
using Jitter2;
using Jitter2.Collision.Shapes;

public class CastEntity {

    public readonly IDynamicTreeProxy? HitProxy;
    public readonly JVector Normal;
    public readonly bool Hit;
    public readonly float Lambda;

    public delegate void Callback( RigidBodyShape shape );

    public CastEntity( Vector3 origin, Vector3 direction, World world ) {
   
        Hit = world.DynamicTree.RayCast( origin, direction, null, null, out HitProxy, out Normal, out Lambda );

    }

    public void then( Callback cb ) {
        
        if( Hit ) {
        
            if( HitProxy is RigidBodyShape ) cb( (RigidBodyShape)HitProxy );
        
        }
    
    }


}