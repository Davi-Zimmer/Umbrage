using System.Numerics;
using Jitter2;
using Raylib_cs;

public class Map {
    Camera3D camera = new() {
        Position   = new Vector3( -20.0f, 8.0f, 10.0f ),
        Target     = new Vector3(   0.0f, 4.0f, 0.0f  ),
        Up         = new Vector3(   0.0f, 1.0f, 0.0f  ),
        FovY       = 45.0f,
        Projection = CameraProjection.Perspective
    };

    World world = new();

    public void Update( float delta ) {

        Raylib.BeginMode3D( camera );
        {
            world.Step( delta, true );


        }
        Raylib.EndMode3D();


    }

}