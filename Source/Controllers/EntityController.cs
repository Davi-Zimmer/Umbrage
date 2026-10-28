using System.Numerics;

public class EntityController {
    
    private static EntityController Instance = null!;

    public Entity e;

    public static void Begin( Entity e ) {
        Instance = new( e );
    }

    EntityController( Entity entity ) {
        e = entity;
    }

    public Vector3 GetForward() {

        e.Pitch = Math.Clamp( e.Pitch, -1.5f, 1.5f );

        Vector3 forward = new(
            MathF.Cos( -e.Pitch ) * MathF.Sin( -e.Yaw ),
            MathF.Sin( -e.Pitch ),
            MathF.Cos( -e.Pitch ) * MathF.Cos( -e.Yaw )
        );

        forward = Vector3.Normalize( forward );
        
        return forward;

    }

    public static EntityController Get() {
        return Instance;
    }

    public void Move( bool left, float dt ) {
        Vector3 right = Vector3.Normalize( Vector3.Cross( e.Forward, Vector3.UnitY  ) ) * e.Speed * dt;
        //Vector3 forwardFlat = Vector3.Normalize(   new Vector3( e.Forward.X, 0, e.Forward.Z ) );

        if( left ) e.RigidBody.ApplyImpulse(  right ); 
        else       e.RigidBody.ApplyImpulse( -right ); 

    }

}