using Raylib_cs;

public class Shaders {
    
    private static Dictionary< Name, Shader > Loaded = new();

    public enum Name {
        Basic,
        Lighting
    }

    public static Shader Get( Name name ) {
        
        return Loaded.TryGetValue( name, out _ ) ? Loaded[ name ] : Loaded[ Name.Basic ];

    }

    public static void Unload(){

        foreach( var e in Loaded ) Raylib.UnloadShader( e.Value ); 

    }

    private static void Setup() {
        
        
        

    }

    public static void Load() {

        string shadersFolder = "./Assets/Shaders/"; 

        Dictionary< Name, string > l = new() {
            [ Name.Basic     ] = "Basic",
            [ Name.Lighting ] = "Lighting"
        };

        foreach( var e in l ) {
            
            string vs = shadersFolder + e.Value + ".vs";
            string fs = shadersFolder + e.Value + ".fs";

            Loaded[ e.Key ] = Raylib.LoadShader( vs, fs );

        }

        Setup();
    }

}