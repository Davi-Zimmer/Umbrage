using System.Numerics;
using Umbrage.Components;

namespace Umbrage.World.Entity;


public class Enemy: GenericEntity {
    
    public float Speed = 10;

    public new Health Health { get; protected set; } = new( 10 ); 

    public Vector3 lookingAt = new( 0 );

    public Enemy( Map map ): base( map ) {

        Health.DeathEvents.Add( () => {
            
            map.removeScene( RigidBody );

        });

        // Health.DamageEvents.Add( () => {});

    }

    public override void Tick( float delta ) {
        
        followPlayer( delta );
    }

    public void followPlayer( float delta ) {
        
        Player player = Map.Player;

        Vector3 direction = player.RigidBody.Position - RigidBody.Position;
    
        Vector3 distance = Vector3.Normalize( direction );

        RigidBody.Position += distance * delta * Speed;

    }

    public override void Render() {
        
        base.Render();

    }


}