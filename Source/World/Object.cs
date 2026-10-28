using System.Numerics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class Object: GenericEntity {
    
    public Material Material = Raylib.LoadMaterialDefault();
    
    public Mesh Mesh;

    public Object( Map map ) : base( map ) {

    

    }

    public override void Update( float delta ) {
        
        Tick( delta );

        Render();

    }

    public override void Tick( float dt ) {
        
    }
    
    public override void Render() {

        // Shader shader = Shaders.Get( Shaders.Name.Basic );

        // int timeLoc = Raylib.GetShaderLocation(shader, "time");

        // Dentro do loop do jogo:
        // float time = (float)Raylib.GetTime();

        /// Raylib.SetShaderValue( shader, timeLoc, time, ShaderUniformDataType.Float );

        Material.Shader = ShaderManager.Lighting();


        //Raylib.BeginShaderMode( shader );

        Raylib.DrawMesh( Mesh, Material, Utils.MatrixTransformRigidBody( RigidBody ) );

       // Raylib.DrawCube( RigidBody.Position, 1, 1, 1, Color.White  );

        //Raylib.EndShaderMode();

    }

}