using System.Numerics;
using Raylib_cs;
using Umbrage;
using Umbrage.World;
using Umbrage.World.Entity;
using Jitter2;

public class Map {

    public World World = new();
    
    Player Player;

    public List<GenericEntity> scene = new();

    public Map() {
        
        Player  = new Player( this ).Configure<Player>( p => {
            p.Position = new Vector3( 0, 5, 0 );
            p.Size     = new Vector3( 1 );
        });


        AddToScene( new GenericEntity( this ).Configure<GenericEntity>( e => {
            e.Color    = Color.Gray;
            e.Position = new Vector3( 0, 2, 0 );
            e.Size     = new Vector3( 20, 2, 20 );
        }) );

        AddToScene( Player );

    }

    private void AddToScene( GenericEntity entity ) {
        
        scene.Add( entity );

    }

    public void Update( float delta ) {
        
        World.Step( delta, true );

        Raylib.BeginMode3D( Player.Camera );

        foreach( GenericEntity entity in scene ) {
            
            entity.Update( delta );

        }

        Raylib.EndMode3D();


    }

}