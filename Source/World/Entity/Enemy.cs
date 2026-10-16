using System.Numerics;
using Raylib_cs;

public class Enemy: Object {

    public Model Model = new();
    public Vector3 RenderOffset = new( 0, -.5f, 0 );
    
    public float Pitch = 0;
    public float Yaw = 0;


    public Enemy( Map map ): base( map ) {

    }

    public void LookXAt( Vector3 pos ) {

        Vector3 d = pos - RigidBody.Position;

        float horizontal = MathF.Sqrt( d.X * d.X + d.Z * d.Z );

        Yaw   = MathF.Atan2( d.X, d.Z )        * ( 180f / MathF.PI );
        Pitch = MathF.Atan2( d.Y, horizontal ) * ( 180f / MathF.PI );
    }


    public void LookYAt( Vector3 pos ) {

        Vector3 dir = Vector3.Normalize( RigidBody.Position - pos );


        float pitch = MathF.Atan2(
            dir.Y,
            MathF.Sqrt( dir.X * dir.X + dir.Y * dir.Y )
        );

        Pitch = pitch * ( 180f / MathF.PI );

    }

    public override void Tick( float dt ) {

        LookXAt( Map.Player.RigidBody.Position );

        Model.Transform = Utils.MatrixRotateX( Yaw );

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