using System.Numerics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;
using Umbrage.Physics;

namespace Umbrage.World.Entity;

public class GenericEntity {
    
    private Map Map;
    private RigidBody RigidBody;
    
    public MobileObject? mobileObject;


    public GenericEntity( Map map ) {
        
        Map = map;

        RigidBody = map.World.CreateRigidBody();

        ConfigRigidBody();

    }

    public void ConfigRigidBody() {

        RigidBody.Position = Position;

        RigidBody.AddShape( new BoxShape( Size.X, Size.Y, Size.Z ) );

        RigidBody.SetMassInertia( Mass );

    }

    public T Configure<T>( Action<T> callback ) where T: GenericEntity {
        
        callback( (T)this );

        ConfigRigidBody();

        return (T)this;

    }

    public float Mass = 1;

    public Vector3 Position = new( 0, 0, 0 );
    public Vector3 Size = new( 1, 1, 1 );

    public Color Color = Color.Gray;

    public void Update( float delta ) {
        
        Tick( delta );

        Render();

    }

    public virtual void Tick( float delta ) {
        
    }

    public virtual void Render() {
        
        Raylib.DrawCube(
            Position,
            Size.X,
            Size.Y,
            Size.Z,
            Color
        );

    }

}