using Raylib_cs;

public class Fonts {
    public static Font Default;
    public static float Spacing = 5;
    
    public static void Load() {
        Default = Raylib.LoadFont( "Assets/Font/Brang.ttf" );
    }

    public static void Unload() {
        Raylib.UnloadFont( Default );
    }

}