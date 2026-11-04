using System.Numerics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class Enemy: Entity {

    public Model Model = new();
    public Vector3 RenderOffset = new( 0, -.5f, 0 );

    public Material Material = Raylib.LoadMaterialDefault();

    public Enemy( Map map ): base( map ) {
        
        EntityController.Begin( this );

    }

    public void LookAt( Vector3 pos, out float yaw, out float pitch ) {

        Vector3 direction = pos - RigidBody.Position;

        yaw = -MathF.Atan2( direction.X, direction.Z );

        float horizontalDistance = MathF.Sqrt(
            direction.X * direction.X +
            direction.Z * direction.Z
        );

        pitch = -MathF.Atan2( direction.Y, horizontalDistance );
        
    }

    public Vector3 GetLookDirection() {
        /*
        float pitch = Pitch * Raylib.DEG2RAD;
        float yaw   = Yaw   * Raylib.DEG2RAD;

        return new Vector3(
            MathF.Cos(pitch) * MathF.Sin(yaw),
            MathF.Sin(pitch),
            MathF.Cos(pitch) * MathF.Cos(yaw)
        );
        */

        return new Vector3(
           -MathF.Sin( Yaw ) * MathF.Cos( Pitch ),
           -MathF.Sin( Pitch ),
            MathF.Cos( Yaw ) * MathF.Cos( Pitch )
        );

    }

    public override void Tick( float dt ) {
        /*
        LookAt( Map.Player.RigidBody.Position, out Yaw, out Pitch );

        */
    }
        

    public void ShotForward() {
        float speed = 20f;

        Vector3 velocity = GetLookDirection() * speed;

        Projectile projectile = new Projectile( Map, this ).Configure<Projectile>( p => {
            p.RigidBody.Position = RigidBody.Position;  
            p.RigidBody.Velocity = velocity;
            p.RigidBody.AffectedByGravity = false;
            p.RigidBody.AddShape( new BoxShape( .1f ) );

        });

        Map.AddScene( projectile );
        
    }

    public override void Render() {

        ShaderManager.SetShader( Model, ShaderManager.Lighting() );

        Raylib.DrawModel( Model, GetRenderPosition(), 1, Color.White );
        
    }

    private Vector3 GetRenderPosition() {
        return RigidBody.Position + RenderOffset;
    } 

}