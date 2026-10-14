using System.Numerics;
using System.Runtime.InteropServices;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;

public class Map {

    public World World = new();
    private List<Object> Scene = new();
    private List<Action> Actions = new();
    RigidBody plane;
    Model planeModel;

    Player Player;
    Model Model;
    Vector3 ModelPosition;

    Room Room;

    public Map(){

        World.SubstepCount = 4;

        World.Gravity = new( 0, -9, 0 );

        for( int i = 0; i < 10; i++ ) {
            
            AddScene( new Object( this ).Configure<Object>( o => {
                o.RigidBody.AddShape( new BoxShape( 1 ) );
                o.RigidBody.Position = new( 0, 10 + i * 2 + .5f, 0 );
            }));

        }

        Texture2D texture = Texture.Get( Texture.Name.Ground );
        planeModel = Raylib.LoadModelFromMesh(Raylib.GenMeshPlane( 50, 50, 10, 10 ) );
        Raylib.SetMaterialTexture(ref planeModel, 0, MaterialMapIndex.Diffuse, ref texture);
        
        plane = World.CreateRigidBody();
        plane.AddShape( new BoxShape( 50, 10, 50 ) );
        plane.Position = new JVector( 0, -5, 0 );
        plane.MotionType = MotionType.Static;


        Player = new Player( this ).Configure<Player>( p => {
            
            // p.RigidBody.AffectedByGravity = false;

            p.Camera = new() {
                Position   = new( -15, 10, -15 ),
                Target     = new Vector3( 10, 0, 10 ),
                Up         = new Vector3( 0.0f, 1.0f, 0.0f ),
                FovY       = 45.0f,
                Projection = CameraProjection.Perspective
            };

            p.CameraOffset = new( 0, 1, 0 );

            p.RigidBody.AddShape( new BoxShape( 1, 2, 1 ) );

        });

        Room = new Room( this ).Configure<Room>( o => {

            o.RigidBody.MotionType = MotionType.Static;
            o.RigidBody.Position   = new( 20, 0, 0 );

            o.Model = Model3D.Get( Model3D.Names.Null );

            Utils.CreateMapCollider( o.Model, o.RigidBody );

        });

    }

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

        Actions.Clear();

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

    public void ExecuteKeys( float delta ) {

    }

    public void Step( float delta ) {

        World.Step( delta, true );
        World.Step( delta, true );
    }

    public void Update() {

        float delta = Raylib.GetFrameTime();
        Raylib.ClearBackground( Color.Black );

        ExecuteActions();

        ExecuteKeys( delta );

        Player.Update( delta );

        Step( delta );
  
        Raylib.BeginMode3D( Player.Camera );
        {

            Raylib.DrawModel( planeModel, Vector3.Zero, 1.0f, Color.White );

            Raylib.DrawModel( Room.Model, Room.RigidBody.Position, 1, Color.White );

            foreach( Object o in Scene ) {
                
                Raylib.DrawMesh( o.Mesh, o.Material, MatrixTransform( o.RigidBody ) );

            }

        }
        Raylib.EndMode3D();

        Raylib.DrawFPS( 50, 50 );

    }

}