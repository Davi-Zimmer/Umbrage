using Raylib_cs;

public class Window {

    public static void Config() {

        Raylib.SetConfigFlags(
            // ConfigFlags.MaximizedWindow |
            // ConfigFlags.MinimizedWindow |
            ConfigFlags.ResizableWindow 
            // | ConfigFlags.Msaa4xHint
            
        );
        
        Raylib.InitWindow( 900, 600, "Umbrage" );

        Raylib.SetTargetFPS( 60 );

    }
    
    public delegate void Update( float delta );

    public static void StartUpdate( Update update ) {

        while( !Raylib.WindowShouldClose() ) {
            
            Raylib.BeginDrawing();
            
            update( Raylib.GetFrameTime() );

            Raylib.EndDrawing();

        }

    }

}