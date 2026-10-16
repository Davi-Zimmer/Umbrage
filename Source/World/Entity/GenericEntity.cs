using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class GenericEntity {

    public RigidBody RigidBody;

    public Map Map;

    public GenericEntity( Map map ) {

        Map = map;

        RigidBody = map.World.CreateRigidBody();

        PosConfig();

    }

    public T Configure<T>( Action<T> cb ) where T: GenericEntity {
        
        cb( (T)this );

        PosConfig();
        
        return (T)this;
        
    }

    public virtual void PosConfig() {
        
    }

    public virtual void Update( float delta ) {
        
        Tick( delta );

        Render();

    }

    public virtual void Tick( float dt ) {
        
    }
    
    public virtual void Render() {
        
    }


}