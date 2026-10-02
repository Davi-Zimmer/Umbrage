using Raylib_cs;

public class Object {
    
    Mesh Mesh = Raylib.GenMeshCube( 1, 1, 1 );

    public Object() {
        
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