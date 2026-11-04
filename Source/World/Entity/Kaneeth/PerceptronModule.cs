using System.Numerics;
using Raylib_cs;

public class PerceptronModule {

    public float PlayerDistance  = 0;
    public bool PlayerInFOV = false;
    public Vector3 PlayerDirection = new();



    private Kaneeth Kaneeth;

    public PerceptronModule( Kaneeth kaneeth ) {
        Kaneeth = kaneeth;
    }

    public void UpdatePlayerLinearDistance() {
        
        Vector3 p = Game.Map.Player.RigidBody.Position;
        Vector3 k = Kaneeth.RigidBody.Position;

        PlayerDistance  = Vector3.Distance( p, k );
        PlayerDirection = Vector3.Normalize( p - k );

    }

    public bool IsPlayerInFOV() {

        Vector3 look = Kaneeth.GetLookDirection();

        float dot = Vector3.Dot( look, PlayerDirection );

        float halfAngle = Kaneeth.VisionAngle * 0.5f * ( MathF.PI / 180f );

        return dot >= MathF.Cos( halfAngle );

    }

    public bool IsInsideVision( Vector3 target ) {
        Vector3 origin = Kaneeth.RigidBody.Position;
        Vector3 forward = Kaneeth.GetLookDirection();

        Vector3 direction = target - origin;

        float distance = direction.Length();

        if( distance > Kaneeth.VisionDistance || distance == 0 ) return false;

        direction = Vector3.Normalize(direction);

        float dot = Vector3.Dot(forward, direction);

        float threshold = MathF.Cos( Kaneeth.VisionAngle * 0.5f * ( MathF.PI / 180f ) );

        return dot >= threshold;
    }

    public void Tick( float delta ) {
        UpdatePlayerLinearDistance();

        DebugVision();

        PlayerInFOV = IsInsideVision( Game.Map.Player.RigidBody.Position );

    }


    // --------------------------------- Debug --------------------------------- \\


    public void DebugVision() {

        Vector3 origin = Kaneeth.RigidBody.Position;
        Vector3 forward = Kaneeth.GetLookDirection();

        Vector3 referenceUp = MathF.Abs( Vector3.Dot(forward, Vector3.UnitY)) > 0.999f? Vector3.UnitZ : Vector3.UnitY;

        Vector3 right = Vector3.Normalize( Vector3.Cross(forward, referenceUp) );
    
        Vector3 up = Vector3.Normalize( Vector3.Cross(right, forward) );

        float halfAngle = Kaneeth.VisionAngle * 0.5f * ( MathF.PI / 180f );

        Vector3 previousPoint = Vector3.Zero;

        for( int i = 0; i <= Kaneeth.VisionSegments; i++ ){
            float angle = (i / (float)Kaneeth.VisionSegments) * MathF.Tau;

            Vector3 radial =
                right * MathF.Cos(angle) +
                up * MathF.Sin(angle);

            Vector3 direction =
                forward * MathF.Cos(halfAngle) +
                radial * MathF.Sin(halfAngle);

            Vector3 point = origin + direction * Kaneeth.VisionDistance;

            if( i > 0 ) Raylib.DrawLine3D( previousPoint, point, Color.Yellow );

            Raylib.DrawLine3D( origin, point, Color.Yellow );

            previousPoint = point;
            
        }

        Raylib.DrawLine3D(
            origin,
            origin + forward * Kaneeth.VisionDistance,
            Color.Green
        );
    }
}