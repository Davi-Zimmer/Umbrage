
using System.Numerics;
using Raylib_cs;
using Umbrage.Graphycs;

namespace Umbrage.World.Entity;

public class TestEntity : GenericEntity {
  
    public Mesh mesh;
    public Material material;
    
    public TestEntity( Map game ): base( game ) {
        mesh            = Raylib.GenMeshCube( Size.X, Size.Y, Size.Z );
        material        = Raylib.LoadMaterialDefault();
        material.Shader = Shaders.GetShader( Shaders.Names.Test );
        Color           = Color.White;
    }

    public override void UpdateConfig() {
        mesh = Raylib.GenMeshCube( Size.X, Size.Y, Size.Z );
    }

    public override void Render() {

        Raylib.DrawMesh(
            mesh,
            material,
            Raymath.MatrixTranslate(
                RigidBody.Position.X,
                RigidBody.Position.Y,
                RigidBody.Position.Z
            )
        );

    }

}