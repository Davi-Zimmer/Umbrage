using Raylib_cs;

public class Texture {
    
    private static Dictionary< Name, Texture2D > Loaded = new();

    public static void Load() {

        AddTexture( Name.Ground , MissingTexture( 10, 1, Color.Gray   , Color.DarkGray ) );
        AddTexture( Name.Missing, MissingTexture( 2 , 1, Color.Magenta, Color.Black    ) );

    }

    private static Texture2D MissingTexture( int size, int checks, Color a, Color b ) {
        
        Image img = Raylib.GenImageChecked( size, size, checks, checks, a, b );
        
        Texture2D texture = Raylib.LoadTextureFromImage( img );
        
        return texture;
    }

    private static void AddTexture( Name name, Texture2D texture ) {
        Loaded.Add( name, texture );
    }

    public static void UnLoad() {

        foreach( var item in Loaded ) {

            Raylib.UnloadTexture( item.Value );

        }

    }

    public static Texture2D Get( Name name ) {
        
        return Loaded.TryGetValue( name, out _ ) ? Loaded[ name ] : Loaded[ Name.Missing ];

    }

    public enum Name {
        Missing,
        Ground
    }


}