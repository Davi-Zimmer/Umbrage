using System.Numerics;
using Raylib_cs;

namespace Umbrage.World.Entity;

public class GenericEntity {

    public T Configure<T>( Action<T>  cfg ) where T: GenericEntity {
        cfg( (T)this );
        return (T)this;
    }

    // -------------------------------------- Setters -------------------------------------- \\
    public GenericEntity setPosition( Vector3 vec ){ position = vec; return this; }
    public GenericEntity setSize( Vector3 vec ){ size = vec; return this; }

    public GenericEntity setColor( Color c ){ color = c; return this; }
    public GenericEntity setX( float X ){ position.X = X; return this; }
    public GenericEntity setY( float Y ){ position.Y = Y; return this; }
    public GenericEntity setZ( float Z ){ position.Z = Z; return this; }

    public GenericEntity setSolid( bool s ) { isSolid = s; return this; }
    public GenericEntity setStatic( bool s ) { isStatic = s; return this; }
    public GenericEntity setUseGravity( bool g ) { gravity = g; return this; }

    public GenericEntity setGrounded( bool g ) { grounded = g; return this; }
    
    // -------------------------------------- Getters -------------------------------------- \\
    public virtual BoundingBox getBoundingBox() {

        Vector3 halfSize = size / 2f;

        return new BoundingBox {
            Min = position - halfSize,
            Max = position + halfSize
        };

    }

    public Vector3 getPosition() { return position; }
    public Vector3 getSize() { return size; }
    public Color getColor() { return color; }
    public float getX(){ return position.X; }
    public float getY(){ return position.Y; }
    public float getZ(){ return position.Z; }
    public bool getSolid(){ return isSolid; }
    public bool getStatic(){ return isStatic; }
    public bool getGrounded() { return grounded; }

    public bool useGravity() { return gravity; }


    public virtual Vector3 extractRealPosition() {
        return position;
    }

    public Vector3 position = new( 0, 0, 0 );
    private Vector3 size = new( 0, 0, 0 );
    
    private Color color = Color.Black;
    private bool grounded = false; 

    public Game game;

    public bool isSolid = true;
    public bool gravity = true;

    public bool isStatic = false;

    public GenericEntity( Game game_ ) {
        game = game_;
    }

    public virtual void update( float delta ) {
        
        
        
    }


}