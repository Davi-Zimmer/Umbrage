using System.Numerics;
using Raylib_cs;

public class Enemy: Object {

    public Model Model = new();
    public Vector3 RenderOffset = new( 0, -.5f, 0 );
    
    public float Pitch = 0;
    public float Yaw = 0;

    public Bar Health = new( 100 );

    public Enemy( Map map ): base( map ) {

        Health.DecreaseObserver.Add( () => {

            if( Health.Value != 0 ) return;

            Console.WriteLine("DEAD");

        });

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

    public override void Tick( float dt ) {

        LookAt( Map.Player.RigidBody.Position, out Yaw, out Pitch );

        Model.Transform = Matrix4x4.CreateRotationY( Yaw ) * Matrix4x4.CreateRotationX( -Pitch );

        if( Raylib.IsKeyPressed( KeyboardKey.R ) ) ShotForward();

    }

    public void ShotForward() {
        float speed = 20f;

        Vector3 velocity = new Vector3(
            -MathF.Sin( Yaw ) * MathF.Cos( Pitch ),
            -MathF.Sin( Pitch ),
             MathF.Cos( Yaw ) * MathF.Cos( Pitch )
        ) * speed;

        Projectile projectile = new Projectile( Map ).Configure<Projectile>( p => {
            p.RigidBody.Position = RigidBody.Position;  
            p.RigidBody.Velocity = velocity;
            p.RigidBody.AffectedByGravity = false;

        });

        Map.AddScene( projectile );
        
    }

    public override void Render() {
        
        Raylib.DrawModel( 
            Model,
            GetRenderPosition(),
            1,
            Color.White
        );
        
    }

    private Vector3 GetRenderPosition() {
        return RigidBody.Position + RenderOffset;
    } 

}