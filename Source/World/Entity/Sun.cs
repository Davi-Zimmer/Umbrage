using System.Numerics;
using Raylib_cs;

public class Sun: Light {
    
    public Sun( Map map ): base( map ) {

    }

    public Vector3 GetLightPos( float time ) {
        return new Vector3(
            MathF.Cos(time * Speed) * LightRadius,
            Height,
            MathF.Sin(time * Speed) * LightRadius
        );
    }

    public override void Tick( float delta ) {

        float time = (float)Raylib.GetTime();

        Position = GetLightPos( time );

    }


}