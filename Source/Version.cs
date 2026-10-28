using System.Dynamic;
using System.Text.Json;

public sealed class Version {
    
    public int Major {get; set;}
    public int Minor {get; set;}
    public int Patch {get; set;}
    public int Experimental {get; set;}
    public int Build {get; set;}
    public int Year {get; set;}
    public string Stage {get; set;} = "";

    public static Version Instance = null!;

    public static string Get() {

        return $"{Instance.Year}-{Instance.Major}.{Instance.Minor}.{Instance.Patch}.{Instance.Experimental} - {Instance.Stage} Build {Instance.Build}";

    }

    public static Version Load() {

        if( Instance == null ) {
            
            string json = File.ReadAllText("./Version.json");

            Version? v = JsonSerializer.Deserialize<Version>( json, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );


            if( v == null ) throw new InvalidDataException( "O arquivo de versão está vazio ou inválido." );
        
            Instance = v;
        
        }

        return Instance;
    
    }
}