using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Raylib_cs;

public class Light {

    public Vector3 CoreColor;
    public float Radius = 3f;
    public float LightRadius = 10f;
    public float Speed = .1f;
    public float Height = 8f;
    public float Intensity = 5f;
    public float LightDistance = 50f;
    public float CoreRadius = .2f;
    public float Brightness = 5f;
    

    public Mesh HaloMesh;
    public Model HaloModel;
    public Material HaloMaterial = Raylib.LoadMaterialDefault();
    public Vector3 Position = new();

    private Map Map;

    public Model Model;

    public Vector3 FadeColor = new();


    public Light( Map map ) {
        Map = map;

        HaloMesh   = Raylib.GenMeshPlane( Radius, Radius, 10, 10 );
        HaloModel = Raylib.LoadModelFromMesh( HaloMesh );
        
        CoreColor = ColorToVec3( Color.White );
        FadeColor = ColorToVec3( new Color( 252, 255, 62 ) );
        
        Shader shader =  Shaders.Get( Shaders.Name.Halo );

        Raylib.SetMaterialShader( ref HaloModel, 0, ref shader );

        unsafe {
            HaloModel.Materials[0].Maps[(int)MaterialMapIndex.Albedo].Color = Color.White;
        }
    }

    public T Configure<T>( Action<T> cb ) where T: Light {
        
        cb( (T)this );

        return (T)this;
    }


    public Vector3 ColorToVec3( Color color ) {
        return new Vector3(
            color.R / 255f,
            color.G / 255f,
            color.B / 255f
        );
    }

    public virtual void Update( float delta ) {
        
        Tick( delta );
        Render();

    }


    public virtual void Tick( float delta ) {


    }

    public virtual void Render() {
        SetShadersVars();
        
        DrawHalo();
    }

    public void SetShadersVars() {
        Shader HaloShader = Shaders.Get( Shaders.Name.Halo );

        Raylib.SetShaderValue( HaloShader,
            Raylib.GetShaderLocation(HaloShader, "coreColor"),
            CoreColor,
            ShaderUniformDataType.Vec3
        );

        Raylib.SetShaderValue( HaloShader,
            Raylib.GetShaderLocation(HaloShader, "fadeColor"),
            FadeColor,
            ShaderUniformDataType.Vec3
        );

        Raylib.SetShaderValue( HaloShader,
            Raylib.GetShaderLocation(HaloShader, "coreRadius"),
            CoreRadius,
            ShaderUniformDataType.Float
        );

        Raylib.SetShaderValue( HaloShader,
            Raylib.GetShaderLocation(HaloShader, "intensity"),
            Brightness,
            ShaderUniformDataType.Float
        );

        
    }

    void DrawHalo() {

        Vector3 direction = Vector3.Normalize(Game.Map.Player.Camera.Position - Position);

        // O plano original tem a normal apontando para cima.
        Vector3 normal = Vector3.UnitY;

        Vector3 axis = Vector3.Cross(normal, direction);
        float axisLength = axis.Length();

        if (axisLength < 0.001f) axis = Vector3.UnitX;
        else axis /= axisLength;

        float dot = Math.Clamp(Vector3.Dot(normal, direction), -1.0f, 1.0f);
        float angle = MathF.Acos(dot) * (180.0f / MathF.PI);

        Raylib.BeginBlendMode(BlendMode.Alpha);
        Rlgl.DisableDepthMask();

        Raylib.DrawModelEx( HaloModel, Position, axis, angle,
            new Vector3(Radius, Radius, Radius),
            Color.White
        );

        Rlgl.EnableDepthMask();
        Raylib.EndBlendMode();
    }

    

}
