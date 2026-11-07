using System.Numerics;
using Raylib_cs;

public class Letter {
    public static Font Default;
    public static float Spacing = 5;
    
    public static void Load() {
        Default = Raylib.LoadFont( "Assets/Font/Brang.ttf" );
    }

    public static void Unload() {
        Raylib.UnloadFont( Default );
    }

    public static void DrawTextEx( string text, Vector2 position, float fontSize, float spacing, Color color ) {
        Raylib.DrawTextEx( Letter.Default, text, position, fontSize, spacing, color  );
 
    }

    public static void DrawText( string text, float x, float y, float fontSize, Color color ) {
        Raylib.DrawTextEx( Letter.Default, text, new( x, y ), fontSize, Letter.Spacing, color );
    }
}