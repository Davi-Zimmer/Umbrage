using System.Numerics;
using Raylib_cs;

public class Game {

    public static Map Map = new(); 

    public static void Update( float dt ) {
        
        Map.Update( dt );
        // Raylib.DrawText("Hello, World!", 100, 100, 20, Color.White );

    }
    
}