using Raylib_cs;

public class Projectile: Object {
    
    public Projectile( Map map ): base( map ) {
        
    }


    public override void Render() {
        
        Raylib.DrawCube( RigidBody.Position, .1f, .1f, .1f, Color.Red );

    }
}