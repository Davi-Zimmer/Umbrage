using System.Numerics;
using Raylib_cs;

public class PlayerController {
    
    static PlayerController Instance;

    Player p;

    PlayerController( Player player ) {
        p = player;
    }

    public static void Begin( Player p ) {
        Instance = new( p );
    }

    public static PlayerController Get() {
        return Instance;
    }

    public static KeyboardKey DashKey = KeyboardKey.LeftControl; 

    public Vector3 GetForwardDelta() {

        Vector2 mouseDelta = Raylib.GetMouseDelta();

        p.Yaw   -= mouseDelta.X * 0.003f;
        p.Pitch -= mouseDelta.Y * 0.003f;

        p.Pitch = Math.Clamp( p.Pitch, -1.5f, 1.5f );

        Vector3 forward = new(
            MathF.Cos( p.Pitch ) * MathF.Sin( p.Yaw ),
            MathF.Sin( p.Pitch ),
            MathF.Cos( p.Pitch ) * MathF.Cos( p.Yaw )
        );

        forward = Vector3.Normalize( forward );
        
        return forward;

    }

    public float GetAxis( KeyboardKey positive, KeyboardKey negative, float speed ) {

        float value = 0;

        if( Raylib.IsKeyDown( positive ) ) value += speed;
        if( Raylib.IsKeyDown( negative ) ) value -= speed;

        return value;

    }

    public Vector3 GetMovementDirection( Vector3 forward, Vector3 right, float speed ) {
        
        Vector3 input;

        input.Z = GetAxis( KeyboardKey.W, KeyboardKey.S, speed );
        input.X = GetAxis( KeyboardKey.D, KeyboardKey.A, speed ); 

        Vector3 movement = right * input.X + forward * input.Z;

        return movement * DirectionalDashMultiplyer();

    }

    public float DirectionalDashMultiplyer() {
        
        if( p.CanDash && Raylib.IsKeyDown( DashKey ) && !DashLimit() ) {

            Dash();
            
            return p.DashSpeed;

        }
        
        return 1;
    }

    public static bool IsAnyUpButDashKey() {
        return (
            Raylib.IsKeyDown( DashKey ) &&
            Raylib.IsKeyUp( KeyboardKey.W ) &&
            Raylib.IsKeyUp( KeyboardKey.S ) &&
            Raylib.IsKeyUp( KeyboardKey.A ) &&
            Raylib.IsKeyUp( KeyboardKey.D ) &&
            Raylib.IsKeyUp( KeyboardKey.Space ) 
        );
    }

    public Vector3 GetJumpForce( float speed ) {

        return new Vector3( 0, JumpFactor() * speed, 0 );

    }

    private float JumpFactor() {
        //Console.WriteLine( $"{p.CanJump}, {p.Grounded}" );

        if(  Raylib.IsKeyDown( KeyboardKey.Space ) && p.CanJump && p.Grounded ) {
            p.CanJump = false;

            return p.JumpMultiplyer;
        }

        return 0;
    }

    public bool DashLimit() {

        if( p.DashCooldownCount > 0 ) return true;

        return p.DashCount >= p.MaxDash;
    }

    public Vector3 CheckForwardDash( Vector3 direction ) {
        if( !p.CanDash ) return new Vector3( 0 );

        if( !IsAnyUpButDashKey() ) return new Vector3( 0 );

        if( DashLimit() ) return new Vector3( 0 );

        Dash();

        return direction * p.DashSpeed * p.Speed;


    }

    public void Dash() {
        
        p.DashCount++;

        p.CanDash = false;

        p.DashCooldownCount = p.DashCooldown;

    }

    public void ProcessUpKeys() {
        
        if( Raylib.IsKeyUp( DashKey )           ) p.CanDash = true;
        if( Raylib.IsKeyUp( KeyboardKey.Space ) ) p.CanJump = true;
        
    }

}