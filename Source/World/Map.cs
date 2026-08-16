using System.Numerics;
using Raylib_cs;
using Umbrage;
using Umbrage.World.Entity;
using Umbrage.World.Object;

public class Map {

    public Player player;
    private Game game;

    public Map( Game game_ ) {
        game = game_;
        player = new Player( game ).Configure<Player>( p => {
            p.setUseGravity   ( true );
            p.setSolid        ( true );
            p.setStatic       ( false );
            p.setPosition     ( new Vector3( 0, 2, -5   ) );
            p.setSize         ( new Vector3( 2f, 2f, 2f ) );
            p.setCameraAnchor ( new Vector3( 0, 2f, 0   ) );

        });
        
        addToScene( 
            new Cube( game )
                .Configure<Cube>( c => {
                    c.setUseGravity( true );
                    c.setSolid     ( true );
                    c.setPosition  ( new Vector3( 0, 100, 10 ) )
                    .setSize       ( new Vector3( 2f, 2f, 2f ) )
                    .setColor      ( Color.Pink );
                }
            )
        );

        addToScene(
            new Cube( game ).Configure<Cube>( c => {
                c.setUseGravity( false );
                c.setSolid     ( true );
                c.setPosition  ( new Vector3( 0, -7, 0 ) );
                c.setStatic    ( true )
                .setSize       ( new Vector3( 100f, 5f, 100f ) )
                .setColor      ( Color.Gray );

            })
        );

        addToScene( player );
    }

    private void addToScene( GenericEntity entity ) {
        
        scene.Add( entity );

    }

    public List<GenericEntity> scene = new();

    public float CollisionCalc(GenericEntity a, GenericEntity b) {
        BoundingBox ab = a.getBoundingBox();
        BoundingBox bb = b.getBoundingBox();

        float overlapY = MathF.Min(ab.Max.Y, bb.Max.Y) - MathF.Max(ab.Min.Y, bb.Min.Y);

            if( a.getStatic() && !b.getStatic() ) {
                
                if (b.position.Y > a.position.Y) b.position.Y += overlapY;
                else {
                    b.position.Y -= overlapY;
                    b.setGrounded( true );
                    
                };
            }
            else if (b.getStatic()  && !a.getStatic() )
            {
                if (a.position.Y > b.position.Y) a.position.Y += overlapY;
                    
                else {
                    a.position.Y -= overlapY;
                    a.setGrounded( true );

                };
            }

        return overlapY;

    }

    public void collisions( GenericEntity entity, float delta ) {
        
        foreach( GenericEntity target in scene ) {
            
            if( entity == target ) continue;

            if( !entity.getSolid() || !target.getSolid() ) continue;

            if( Raylib.CheckCollisionBoxes( entity.getBoundingBox(), target.getBoundingBox() ) ) {
                
                // if( entity is PhysicalObject ) ((PhysicalObject)entity).multiplyMomentum( 0, -.0f, 0 );

                CollisionCalc( entity, target );
            /*
                Raylib.DrawBoundingBox(
                    ((PhysicalObject)entity).getBoundingBox(),
                    Color.Red
                );

                Raylib.DrawBoundingBox(
                    ((PhysicalObject)target).getBoundingBox(),
                    Color.Green
                );
            */
                
            }
        }

    }

    public void update( float delta ) {

        foreach( GenericEntity entity in scene ) {
            
            entity.update( delta );

            collisions( entity, delta );

            
        }

    }

}