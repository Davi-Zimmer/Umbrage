using System.Numerics;
using Raylib_cs;

public class Kaneeth: Enemy {
    public PerceptronModule Perceptron;
    public MemoryModule Memory;
    public K_Brain      Brain;

    public float VisionAngle = 90f;
    public float VisionDistance = 15f;
    public int VisionSegments = 24;

    public Kaneeth( Map map ): base( map ) {
        Yaw   = 0;
        Pitch = 0;
        
        SetGameType( GameType.Kaneeth );

        Health.Reset( 1000 );

        Health.DecreaseObserver.Add( () => {

            if( Health.Value != 0 ) return;

            Map.RemoveScene( RigidBody );

            Game.SetStatus( Game.Status.Win );

        });

        Perceptron = new( this );
        Brain      = new( this );
        Memory     = new( this );

    }

    public override void Tick( float delta ) {
        
        ExecuteKeys();

        Perceptron.Tick( delta );
        
        Memory.Tick();

    }

    private void UpdateMemory() {
        
    }

    private void ExecuteKeys() {

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


        //Console.Write( $"Pitch {Pitch}, Yaw: {Yaw}\n" );
    }



}