using System.Numerics;
using Raylib_cs;

public class ShaderManager {

    public static Shader Lighting() {
        Shader shader     = Shaders.Get( Shaders.Name.Lighting );
        int    lightPosLoc        = Raylib.GetShaderLocation( shader, "lightPos" );
        int    lightColorLoc      = Raylib.GetShaderLocation( shader, "lightColor" );
        int    ambientStrengthLoc = Raylib.GetShaderLocation( shader, "ambientStrength" );

        int intensityLoc = Raylib.GetShaderLocation( shader, "lightIntensity" ); 
        int rangeLoc = Raylib.GetShaderLocation( shader, "lightRange");

        float time = (float)Raylib.GetTime();

        Sun sun = Game.Map.Sun;

        Raylib.SetShaderValue( shader, lightPosLoc       , sun.GetLightPos(time) , ShaderUniformDataType.Vec3  );
        Raylib.SetShaderValue( shader, lightColorLoc     , sun.FadeColor            , ShaderUniformDataType.Vec3  );
        Raylib.SetShaderValue( shader, ambientStrengthLoc, Game.Map.Ambient      , ShaderUniformDataType.Float );
        Raylib.SetShaderValue( shader, intensityLoc      , sun.Intensity         , ShaderUniformDataType.Float );
        Raylib.SetShaderValue( shader, rangeLoc          , sun.LightDistance     , ShaderUniformDataType.Float );
        
        return shader;

    }

    public static void SetShader( Model model, Shader shader ){
        unsafe {
                
            for( int i = 0; i < model.MaterialCount; i++ ) {

                model.Materials[ i ].Shader = shader;

            }

        }

    }

}