using Raylib_cs;

public class Window {

    public static void Config() {

        Raylib.SetConfigFlags(
            ConfigFlags.MaximizedWindow |
            ConfigFlags.MinimizedWindow |
            ConfigFlags.ResizableWindow
            
        );
        
        Raylib.InitWindow( 800, 600, "Umbrage" );

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