using System.Numerics;
using Raylib_cs;
using Umbrage.Controllers;
using Umbrage.Physics;

namespace Umbrage.World.Entity;

public class Player: GenericEntity {
    
    public float Yaw   = 0;
    public float Pitch = 0;
    public float Speed = .2f;
    public bool CanDash = true;

    public float DashSpeed = 50;

    public new MobileObject mobileObject;

    public Camera3D Camera;
    public Player( Map map  ): base( map ) {
        
        PlayerController pc = PlayerController.CreateInstance( this );

        Camera = new Camera3D(
            Position,
            Position + Vector3.UnitZ,
            Vector3.UnitY,
            90f,
            CameraProjection.Perspective
        );

        mobileObject = new( this );

    }

    private void UpdatePlayerMovement( PlayerController pc,  float delta ) {

        // Vector3 forwardFlat = new Vector3(forward.X, 0, forward.Z);
        // forwardFlat = Vector3.Normalize( forwardFlat );

        Vector3 forward = pc.GetForwardDelta();
        Vector3 right   = Vector3.Normalize( Vector3.Cross( forward, Vector3.UnitY ) );
        Vector3 force   = new();

        pc.ProcessUpKeys();

        pc.CheckForwardDash( forward );
        
        force += pc.GetMovementDirection( forward, right );

        force *= pc.DirectionalDashMultiplyer();

        mobileObject.move( force );

        mobileObject.UpdateEntityPosition( delta );        

        UpdateCamera( forward );
  
    }

    private void UpdateCamera( Vector3 forward ) {
        
        Camera.Position = Position;
        Camera.Target   = Position + forward;

    }

    public override void Tick( float delta ) {
        
        PlayerController pc = PlayerController.GetInstance();

        // pc.ExecuteEvents( delta );

        UpdatePlayerMovement( pc, delta );
        
        mobileObject.Momentum *= .9f;
        mobileObject.Movement *= .9f;

        // pc.ExecuteRegistredEvents( delta );

    }

    
    public override void Render() {
        
    }

}