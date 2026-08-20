using System.Numerics;
using Jitter2.Dynamics;
using Raylib_cs;
using Umbrage.Controllers;

namespace Umbrage.World.Entity;

public class Player: GenericEntity {
    
    public float Yaw   = 0;
    public float Pitch = 0;
    public float Speed = 90f;
    public float JumpMultiplyer = 2f;
    public bool CanJump = true;
    public bool Grounded = false;
    public bool CanDash = true;
    public int DashCount = 0;
    public int MaxDash = 3;
    public int DashCooldown = 10;
    public int DashCooldownCount = 0;

    public Vector3 CameraAnchor = new();
    public float DashSpeed = 20f;
    public Camera3D Camera;
    public Player( Map map  ): base( map ) {
        
        PlayerController.CreateInstance( this );
        
        Camera = new Camera3D(
            RigidBody.Position,
            RigidBody.Position + Vector3.UnitZ,
            Vector3.UnitY,
            90f,
            CameraProjection.Perspective
        );

        JumpMultiplyer *= -RigidBody.World.Gravity.Y;

    }

    private void UpdatePlayerMovement( PlayerController pc,  float delta ) {

        Vector3 forward     = pc.GetForwardDelta();
        Vector3 right       = Vector3.Normalize( Vector3.Cross( forward, Vector3.UnitY ) );
        Vector3 forwardFlat = Vector3.Normalize( new Vector3(forward.X, 0, forward.Z) );

        Vector3 force   = new();

        pc.ProcessUpKeys();

        force += pc.CheckForwardDash( forwardFlat );

        force += pc.GetJumpForce();

        force += pc.GetMovementDirection( forwardFlat, right );

        RigidBody.ApplyImpulse( force );

        UpdateCamera( forward );

    }
    
    
    public override void Tick( float delta ) {

        PlayerController pc = PlayerController.GetInstance();
        
        RigidBody.Velocity *= new Vector3( .9f, 1f, .9f );

        CheckJumpReset();

        UpdatePlayerMovement( pc, delta );

        Console.WriteLine( DashCooldownCount );

        DashCooldownCount = Math.Max( DashCooldownCount - 1, 0 );

    }

    
    public override void Render() {
        
    }

    // ------------------------------------ Features Functions ------------------------------------ \\ 
    public void CheckJumpReset() {
        
        Grounded = IsGrounded();

        if( Grounded ) DashCount = 0;

    }

    public void AddDashCooldown() { 
        DashCooldownCount = DashCooldown;
    }

    public void Dash() {

        DashCount++;

        Console.WriteLine( $"Dash { DashCount }" );

        CanDash = false;

        AddDashCooldown();
    }

    bool IsGrounded(){

        foreach (var arbiter in RigidBody.Contacts) {

            ref var data = ref arbiter.Handle.Data;

            if( data.Contact0.Normal.Y < -0.5f ) return true;
            /*
            if( ( data.UsageMask & 1 ) != 0 && data.Contact0.Normal.Y < -0.5f ) return true;
            if( ( data.UsageMask & 2 ) != 0 && data.Contact1.Normal.Y < -0.5f ) return true;
            if( ( data.UsageMask & 4 ) != 0 && data.Contact2.Normal.Y < -0.5f ) return true;
            if( ( data.UsageMask & 8 ) != 0 && data.Contact3.Normal.Y < -0.5f ) return true;
            */

        }

        return false;
    }

    private void UpdateCamera( Vector3 forward ) {
        
        Camera.Position = RigidBody.Position + CameraAnchor;
        Camera.Target   = RigidBody.Position + CameraAnchor + forward;

    }
}