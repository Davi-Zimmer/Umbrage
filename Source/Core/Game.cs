using System.Numerics;
using Raylib_cs;

public class Game {

    public static Map Map = new(); 

    public static void Update() {
        
        Map.Update();

    }
    
}