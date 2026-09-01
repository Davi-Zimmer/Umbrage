using System.Diagnostics;
namespace Umbrage;

using Raylib_cs;

public class Window {
    
    public delegate void Loop( float delta );
    public delegate void Func();

    public Window( Loop loop, Func end, Func config ) {

        configuration();

        config();
        
        startLoop( loop, end );

    }

    private void configuration() {
        Raylib.SetConfigFlags( ConfigFlags.ResizableWindow );
        Raylib.InitWindow( 800, 450, "Umbrage" );
        Raylib.SetTargetFPS( 60 );


        string path = Directory.GetCurrentDirectory() + "/Assets/placeholder.png";
        
    }

    private void startLoop( Loop loop, Func end ) {

        Stopwatch timer = new();

        timer.Start();

        double lastTime = timer.Elapsed.TotalSeconds;

        while( !Raylib.WindowShouldClose() ) {
            
            double currentTime = timer.Elapsed.TotalSeconds;

            double deltaTime = currentTime - lastTime;

            lastTime = currentTime;

            Raylib.BeginDrawing();
        
            loop( (float)deltaTime );

            Raylib.EndDrawing();

        }

        end();

        Raylib.CloseWindow();

    }

    
}