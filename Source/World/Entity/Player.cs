using System.Numerics;
using Jitter2.Dynamics;
using Raylib_cs;

public class Player : GenericEntity {
    
    public Camera3D Camera;
    public int DashCooldownCount = 0;
    public int DashCooldown = 0;
    public int MaxDash = 3;
    public int DashCount = 0;
    public float DashSpeed = 30f;
    public float JumpMultiplyer = 15;
    public float Yaw = 0;
    public float Pitch = 0;
    public float Speed = 2;
    public bool CanJump = true;
    public bool Grounded = false;
    public bool CanDash = true;

    public Vector3 CameraOffset = new();

    public Vector3 Forward = new();

    public Player( Map map ) : base( map ) {
        
        RigidBody.SetMassInertia( 10 );

        PlayerController.Begin( this );

        RigidBody.Damping = new( .002f, .001f  );

    }

    public override void PosConfig() {

        RigidBody.Position = Camera.Position;

        Vector3 initialForward = Vector3.Normalize( Camera.Target - Camera.Position );

        Yaw = MathF.Atan2( initialForward.X, initialForward.Z );

        Pitch = MathF.Asin( initialForward.Y );

    }

    private void TickMovement( PlayerController pc, float dt ) {

        pc.ProcessUpKeys();

        Vector3 right       = Vector3.Normalize( Vector3.Cross( Forward, Vector3.UnitY  ) );
        Vector3 forwardFlat = Vector3.Normalize(   new Vector3( Forward.X, 0, Forward.Z ) );

        Vector3 force = new();

        force += pc.CheckForwardDash( forwardFlat );

        force += pc.GetJumpForce( Speed );

        force += pc.GetMovementDirection( forwardFlat, right, Speed );

        RigidBody.ApplyImpulse( force );

        UpdateCamera( Forward );

    }

    public override void Tick( float dt ) {
        
        PlayerController pc = PlayerController.Get();
        
        Forward = pc.GetForwardDelta();
        
        RigidBody.Velocity *= new Vector3( .9f, 1f, .9f );
        
        CheckJumpReset();

        TickMovement( pc, dt );

        DashCooldownCount = Math.Max( DashCooldownCount - 1, 0 );

        if( Raylib.IsMouseButtonPressed( MouseButton.Right ) ) {
            RigidBody.AddForce( (0, JumpMultiplyer*Speed * 100, 0));
        }

    }

    public void UpdateCamera( Vector3 forward ) {
 
        Camera.Position = RigidBody.Position + CameraOffset;
        Camera.Target   = RigidBody.Position + CameraOffset + forward;

    }

    public void CheckJumpReset() {
        
        Grounded = IsGrounded();

        if( Grounded ) DashCount = 0;

    }

    bool IsGrounded(){

        foreach( var arbiter in RigidBody.Contacts ) {

            ref var data = ref arbiter.Handle.Data;

            if( data.Contact0.Normal.Y <= 1f ) return true;
            // if( ( data.UsageMask & 1 ) != 0 && data.Contact0.Normal.Y < -0.5f ) return true;
            // if( ( data.UsageMask & 2 ) != 0 && data.Contact1.Normal.Y < -0.5f ) return true;
            // if( ( data.UsageMask & 4 ) != 0 && data.Contact2.Normal.Y < -0.5f ) return true;
            // if( ( data.UsageMask & 8 ) != 0 && data.Contact3.Normal.Y < -0.5f ) return true;

        }

        return false;
    }

}