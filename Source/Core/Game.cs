using System.Numerics;
using Raylib_cs;

public class Game {

    public static Map map = new(); 

    public static void Update( float dt ) {
        
        Raylib.ClearBackground( Color.Black );

        Raylib.DrawText("Hello, World!", 100, 100, 20, Color.White );

    }
    
}