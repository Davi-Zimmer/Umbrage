using System.Numerics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;
using Umbrage.Components;

namespace Umbrage.World.Entity;

public class GenericEntity {
    
    public Map Map;
    public Health? Health;
    public RigidBody RigidBody;

    public GenericEntity( Map map ) {
        
        Map = map;

        RigidBody = map.World.CreateRigidBody();

        ConfigRigidBody();

    }

    public void ConfigRigidBody() {

        RigidBody.AddShape( new BoxShape( Size.X, Size.Y, Size.Z ) );

    }

    private void updateRigidBoryMass() { RigidBody.SetMassInertia( Mass ); }


    public T Configure<T>( Action<T> callback ) where T: GenericEntity {
        
        callback( (T)this );

        ConfigRigidBody();

        updateRigidBoryMass();

        return (T)this;

    }

    public float Mass = 1;

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
            RigidBody.Position,
            Size.X,
            Size.Y,
            Size.Z,
            Color
        );

    }

}