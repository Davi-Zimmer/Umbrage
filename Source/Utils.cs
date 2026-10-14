using System.Runtime.CompilerServices;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_cs;

class Utils {
    
    public static unsafe void CreateMapCollider( Model model, RigidBody body ) {
        List<JTriangle> triangles = new();

        for( int m = 0; m < model.MeshCount; m++ ){
            Mesh mesh = model.Meshes[m];

            ReadOnlySpan<JVector> vertices = mesh.VerticesAs<JVector>();

            for (int i = 0; i < mesh.TriangleCount; i++) {
                JVector a;
                JVector b;
                JVector c;

                if( mesh.Indices != null ) {
                    int index = i * 3;

                    a = vertices[mesh.Indices[index + 0]];
                    b = vertices[mesh.Indices[index + 1]];
                    c = vertices[mesh.Indices[index + 2]];
                } else {
                    int vertex = i * 3;

                    a = vertices[vertex + 0];
                    b = vertices[vertex + 1];
                    c = vertices[vertex + 2];
                }

                triangles.Add( new JTriangle( a, b, c ) );
            
            }
        
        }

        TriangleMesh triangleMesh = new TriangleMesh( triangles, true );

        for( int i = 0; i < triangles.Count; i++ ) {

            body.AddShape( 
                new TriangleShape( triangleMesh, i ),
                MassInertiaUpdateMode.Preserve
            );

        }

    }

}