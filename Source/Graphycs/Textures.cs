using System.Numerics;
using Raylib_cs;

namespace Umbrage.Graphycs;

public class Textures {
    
    public enum Names {
        Crosshair,
        SkyBox,
        Grass
    }
    
    private Textures() {}

    public static Dictionary< Names, Texture2D > Loaded2D = new();

    private static void AddToLoaded2D( Names name, string path ) {
        
        Loaded2D.Add( name, Raylib.LoadTexture( path ) );
        
    }

    public static void Load() {

        string base_ = Directory.GetCurrentDirectory() + "/Assets";

        AddToLoaded2D( Names.Crosshair, base_ + "/crosshair.png" );

        AddToLoaded2D( Names.SkyBox, base_ + "/skybox.png" );

        AddToLoaded2D( Names.Grass, base_ + "/grass.png" );


    }

    public static void Unload() {

        foreach ( var x in Loaded2D ) {
            Raylib.UnloadTexture( x.Value );
        }

    }

    public static void Render2D( Names name, int x, int y, float scale, Color color ) {
        
         Raylib.DrawTextureEx(
            Loaded2D[ name ],
            new Vector2( x, y ),
            0,
            scale,
            color

        );

    }

    public static void Render2D( Texture2D texture, int x, int y, float scale, Color color ) {
        
        Raylib.DrawTextureEx(
            texture,
            new Vector2( x, y ),
            0,
            scale,
            color

        );
    }

    public static Texture2D GetTexture2D( Names name ) {
        
        return Loaded2D[ name ];

    }
}