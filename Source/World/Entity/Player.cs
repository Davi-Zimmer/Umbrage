using System.Numerics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;
using Umbrage.Components;
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

    public Vector3 Forward = new( 0 );
    public Vector3 CameraAnchor = new();
    public float DashSpeed = 20f;
    public Camera3D Camera;
    public new Health Health = new( 100 ); 


    public Player( Map map  ): base( map ) {
        
        PlayerController.CreateInstance( this );
        
        Camera = CreateCamera();

        JumpMultiplyer *= -RigidBody.World.Gravity.Y;

        RegisterCrossHair();

    }

    private void UpdatePlayerMovement( PlayerController pc,  float delta ) {

        Vector3 forward     = Forward;
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
        
        Forward = pc.GetForwardDelta();
        
        RigidBody.Velocity *= new Vector3( .9f, 1f, .9f );

        CheckJumpReset();

        UpdatePlayerMovement( pc, delta );

        DashCooldownCount = Math.Max( DashCooldownCount - 1, 0 );

        pc.MouseInput();


    }

    public override void Render() {
        
    }

    // ------------------------------------ Features Functions ------------------------------------ \\ 
    
    public void Shot(){

        Vector3 from = RigidBody.Position + CameraAnchor;
        Vector3 to = Forward;
        
        Map.RayCast( from, to ).then( shape => {

            RigidBody target = shape.RigidBody;

            Map.FindByRigidBody( target, entity => {
                
                entity.Health?.TakeDamage( 1 );

                Console.WriteLine( entity.Health?.Current );

            });

        });

    }

    
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

    private void RegisterCrossHair() {
        
        Map.pos3DRender.Add(() => {
            int size = 10;
            int width  = Map.Game.innerWidth;
            int height = Map.Game.innerHeight;

            int middleX = width / 2;
            int middleY = height / 2;

            int posY = middleY + size;
            Raylib.DrawLine( middleX, middleY, middleX - size, posY, Color.Red );
            Raylib.DrawLine( middleX, middleY, middleX + size, posY, Color.Red );
        });

    }

    private Camera3D CreateCamera() {
        return new Camera3D(
            RigidBody.Position,
            RigidBody.Position + Vector3.UnitZ,
            Vector3.UnitY,
            90f,
            CameraProjection.Perspective
        );
    }
}