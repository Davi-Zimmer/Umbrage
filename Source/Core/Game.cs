using Raylib_cs;

namespace Umbrage;

public class Game {
    
    private int innerWidth  = 0;
    private int innerHeight = 0;

    Map map;

    public Game() {
        map = new Map( this );
    }
    

    public void setup() {

        Raylib.DisableCursor();
        resizeWindow();

        //string path = Directory.GetCurrentDirectory() + "/Assets/placeholder.png";
        // spritesheet = Raylib.LoadTexture( path );
        // Raylib.SetTextureFilter( spritesheet, TextureFilter.Point );
        // configCamera();
    }


    public void resizeWindow() {
        innerWidth  = Raylib.GetScreenWidth();
        innerHeight = Raylib.GetScreenHeight();
        // cam.Offset  = new Vector2( innerWidth / 2 , innerHeight / 2 );
    }


    public void finish() {

        /// Raylib.UnloadTexture( spritesheet );

    }

    public void update( float delta ) {
        
        Raylib.ClearBackground( new Color( 20, 20, 50 ) );

        //Raylib.DrawText( "Hello, World!", innerWidth / 2, innerHeight / 2, 20, Color.Purple );
        Raylib.BeginMode3D( map.player.camera );

        map.update( delta );

        Raylib.EndMode3D();

    }

}