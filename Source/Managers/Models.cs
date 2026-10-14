using Raylib_cs;

public class Model3D {
    
    public static Dictionary< Names, string > ModelPaths = new() {
        [ Names.Null                  ] = "./Assets/untitled.glb",
        [ Names.Light_Zone_Checkpoint ] = "./Assets/Checkpoint.glb"
    };

    public enum Names {
        Null,
        Light_Zone_Checkpoint
    }

    public static Model Get( Names name ) {
        
        string modelPath = ModelPaths.TryGetValue( name, out _ ) ? ModelPaths[ name ] : ModelPaths[ Names.Null ];

        return Raylib.LoadModel( modelPath );
    
    }

}