using System.Numerics;
using System.Reflection.Metadata;
using Umbrage.World.Entity;

namespace Umbrage.Physics;

public class MobileObject {

    GenericEntity Entity;

    public MobileObject( GenericEntity entity ) {
        
        Entity = entity;

    }

    public Vector3 Momentum = new();
    public Vector3 Movement = new();
    public void move( Vector3 vec ) {
        Movement += vec;
    }

    public void UpdateEntityPosition( float delta ) {
        Entity.Position = ( Momentum + Movement ) * delta;
    }


}