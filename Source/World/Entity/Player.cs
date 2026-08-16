using System.Numerics;
using Raylib_cs;
using Umbrage.Utils;
using Umbrage.World.Object;

namespace Umbrage.World.Entity;

public class Player : PhysicalObject {
    public delegate void ConfigPlayer( Player player );
    
    // -------------------------------------- Variables -------------------------------------- \\
    public Camera3D camera;
    public float yaw;
    public float pitch;
    
    private Vector3 cameraAnchor = new();
    // -------------------------------------- Getter -------------------------------------- \\
    public Vector3 getCameraAnchor() { return cameraAnchor; }

    // -------------------------------------- Setter -------------------------------------- \\
    public Player setCameraAnchor( Vector3 v ) { cameraAnchor = v;  return this; }


    // -------------------------------------- Main -------------------------------------- \\
    public Player( Game game ) : base( game ) {

        camera = new Camera3D(
            position,
            position + Vector3.UnitZ,
            Vector3.UnitY,
            90f,
            CameraProjection.Perspective
        );

    }

    private void events( Vector3 v ) {
        /*
        if( Raylib.IsMouseButtonDown( MouseButton.Left ) ) {
            


        }
        */

    }

    // -------------------------------------- Methods -------------------------------------- \\
    public override void update( float delta ) {

        Vector3 forward = PlayerController.update( this, ref camera, delta );

        // camera.Position.X = position.X + getMomentum().X; //+ getMovement().X;
        // camera.Position.Z = position.Z + getMomentum().Z; //+ getMovement().Y; // not wrong
        // camera.Position.Y = position.Y + getMomentum().Y;

        WorldPhysics.AddGravity( this, delta );

        multiplyMovement( .9f, .9f, .9f );

        // Raylib.DrawCube( position + getMomentum(), 10, 10, 10, Color.White );

    }

}