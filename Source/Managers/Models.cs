using Raylib_cs;

public class Model3D {
    
    public static Dictionary< Names, string > ModelPaths = new() {
        [ Names.Null  ] = "./Assets/Private/Untitled.glb",
        [ Names.Enemy ] = "./Assets/Private/Enemy.glb"
    };

    public static Dictionary< Names, Model > modelsss= new();

    public static void LoadModels() {
        foreach( var s in ModelPaths ) {
            modelsss[s.Key] = Raylib.LoadModel( s.Value );
        }
    }

    public enum Names {
        Null,
        Enemy
    }

    public static Model Get( Names name ) {
        
        string modelPath = ModelPaths.TryGetValue( name, out _ ) ? ModelPaths[ name ] : ModelPaths[ Names.Null ];
        
        return Raylib.LoadModel( modelPath );
    
    }

}