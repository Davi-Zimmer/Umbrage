using System.Numerics;
using Raylib_cs;

public class ShaderManager {

    public static Shader Lighting() {
        Shader lightingShader     = Shaders.Get( Shaders.Name.Lighting );
        int    lightPosLoc        = Raylib.GetShaderLocation( lightingShader, "lightPos" );
        int    lightColorLoc      = Raylib.GetShaderLocation( lightingShader, "lightColor" );
        int    ambientStrengthLoc = Raylib.GetShaderLocation( lightingShader, "ambientStrength" );

        float time = (float)Raylib.GetTime();

        float radius = 12f;
        float height = 8f;
        float speed = 0.5f;

        float ambient = 0.12f; // mudar pra alguma classe de contexto

        Vector3 lightColor = new Vector3( 1f, 0.82f, 0.55f );

        Vector3 lightPos = new Vector3(
            MathF.Cos(time * speed) * radius,
            height,
            MathF.Sin(time * speed) * radius
        );

        Raylib.SetShaderValue( lightingShader, lightPosLoc       , lightPos  , ShaderUniformDataType.Vec3  );
        Raylib.SetShaderValue( lightingShader, lightColorLoc     , lightColor, ShaderUniformDataType.Vec3  );
        Raylib.SetShaderValue( lightingShader, ambientStrengthLoc, ambient   , ShaderUniformDataType.Float );
        
        /*
        int lightDirectionLoc  = Raylib.GetShaderLocation( lightingShader, "lightDirection" );
        int lightColorLoc      = Raylib.GetShaderLocation( lightingShader, "lightColor" );
        int ambientStrengthLoc = Raylib.GetShaderLocation( lightingShader, "ambientStrength" );

        // { -0.5f,  1.0f, -0.3f };
        // { 1.0f, 0.95f, 0.85f };
        // 0.15f;
    
        Raylib.SetShaderValue( lightingShader, lightDirectionLoc , lightDirectionArray , ShaderUniformDataType.Vec3  );
        Raylib.SetShaderValue( lightingShader, lightColorLoc     , lightColorArray     , ShaderUniformDataType.Vec3  );
        Raylib.SetShaderValue( lightingShader, ambientStrengthLoc, ambientStrength     , ShaderUniformDataType.Float );
        */
        return lightingShader;

    }

    public static void SetShader( Model model, Shader shader ){
        unsafe {
                
            for( int i = 0; i < model.MaterialCount; i++ ) {

                model.Materials[ i ].Shader = shader;
            }
        }

    }

}