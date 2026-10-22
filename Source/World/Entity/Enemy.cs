using System.Numerics;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_cs;

public class Enemy: Entity {

    public Model Model = new();
    public Vector3 RenderOffset = new( 0, -.5f, 0 );
    
    public Enemy( Map map ): base( map ) {

        Health.DecreaseObserver.Add( () => {

            if( Health.Value != 0 ) return;

            Map.RemoveScene( RigidBody );

            Game.SetStatus( Game.Status.Win );

        });

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

    public override void Tick( float dt ) {
        /*
        LookAt( Map.Player.RigidBody.Position, out Yaw, out Pitch );

        */
        Model.Transform = Matrix4x4.CreateRotationY( Yaw ) * Matrix4x4.CreateRotationX( -Pitch );
        
        if( Raylib.IsKeyPressed( KeyboardKey.R ) ) ShotForward();


        EntityController ec = EntityController.Get();
        Forward = ec.GetForward();

        if( Raylib.IsKeyDown( KeyboardKey.Up   ) ) RigidBody.ApplyImpulse(  Forward * Speed * .1f ); 
        if( Raylib.IsKeyDown( KeyboardKey.Down ) ) RigidBody.ApplyImpulse( -Forward * Speed * .1f ); 
    
        if( Raylib.IsKeyDown( KeyboardKey.Left  ) ) ec.Move( false, .1f );
        if( Raylib.IsKeyDown( KeyboardKey.Right ) ) ec.Move( true , .1f );

        float t = .01f;

        if( Raylib.IsKeyDown( KeyboardKey.Kp4 ) )  Yaw += t;
        if( Raylib.IsKeyDown( KeyboardKey.Kp6  ) ) Yaw -= t;

        if( Raylib.IsKeyDown( KeyboardKey.Kp8 ) )  Pitch += t;
        if( Raylib.IsKeyDown( KeyboardKey.Kp2  ) ) Pitch -= t;
    }

    public void ShotForward() {
        float speed = 20f;

        Vector3 velocity = new Vector3(
            -MathF.Sin( Yaw ) * MathF.Cos( Pitch ),
            -MathF.Sin( Pitch ),
             MathF.Cos( Yaw ) * MathF.Cos( Pitch )
        ) * speed;

        Projectile projectile = new Projectile( Map, this ).Configure<Projectile>( p => {
            p.RigidBody.Position = RigidBody.Position;  
            p.RigidBody.Velocity = velocity;
            p.RigidBody.AffectedByGravity = false;
            p.RigidBody.AddShape( new BoxShape( .1f ) );

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