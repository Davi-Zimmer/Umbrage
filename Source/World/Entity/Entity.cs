using System.Numerics;
using Raylib_cs;

public class Entity : GenericEntity {

    public float Yaw = 0;
    public float Pitch = 0;
    public float Speed = 2;
    public int MaxDash = 3;
    public int DashCount = 0;
    public float DashSpeed = 50f;

    public Bar Health = new( 100 );
    public Vector3 Forward = new();

    public Entity( Map map ): base( map ) {
        
        
    }

}