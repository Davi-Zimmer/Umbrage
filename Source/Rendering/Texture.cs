using Raylib_cs;

public class Texture {
    
    private static Dictionary< Name, Texture2D > Loaded = new();
    private static List<Texture2D > DynamicTexture = new(); 

    public static void Load() {

        AddTexture( Name.Ground , MissingTexture( 50, 1, Color.Gray   , Color.DarkGray ) );
        AddTexture( Name.Missing, MissingTexture( 2 , 1, Color.Magenta, Color.Black    ) );

    }

    public static Texture2D MissingTexture( int size, int checks, Color a, Color b ) {
        
        Image img = Raylib.GenImageChecked( size, size, checks, checks, a, b );
        
        Texture2D texture = Raylib.LoadTextureFromImage( img );
        
        return texture;
    }

    public static Texture2D Create( int size, int checks, Color a, Color b, out int ID ) {
        
        Texture2D t = MissingTexture( size, checks, a, b );
        
        ID = DynamicTexture.Count();

        DynamicTexture.Add( t );

        return t;

    }

    private static void AddTexture( Name name, Texture2D texture ) {
        Loaded.Add( name, texture );
    }

    public static void UnLoad() {

        foreach( var item in Loaded ) Raylib.UnloadTexture( item.Value );

        foreach( var item in DynamicTexture ) Raylib.UnloadTexture( item );

    }

    public static Texture2D Get( Name name ) {
        
        return Loaded.TryGetValue( name, out _ ) ? Loaded[ name ] : Loaded[ Name.Missing ];

    }

    public enum Name {
        Missing,
        Ground
    }


}