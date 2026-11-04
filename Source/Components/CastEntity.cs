
using System.Numerics;
using Jitter2;
using Jitter2.Collision;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;

public class CastEntity {

    public IDynamicTreeProxy? HitProxy;
    public JVector Normal;
    public  bool Hit;
    public float Lambda;

    public delegate void Callback( RigidBodyShape shape );
    Vector3 Origin;
    Vector3 Direction;

    RigidBody? ignore = null;

    public CastEntity( Vector3 origin, Vector3 direction ) {

        Origin    = origin;
        Direction = direction;

    }

    public CastEntity Ignore( RigidBody body ) {
        ignore = body;
        
        return this;
    }

    public CastEntity Start() {
        
        Hit = Game.Map.World.DynamicTree.RayCast(
            Origin,
            Direction,
            proxy => proxy_( proxy ),
            null,
            out HitProxy, out Normal, out Lambda
        );
        
        return this;
    }

    private bool proxy_( IDynamicTreeProxy proxy ) {
        if( proxy is RigidBodyShape shape ) return shape.RigidBody != ignore;

        return true;
    }

    public void Then( Callback cb ) {
        
        if( Hit ) {
        
            if( HitProxy is RigidBodyShape ) cb( (RigidBodyShape)HitProxy );
        
        }
    
    }

}