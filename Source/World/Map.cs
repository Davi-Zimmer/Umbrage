using System.Numerics;
using Raylib_cs;
using Umbrage;
using Umbrage.World;
using Umbrage.World.Entity;
using Jitter2;
using Jitter2.Dynamics;
using System.Diagnostics;

public class Map {

    public World World = new();
    
    public float gameSpeed = 3;

    public Game Game;

    Player Player;

    public List<GenericEntity> scene = new();

    public Map( Game game ) {
        Game = game; 

        World.Gravity = new Vector3( 0, -9, 0 );
        
        Player  = new Player( this ).Configure<Player>( p => {
            //p.RigidBody.AffectedByGravity = false;
            p.RigidBody.Position = new Vector3( 0, 5, 0 );
            p.Size               = new Vector3( 1, 2, 1 );
            p.CameraAnchor       = new Vector3( 0, p.Size.Y, 0);
            p.Mass = 80;

        });

        AddToScene( new GenericEntity( this ).Configure<GenericEntity>( e => {
            e.Color              = Color.Purple;
            e.RigidBody.Position = new Vector3( 7, 10, 0 );
            e.Size               = new Vector3( 2, 2, 2 );
            e.Mass               = 1f;
            // e.RigidBody.AffectedByGravity = false;

        }) );

        AddToScene( new GenericEntity( this ).Configure<GenericEntity>( e => {
            RigidBody r          = e.RigidBody; 
            e.Color              = Color.Gray;
            e.RigidBody.Position = new Vector3( 0, 2, 0 );
            e.Size               = new Vector3( 50, 10, 50 );
            r.AffectedByGravity  = false;
            r.MotionType         = MotionType.Static;
        }) );

        AddToScene( new GenericEntity( this ).Configure<GenericEntity>( e => {
            RigidBody r          = e.RigidBody; 
            e.Color              = Color.Red;
            e.RigidBody.Position = new Vector3( 0, 15, 20 );
            e.Size               = new Vector3( 20, 30, 2 );
            r.AffectedByGravity  = false;
            r.MotionType         = MotionType.Static;
        }) );

        AddToScene( Player );

    }

    private void AddToScene( GenericEntity entity ) {
        
        actions.Add(() => {
            scene.Add( entity );
        });

    }

    private void StepSimulation( float delta ) {

        for( int i = 0; i < gameSpeed; i++ ) {

            World.Step( delta, true );
            
        }

    }

    public List<Action> actions = new();

    public void removeScene( RigidBody body ) {
        
        actions.Add(() => {

            GenericEntity? toDelete = null;

            foreach( GenericEntity e in scene ) {
            
                if( e.RigidBody.GetHashCode() == body.GetHashCode() ) {
                    toDelete = e;
                    break;
                }

            }

            if( toDelete != null ) {

                scene.Remove( toDelete );
                World.Remove( body );
                
            }

        });

    }

    public void ExecutePreLoopFuncs() {

        foreach( var action in actions ) action();

        actions = [];

    }

    public List<Action> pos3DRender = new();

    public void ExecutePos3D() {
        
        foreach( Action action in pos3DRender ) action();

    }
    
    public void Update( float delta ) {

        // StepSimulation( delta );
        ExecutePreLoopFuncs();


        World.Step( delta * gameSpeed, true );

        Raylib.BeginMode3D( Player.Camera );

        foreach( GenericEntity entity in scene ) {
            
            entity.Update( delta );

        }

        Raylib.EndMode3D();

        ExecutePos3D();
        

    }

}