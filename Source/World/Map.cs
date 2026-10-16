using System.Numerics;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;

public class Map {

    Camera3D camera = new() {
        Position   = new Vector3( -20.0f, 8.0f, 10.0f ),
        Target     = new Vector3(   0.0f, 4.0f, 0.0f  ),
        Up         = new Vector3(   0.0f, 1.0f, 0.0f  ),
        FovY       = 45.0f,
        Projection = CameraProjection.Perspective
    };

    public World World = new();
    private List<Object> Scene = new();

    private List<Action> Actions = new();
    RigidBody plane;
    Model planeModel;

    public Texture2D GenCheckedTexture(int size, int checks, Color colorA, Color colorB)
    {
        Image imageMag = Raylib.GenImageChecked(size, size, checks, checks, colorA, colorB);
        Texture2D textureMag = Raylib.LoadTextureFromImage(imageMag);
        Raylib.UnloadImage(imageMag);
        return textureMag;
    }

    public Map(){

        World.SubstepCount = 4;

        for( int i = 0; i < 10; i++ ) {
            
            AddScene( new Object( this ).Configure<Object>( o => {
                o.RigidBody.AddShape( new BoxShape( 1 ) );
                o.RigidBody.Position = new( 0, 10 + i * 2 + .5f, 0 );
            }));

        }

        Texture2D texture = Texture.Get( Texture.Name.Ground );
        planeModel = Raylib.LoadModelFromMesh(Raylib.GenMeshPlane( 10, 10, 10, 10 ) );
        Raylib.SetMaterialTexture(ref planeModel, 0, MaterialMapIndex.Diffuse, ref texture);
        
        plane = World.CreateRigidBody();
        plane.AddShape( new BoxShape( 10 ) );
        plane.Position = new JVector( 0, -5, 0 );
        plane.MotionType = MotionType.Static;

    }

    /*
    public void AddScene( Object o ) {
        
    }

    public void RemoveScene( Object o ) {

        Object? toDelete = null;

        foreach( RigidBody r in World.RigidBodies ) {
            
            if( r.GetHashCode() == o.RigidBody.GetHashCode()) {
                
                toDelete = o;
                
                break;
            
            }

        }

        if( toDelete == null ) return;

        /// Remove Event

        World.Remove( toDelete.RigidBody );

    }
    */

    public void AddScene( Object o ) {

        Actions.Add( () => Scene.Add( o ) );              

    }

    public void RemoveScene( RigidBody body ) {
        Actions.Add(() => {
           
           Object? toDelete = null;

            foreach( Object o in Scene ) {
                
                if( o.RigidBody.GetHashCode() == body.GetHashCode()) {
                    
                    toDelete = o;
                    
                    break;
                } 

            }

            if( toDelete == null ) return;

            Scene.Remove( toDelete );
            World.Remove( body );

        });

    }
    public void ExecuteActions() {

        foreach( Action action in Actions ) action();

    }

    public Matrix4x4 MatrixTransform( RigidBody b ) {

        JMatrix ori = JMatrix.CreateFromQuaternion( b.Orientation );
        JVector pos = b.Position;

        return new Matrix4x4(
            ori.M11, ori.M12, ori.M13, pos.X,
            ori.M21, ori.M22, ori.M23, pos.Y,
            ori.M31, ori.M32, ori.M33, pos.Z,
            0, 0, 0, 1.0f
        );

    }

    public void Update( float delta ) {
        
        Raylib.ClearBackground( Color.Black );

        ExecuteActions();
        
        World.Step( delta, true );

        Raylib.BeginMode3D( camera );
        {

            Raylib.DrawModel( planeModel, Vector3.Zero, 1.0f, Color.White );

            foreach( Object o in Scene ) {
                
                Raylib.DrawMesh( o.Mesh, o.Material, MatrixTransform( o.RigidBody ) );

            }


        }
        Raylib.EndMode3D();


    }

}