using System.Numerics;

public class K_Perceptron {
    private Kaneeth Kaneeth;

    public K_Perceptron( Kaneeth kaneeth ) {
        Kaneeth = kaneeth;
    }

    public float PlayerLinearDistance = 0;

    public void UpdatePlayerLinearDistance() {
        
        Vector3 p = Game.Map.Player.RigidBody.Position;
        Vector3 k = Kaneeth.RigidBody.Position;

        float x = MathF.Pow( p.X - k.X, 2 );
        float y = MathF.Pow( p.Y - k.Y, 2 );
        float z = MathF.Pow( p.Z - k.Z, 2 );

        PlayerLinearDistance =  x + y + z;

    }

}