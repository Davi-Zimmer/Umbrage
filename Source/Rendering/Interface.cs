using Raylib_cs;
using Umbrage.Graphycs;

namespace Umbrage.Rendering;

public class Interface {
    
    public Game Game;

    public Interface( Game game ) {
        Game = game;
    }

    public void RenderCrosshair() {
        
        Texture2D crosshair = Textures.GetTexture2D( Textures.Names.Crosshair );

        float size = .1f;

        int x = Game.innerWidth  / 2 - (int)((crosshair.Width / 2) * size);
        int y = Game.innerHeight / 2 ;

        Textures.Render2D( crosshair, x, y, size , Color.White );
        
    }
    
}