namespace Umbrage;
using Raylib_cs;


public class Process {
    
    Window window;
    Game game;


    public Process() {

        game = new();

        window = new( update, end, loadTextures );

    }

    
    public void loadTextures() {
        game.setup();
        
    }

    public void update( float delta ) {
        
        
        // Raylib.ClearBackground( Color.Red );

        // Raylib.DrawText( "Funcionou!", 300, 200, 30, Color.White );


        game.update( delta );

        if( Raylib.IsWindowResized() ) {
            game.resizeWindow();
        }

    }

    public void end() {
        game.finish();
    }
      



}