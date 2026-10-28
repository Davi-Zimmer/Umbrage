using Raylib_cs;

public class Room : Object {
    public Model Model = new();
    
    public Room( Map map ): base( map ) {
        

    }

    public override void Render() {

        ShaderManager.SetShader( Model, ShaderManager.Lighting() );

        Raylib.DrawModel( Model, RigidBody.Position, 1, Color.White );
    }

}