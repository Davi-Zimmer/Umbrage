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

        if( Raylib.IsKeyDown( positive ) ) value += 1;
        if( Raylib.IsKeyDown( negative ) ) value -= 1;

        return value;

    }

    public Vector3 GetMovementDirection( Vector3 forward, Vector3 right ) {
        Vector3 input;

        input.Z = GetAxis( KeyboardKey.W, KeyboardKey.S );
        input.X = GetAxis( KeyboardKey.D, KeyboardKey.A );
        input.Y = Raylib.IsKeyDown( KeyboardKey.Space ) ? 1 : 0;

        Vector3 movement = right * input.X + forward * input.Z + Vector3.UnitY * input.Y;

        return movement;
    }

    public void CheckForwardDash( Vector3 direction ) {

        if( !Player.CanDash ) return;

        if( !IsAnyUpButDashKey() ) return;

        Player.mobileObject.move( direction * Player.DashSpeed );

        Player.CanDash = false;

    }

    public float DirectionalDashMultiplyer() {
        
        if( Player.CanDash && Raylib.IsKeyDown( DashKey ) ) {

            Player.CanDash = false;
            
            return Player.DashSpeed;

        }
        
        return 1;
    }

    public void ProcessUpKeys() {
        
        if( Raylib.IsKeyUp( DashKey ) ) Player.CanDash = true;
        
    }

}