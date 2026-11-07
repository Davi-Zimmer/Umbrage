using System.ComponentModel;
using System.Net;
using System.Numerics;
using Jitter2;
using Raylib_cs;

public class Game {

    public static Map Map = new();

    public static void Setup() {



    }

    public static void Load() {
        Version.Load();
        Texture.Load();
        Shaders.Load();
        Letter.Load();
    }

    public static void Unload() {
        Texture.UnLoad();
        Shaders.Unload();
        Letter.Unload();
    }

    public static Status status = Status.Running;

    public enum Status {
        Paused,
        Running,
        Defeat,
        Win
    }

    public static void SetStatus( Status s ) {
        status = s;
    }

    public static Status GetStatus() {
        return status;
    }

    private static Tween dotCount = new Tween( 4, 1 ).Configure( t => {
        t.FilledObserver.Add( () => t.Zero() );
    });

    private static Tween count = new Tween( 50, 1 ).Configure( t => {
        t.FilledObserver.Add( () => {
            dotCount.CallBehaivor( Tween.Grown );
            t.Zero();
        });
    });

    private static Tween delay = new Tween( 100, 1 ).Configure( t => {
        t.FilledObserver.Add( () => {
            count.CallBehaivor( Tween.Grown );
        });
    });

    private static TweenF alpha = new TweenF( 255, 0.01f );
    
    private static void Defeat() {

        alpha.CallBehaivor( TweenF.Grown );
        delay.CallBehaivor( Tween.Grown );

        string dots = String.Concat( Enumerable.Repeat( ".", dotCount.Value ) );

        string message =  "Defeat" + dots;
        
        int fontSize = 70;
        
        GetMiddleText( message, fontSize, out int x , out int y );
        
        Raylib.ClearBackground( Color.Black );

        Letter.DrawText( message, x, y, fontSize, new Color(
            255, 255, 255, alpha.Value
        ));

    }

    private static void Win() {

        int fontSize = 70;

        string message = "Win";
        
        GetMiddleText( message, fontSize, out int x , out int y );

        Raylib.ClearBackground( Color.White );

        Letter.DrawText( message, x, y, fontSize, Color.Black );
    }

    private static void GetMiddleText( string s, int fontSize, out int x, out int y ) {
        int textWidth = Raylib.MeasureText( s, fontSize );
        x = ( Raylib.GetScreenWidth () - textWidth ) / 2;
        y = ( Raylib.GetScreenHeight() - fontSize  ) / 2;
    }

    public static void Update() {

        // Letter.DrawText( "Hello World!", 10, 20, 20, Color.White );
        // Letter.DrawText( "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10, 50, 20, Color.White );
        // Letter.DrawText( "abcdefghijklmnopqrstuvwxyz", 10, 80, 20, Color.White );
        // Letter.DrawText( "-+=/|\\{}()[]<>:;'`?!", 10, 110, 20, Color.White );
        // Letter.DrawText( "0123456789", 10, 130, 20, Color.White );
        // return;

        switch( status ) {
            
            case Status.Defeat : Defeat(); return;
            case Status.Win    : Win(); return;

        }

        Map.Update();
        
    }
    
}