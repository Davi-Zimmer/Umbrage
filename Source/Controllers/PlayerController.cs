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

    public void KeyUpEvent( KeyboardKey key, Callback action ) {
        
        if( !KeyUp.ContainsKey( key ) ) KeyUp.Add( key, new() );

        KeyUp[ key ].Add( action );

    }

    public void KeyDownEvent( KeyboardKey key, Callback action ) {
        
        if( !KeyDown.ContainsKey( key ) ) KeyDown.Add( key, new() );

        KeyDown[ key ].Add( action );

    }

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

    public void ExecuteRegistredEvents( float delta ) {

        foreach ( var pair in KeyDown ) {
            
            if( !Raylib.IsKeyDown( pair.Key ) ) continue;
        
            foreach ( var callback in pair.Value ) {
                
                callback( delta );

            }    
        
        }

        foreach ( var pair in KeyUp ) {
            
            if( !Raylib.IsKeyUp( pair.Key ) ) continue;

            foreach ( var callback in pair.Value ) {
                
                callback( delta );

            }

        }

    }

    public void ExecuteEvents( float delta ) {
        
        
        
        
    }

}