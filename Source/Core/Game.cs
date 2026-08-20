using Raylib_cs;

namespace Umbrage;

public class Game {
    
    public int innerWidth  = 0;
    public int innerHeight = 0;

    private Map Map;

    public Game() {
        Map = new( this );
        
    }
    

    public void Setup() {

        Raylib.DisableCursor();
        ResizeWindow();

        //string path = Directory.GetCurrentDirectory() + "/Assets/placeholder.png";
        // spritesheet = Raylib.LoadTexture( path );
        // Raylib.SetTextureFilter( spritesheet, TextureFilter.Point );
        // configCamera();
    }


    public void ResizeWindow() {
        innerWidth  = Raylib.GetScreenWidth();
        innerHeight = Raylib.GetScreenHeight();
        // cam.Offset  = new Vector2( innerWidth / 2 , innerHeight / 2 );
    }


    public void Finish() {

        /// Raylib.UnloadTexture( spritesheet );

    }

    public void Update( float delta ) {
        
        Raylib.ClearBackground( new Color( 20, 20, 50 ) );

        Map.Update( delta );

    }

}