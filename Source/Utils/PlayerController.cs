using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata;
using Umbrage.World.Entity;
namespace Umbrage.Utils;

public class PlayerController {
    public static int dashSpeed = 30;
    
    public static KeyboardKey DashButton = KeyboardKey.LeftControl;

    public static Vector3 getForwardDelta( Player player ) {

        Vector2 mouseDelta = Raylib.GetMouseDelta();

        player.yaw   -= mouseDelta.X * 0.003f;
        player.pitch -= mouseDelta.Y * 0.003f;

        player.pitch = Math.Clamp( player.pitch, -1.5f, 1.5f );

        Vector3 forward = new(
            MathF.Cos( player.pitch ) * MathF.Sin( player.yaw ),
            MathF.Sin( player.pitch ),
            MathF.Cos( player.pitch ) * MathF.Cos( player.yaw )
        );

        forward = Vector3.Normalize( forward );
        
        return forward;
    }

    public static Vector3 update( Player player, ref Camera3D cam, float dt ) {

        Vector3 forward = getForwardDelta( player );

        Vector3 right = Vector3.Normalize( Vector3.Cross( forward, Vector3.UnitY ) );

        float speed = 3f;

        if( Raylib.IsKeyUp( DashButton ) )       canUseDash = true;
        if( Raylib.IsKeyUp( KeyboardKey.Space) ) canJump    = true;

        if(  IsAnyUpButLShift() ) dashForward( player, forward * speed * dt );

        Vector3 vec = new();

        if( Raylib.IsKeyDown( KeyboardKey.W     ) )     vec +=  forward;
        if( Raylib.IsKeyDown( KeyboardKey.S     ) )     vec += -forward;
        if( Raylib.IsKeyDown( KeyboardKey.A     ) )     vec += -right  ;
        if( Raylib.IsKeyDown( KeyboardKey.D     ) )     vec +=  right  ;
        if( Raylib.IsKeyDown( KeyboardKey.LeftShift ) ) vec += -new Vector3( 0, 1, 0 );

        if( Raylib.IsKeyDown( KeyboardKey.Space ) && canJump ) vec += Jump( player );
        
        move( player, vec * speed * dt );

        player.position += player.sumMovementVectors();

        cam.Position = player.position + player.getCameraAnchor();
        cam.Target   = player.position + player.getCameraAnchor() + forward;

        return forward;

    }

    public static Vector3 Jump( Player player ) {

        canJump = false;

        player.setGrounded( true );

        return new Vector3( 0, 10, 0 );
        
    }

    public static bool canJump = true;

    public static bool IsAnyUpButLShift() {
        return (
            Raylib.IsKeyDown( DashButton ) &&
            Raylib.IsKeyUp( KeyboardKey.W ) &&
            Raylib.IsKeyUp( KeyboardKey.S ) &&
            Raylib.IsKeyUp( KeyboardKey.A ) &&
            Raylib.IsKeyUp( KeyboardKey.D ) &&
            Raylib.IsKeyUp( KeyboardKey.Space ) 

        );
    }

    public static bool canUseDash = true;

    public static int DashMultiplyer() {

        return willDash() ? dashSpeed : 1;

    }


    public static bool willDash() {
        
        if( Raylib.IsKeyDown( DashButton ) ) {
            
            if( canUseDash ) {
                
                canUseDash = false;

                return true;
            }

        }
        
        return false;

    }

    public static void dashForward( Player player, Vector3 vec ) {
        
        if( willDash() ) player.applyVecMomentum( vec * dashSpeed );

    }


    public static void move( Player player, Vector3 vec ) {
        
        player.applyVecMovement( vec );

    }


}