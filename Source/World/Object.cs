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
        Raylib.DrawMesh( Mesh, Material, Utils.MatrixTransformRigidBody( RigidBody ) );
    }

}