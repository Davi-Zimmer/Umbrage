using System.Numerics;
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

    public static Matrix4x4 MatrixTransformRigidBody( RigidBody b ) {

        JMatrix ori = JMatrix.CreateFromQuaternion( b.Orientation );
        JVector pos = b.Position;

        return new Matrix4x4(
            ori.M11, ori.M12, ori.M13, pos.X,
            ori.M21, ori.M22, ori.M23, pos.Y,
            ori.M31, ori.M32, ori.M33, pos.Z,
            0, 0, 0, 1.0f
        );

    }

    public static Matrix4x4 MatrixRotateX( float x ) {
        /*
            não sei quem foi o primeiro a decidir que seria uma boa ideia de trocar o eixo Y por X ou Z.
            seja la quem for deveriam condenar o sujeito a 1000000000000000000 horas programando em binário
            é só mandar um PC sem OS via correio com uma cartinha dizendo que ele vai ter que
            construir cada depencencia, ferramenta e o OS em malbolge antes da punição se quer iniciar.
            cada execução de código que ele for fazer, vai ter que reescrever o proprio compilador
            em C++ moderno sem documentação ou headers
            talvez durante essa jornada o sujeito compreenda a dor que é não saber matematica direito
            e tentar rotacionar um objeto no eixo que em teoria deveria estar certo
            (ps: sei que é desproporcional o erro e a sentença então um ajuste,
            deveria condenar ele programar tudo com um pc de 2048kb de ram 8bits e 6hz )
        */
        return Raymath.MatrixRotateY( x * Raylib.DEG2RAD );

        /*
        Matrix4x4 rotX = Raymath.MatrixRotateX(x * Raylib.DEG2RAD);
        Matrix4x4 rotY = Raymath.MatrixRotateY(y * Raylib.DEG2RAD);
        Matrix4x4 rotZ = Raymath.MatrixRotateZ(z * Raylib.DEG2RAD);
        Matrix4x4 rotation = Raymath.MatrixMultiply(
            rotX,
            Raymath.MatrixMultiply(rotY, rotZ)
        );
        */
    }

    public static Matrix4x4 MatrixRotateY( float y ) {
      
        return Raymath.MatrixRotateX( y * Raylib.DEG2RAD );

        /*
        Matrix4x4 rotX = Raymath.MatrixRotateX(x * Raylib.DEG2RAD);
        Matrix4x4 rotY = Raymath.MatrixRotateY(y * Raylib.DEG2RAD);
        Matrix4x4 rotZ = Raymath.MatrixRotateZ(z * Raylib.DEG2RAD);
        Matrix4x4 rotation = Raymath.MatrixMultiply(
            rotX,
            Raymath.MatrixMultiply(rotY, rotZ)
        );
        */
    }

    public static Matrix4x4 MatrixRotateXY( float x, float y ) {
        
        return Matrix4x4.Multiply(
            MatrixRotateX( x ),
            MatrixRotateY( y )
        );

    }


}