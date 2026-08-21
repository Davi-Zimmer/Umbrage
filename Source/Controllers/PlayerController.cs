using System.Dynamic;
using System.Numerics;
using Raylib_cs;
using Umbrage.World.Entity;

namespace Umbrage.Controllers;

public class PlayerController {

    private PlayerController( Player player ) {
        Player = player;
    }

    private static PlayerController? Instance;
    public static PlayerController GetInstance() {

        if( Instance == null ) throw new Exception("Use \"CreateInstance( Player p )\" before GetInstance() call ");
        
        return Instance;
        
    }

    public static PlayerController CreateInstance( Player p ) {

        if( Instance == null ) Instance = new PlayerController( p );

        return Instance;
    }

    private Player Player;

    public delegate void Callback( float delta );

    private Dictionary< KeyboardKey, List<Callback> > KeyDown = new();
    private Dictionary< KeyboardKey, List<Callback> > KeyUp   = new();
    
    public Vector3 GetForwardDelta() {

        Vector2 mouseDelta = Raylib.GetMouseDelta();

        Player.Yaw   -= mouseDelta.X * 0.003f;
        Player.Pitch -= mouseDelta.Y * 0.003f;

        Player.Pitch = Math.Clamp( Player.Pitch, -1.5f, 1.5f );

        Vector3 forward = new(
            MathF.Cos( Player.Pitch ) * MathF.Sin( Player.Yaw ),
            MathF.Sin( Player.Pitch ),
            MathF.Cos( Player.Pitch ) * MathF.Cos( Player.Yaw )
        );

        forward = Vector3.Normalize( forward );
        
        return forward;

    }

    public KeyboardKey DashKey = KeyboardKey.LeftControl;

    public bool IsAnyUpButDashKey() {
        return (
            Raylib.IsKeyDown( DashKey ) &&
            Raylib.IsKeyUp( KeyboardKey.W ) &&
            Raylib.IsKeyUp( KeyboardKey.S ) &&
            Raylib.IsKeyUp( KeyboardKey.A ) &&
            Raylib.IsKeyUp( KeyboardKey.D ) &&
            Raylib.IsKeyUp( KeyboardKey.Space ) 
        );
    }

    public float GetAxis( KeyboardKey positive, KeyboardKey negative ) {

        float value = 0;

        if( Raylib.IsKeyDown( positive ) ) value += Player.Speed;
        if( Raylib.IsKeyDown( negative ) ) value -= Player.Speed;

        return value;

    }

    private float JumpFactor() {

        if(  Raylib.IsKeyDown( KeyboardKey.Space ) && Player.CanJump && Player.Grounded ) {

            Player.CanJump = false;

            return Player.JumpMultiplyer;
        }

        return 0;
    }

    public Vector3 GetMovementDirection( Vector3 forward, Vector3 right ) {
        
        Vector3 input;

        input.Z = GetAxis( KeyboardKey.W, KeyboardKey.S );
        input.X = GetAxis( KeyboardKey.D, KeyboardKey.A ); 

        Vector3 movement = right * input.X + forward * input.Z;

        return movement * DirectionalDashMultiplyer();
    }

    public Vector3 GetJumpForce() {

        return new Vector3( 0, JumpFactor() * Player.Speed, 0 );

    }

    public bool DashLimit() {

        if( Player.DashCooldownCount > 0 ) return true;

        return Player.DashCount >= Player.MaxDash;
    }

    public Vector3 CheckForwardDash( Vector3 direction ) {

        if( !Player.CanDash ) return new Vector3( 0 );

        if( !IsAnyUpButDashKey() ) return new Vector3( 0 );

        if( DashLimit() ) return new Vector3( 0 );

        Player.Dash();

        return direction * Player.DashSpeed * Player.Speed;

    }

    public float DirectionalDashMultiplyer() {
        
        if( Player.CanDash && Raylib.IsKeyDown( DashKey ) && !DashLimit() ) {

            Player.Dash();
            
            return Player.DashSpeed;

        }
        
        return 1;
    }

    public void ProcessUpKeys() {
        
        if( Raylib.IsKeyUp( DashKey )           ) Player.CanDash = true;
        if( Raylib.IsKeyUp( KeyboardKey.Space ) ) Player.CanJump = true;
        
    }

    public void MouseInput() {
        
        if( Raylib.IsMouseButtonPressed( MouseButton.Left ) ) Player.Shot();
        
    }


}