using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Raylib_cs;
using Umbrage.Controllers;

namespace Umbrage.World.Entity;

public class Player: GenericEntity {
    
    public float Yaw   = 0;
    public float Pitch = 0;

    public Camera3D Camera;
    public Vector3 Momentum = new();
    public Vector3 Movement = new();

    public Player( Map map  ): base( map ) {
        
        PlayerController pc = PlayerController.CreateInstance( this );

        Camera = new Camera3D(
            Position,
            Position + Vector3.UnitZ,
            Vector3.UnitY,
            90f,
            CameraProjection.Perspective
        );

    }

    private void RegisterMovementEvents( PlayerController pc,  float delta ) {

        Vector3 forward = pc.GetForwardDelta();

        Vector3 right   = Vector3.Normalize( Vector3.Cross( forward, Vector3.UnitY ) );

        Vector3 vec = new();

        float speed = 3;

        if( Raylib.IsKeyDown( KeyboardKey.W ) )   vec +=  forward;
        if( Raylib.IsKeyDown( KeyboardKey.S ) )   vec += -forward;
        if( Raylib.IsKeyDown( KeyboardKey.A ) )   vec += -right  ;
        if( Raylib.IsKeyDown( KeyboardKey.D ) )   vec +=  right  ;

        vec *= speed;
        
        Position +=  delta * (vec + Momentum);
        Console.WriteLine( Position );
        Camera.Position = Position;
        Camera.Target   = Position + forward;
    }

    public override void Tick( float delta ) {
        
        PlayerController pc = PlayerController.GetInstance();

        // pc.ExecuteEvents( delta );

        RegisterMovementEvents( pc, delta );

        // pc.ExecuteRegistredEvents( delta );

        Momentum *= .9f;
    }


    
    public override void Render() {
        
    }

}