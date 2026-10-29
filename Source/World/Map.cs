using System.Numerics;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class Map {

    public World World = new();
    private List<GenericEntity> Scene = new();
    private List<Light> Lights = new();
    private List<Action> Actions = new();

    public float Ambient = 0;//0.12f;

    public Player Player;
    public Sun Sun;

    public Map(){

        Sun = new Sun( this ).Configure<Sun>( s => {
            
        });

        AddLight( Sun );

        World.SubstepCount = 4;

        World.Gravity = new( 0, -5, 0 );

        AddScene( new Enemy( this ).Configure<Enemy>( e => {
            e.Model = Model3D.Get( Model3D.Names.Enemy );
            e.RigidBody.Position = new( 0, 40, 0 );
            e.RigidBody.AddShape( new BoxShape( 1 ) );
            Texture2D t = Texture.Get( Texture.Name.Missing );
            Raylib.SetMaterialTexture(ref e.Material, MaterialMapIndex.Diffuse, t);

        }));

        for( int i = 0; i < 10; i++ ) {
            
            AddScene( new Object( this ).Configure<Object>( o => {

                o.RigidBody.AddShape( new BoxShape( 1 ) );

                o.RigidBody.Position = new( 0, 10 + i * 2 + .5f, 0 );

                Texture2D t = Texture.Get( Texture.Name.Missing );

                Raylib.SetMaterialTexture(ref o.Material, MaterialMapIndex.Diffuse, t);
                
                o.Mesh = Raylib.GenMeshCube( 1, 1, 1 );

                o.RigidBody.SetMassInertia( 10 );

            }));

        }

        AddScene( new Object( this ).Configure<Object>( o => {
            
            Vector3 size = new(50, 10, 50 );

            o.RigidBody.AddShape( new BoxShape( size ) );

            o.RigidBody.Position = new( 0, -5, 0 );

            Texture2D t = Texture.Get( Texture.Name.Ground );

            Raylib.SetMaterialTexture(ref o.Material, MaterialMapIndex.Diffuse, t);
                
            o.Mesh = Raylib.GenMeshCube( size.X, size.Y, size.Z );

            o.RigidBody.MotionType = MotionType.Static;
        
        }));

        Player = new Player( this ).Configure<Player>( p => {
            
            // p.RigidBody.AffectedByGravity = false;

            p.Camera = new() {
                Position   = new( -15, 10, -15 ),
                Target     = new Vector3( 10, 0, 10 ),
                Up         = new Vector3( 0.0f, 1.0f, 0.0f ),
                FovY       = 80.0f,
                Projection = CameraProjection.Perspective
            };

            p.CameraOffset = new( 0, 1, 0 );

            p.RigidBody.AddShape( new BoxShape( 1, 2, 1 ) );

        });

        AddScene(new Room( this ).Configure<Room>( o => {
            // Raylib.DrawModel( Room.Model, Room.RigidBody.Position, 1, Color.White );

            o.RigidBody.MotionType = MotionType.Static;
            o.RigidBody.Position   = new( 20, 0, 0 );
            o.Model = Model3D.Get( Model3D.Names.Null );

            Utils.CreateMapCollider( o.Model, o.RigidBody );

        }));

        AddScene( Player );

        AddScene( new Object( this ).Configure<Object>( o => {
            
            Vector3 size = new( 20f, 20f, 10f);

            o.RigidBody.ClearShapes();

            o.RigidBody.Position = new( 20, 20, 20 );

            o.RigidBody.AddShape( new BoxShape( size ) );

            o.Mesh = Raylib.GenMeshCube( size.X, size.Y, size.Z );

            o.RigidBody.MotionType = MotionType.Static;

            Texture2D t = Texture.Create( 20, 1, Color.Red, Color.White, out _ );

            Raylib.SetMaterialTexture( ref o.Material, MaterialMapIndex.Diffuse, t );

        }));

    }

    public void AddScene( GenericEntity o ) {

        Actions.Add( () => Scene.Add( o ) );              

    }

    public void RemoveScene( RigidBody body ) {
        Actions.Add(() => {
           
           GenericEntity? toDelete = null;

            foreach( GenericEntity o in Scene ) {
                
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

    public void AddLight( Light l ) {

        Actions.Add( () => Lights.Add( l ) );

    }

    public void RemoveLight( Light l ) {

        Actions.Add( () => Lights.Remove( l ) );

    }

    public void ExecuteActions() {

        foreach( Action action in Actions ) action();

        Actions.Clear();

    }

    public CastEntity RayCast( Vector3 from, Vector3 to ) {
    
        return new( from, to, World );

    }

    public delegate void FindEByRigidBodyCallback( Enemy entity );

    public void FindEnemyByRigidBody( RigidBody body, FindEByRigidBodyCallback cb ) {

        GenericEntity? o = FindObjectByRidigBody( body );

        if( o is Enemy ) cb( ( o as Enemy )! );

    }

    public GenericEntity? FindObjectByRidigBody( RigidBody body ) {
        
        foreach( GenericEntity e in Scene ) {
            
            if( e.RigidBody == body ) return e;

        }

        return null;

    }

    public void RayCastRigidBody( Vector3 from, Vector3 to, FindEByRigidBodyCallback cb ) {
        
        RayCast( from, to ).then( shape => {

            FindEnemyByRigidBody( shape.RigidBody, e => {

                cb( e ); 

            });

        });

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

        Step( delta );

        // Player.Update( delta );

        Raylib.BeginMode3D( Player.Camera );
        {

            //Raylib.DrawModel( planeModel, Vector3.Zero, 1.0f, Color.White );

            foreach( GenericEntity o in Scene ) o.Update( delta );
            
            foreach( Light l in Lights ) l.Update( delta );



        }
        Raylib.EndMode3D();


        Vector2 c = Raylib.GetScreenCenter();

        int a = 10;
        Color color = Color.Pink;
        Raylib.DrawLine( (int)c.X, (int)c.Y, (int)c.X - a, (int)c.Y + a, color);
        Raylib.DrawLine( (int)c.X, (int)c.Y, (int)c.X + a, (int)c.Y + a, color);

        Raylib.DrawFPS( 50, 50 );

    }

}