using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class Object : GenericEntity {
    
    public Material Material = Raylib.LoadMaterialDefault();
    
    public Mesh Mesh;

    public Object( Map map ) : base( map ) {

        Texture2D t = Texture.Get( Texture.Name.Missing );
        Raylib.SetMaterialTexture(ref Material, MaterialMapIndex.Diffuse, t);

        Mesh = Raylib.GenMeshCube( 1, 1, 1 );

        RigidBody.AddShape( new BoxShape( 1 ) );
        RigidBody.Position   = new( 0, -5, 0 );

    }

    public override void Update( float delta ) {
        
        Tick( delta );

        Render();

    }

    public override void Tick( float dt ) {
        
    }
    
    public override void Render() {
        
    }

}