using Raylib_cs;

namespace Umbrage.Graphycs;

public class Shaders {

    private static Dictionary< Names, Shader > Loaded = new();

    public enum Names {
        Sky,
        Test
    }

    
    public static void AddShader( Names name, string vertexShader, string fragmentShader ) {
        
        Loaded.Add( 
            name,
            Raylib.LoadShader( vertexShader, fragmentShader )
        );

    }

    public static Shader GetShader( Names name ) {
        
        return Loaded[name];

    }

    public static void Load() {
        
        string base_ = Directory.GetCurrentDirectory() + "/Assets/Shaders";

        AddShader(
            Names.Sky,
            base_ + "/Sky/Skybox.vs",
            base_ + "/Sky/Skybox.fs"
        );

        AddShader(
            Names.Test,
            base_ + "/Test/Test.vs",
            base_ + "/Test/Test.fs"
        );

    }

};