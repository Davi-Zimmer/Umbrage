using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class Object {
    
    public Material Material = Raylib.LoadMaterialDefault();
    public RigidBody RigidBody;
    public Mesh Mesh;

    public Object( Map map ) {

        Texture2D t = Texture.Get( Texture.Name.Missing );
        Raylib.SetMaterialTexture(ref Material, MaterialMapIndex.Diffuse, t);
        RigidBody = map.World.CreateRigidBody();

        Mesh = Raylib.GenMeshCube( 1, 1, 1 );

        RigidBody.AddShape( new BoxShape( 1 ) );
        RigidBody.Position   = new( 0, -5, 0 );

    }

    public T Configure<T>( Action<T> cb ) where T: Object {
        
        cb( (T)this );
        
        return (T)this;
        
    }

    public void Update( float delta ) {
        
        Tick( delta );

        Render();

    }

    public virtual void Tick( float dt ) {
        
    }
    
    public virtual void Render() {
        
    }


}