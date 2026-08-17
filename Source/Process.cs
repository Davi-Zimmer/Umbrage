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
        game.Setup();
        
    }

    public void update( float delta ) {
        
        
        // Raylib.ClearBackground( Color.Red );

        // Raylib.DrawText( "Funcionou!", 300, 200, 30, Color.White );


        game.Update( delta );

        if( Raylib.IsWindowResized() ) {
            game.ResizeWindow();
        }

    }

    public void end() {
        game.Finish();
    }
      



}