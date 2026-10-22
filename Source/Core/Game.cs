using System.Net;
using System.Numerics;
using Raylib_cs;

public class Game {

    public static Map Map = new();
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
        t.MaxObserver.Add( () => t.Zero() );
    });

    private static Tween count = new Tween( 50, 1 ).Configure( t => {
        t.MaxObserver.Add( () => {
            dotCount.CallBehaivor( Tween.Grown );
            t.Zero();
        });
    });

    private static Tween delay = new Tween( 100, 1 ).Configure( t => {
        t.MaxObserver.Add( () => {
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

        Raylib.DrawText( message, x, y, fontSize, new Color(
            255, 255, 255, alpha.Value
        ));

    }

    private static void Win() {

        int fontSize = 70;

        string message = "Win";
        
        GetMiddleText( message, fontSize, out int x , out int y );

        Raylib.ClearBackground( Color.White );

        Raylib.DrawText( message, x, y, fontSize, Color.Black );
    }

    private static void GetMiddleText( string s, int fontSize, out int x, out int y ) {
        int textWidth = Raylib.MeasureText( s, fontSize );
        x = ( Raylib.GetScreenWidth () - textWidth ) / 2;
        y = ( Raylib.GetScreenHeight() - fontSize  ) / 2;
    }

    public static void Update() {

        switch( status ) {
            
            case Status.Defeat : Defeat(); return;
            case Status.Win    : Win(); return;

        }

        Map.Update();

    }
    
}