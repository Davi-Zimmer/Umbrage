using System.Numerics;
using Microsoft.VisualBasic;
using Raylib_cs;
using Umbrage.World.Entity;

namespace Umbrage.World.Object; 


public class PhysicalObject: GenericEntity {
    
    public PhysicalObject( Game game ): base( game ) {
        
    }

    private Vector3 momentum = new();
    private Vector3 movement = new();


    public Vector3 getMomentum() { return momentum; }
    public Vector3 getMovement() { return movement; }

    public override Vector3 extractRealPosition() {
        return position + momentum;
    }

    public void applyMomentum( float x, float y, float z ) {
        momentum.X += x;
        momentum.Y += y;
        momentum.Z += z;
    }

    public void applyVecMomentum( Vector3 vec ) {
        momentum += vec;
    }

    public void applyVecMovement( Vector3 vec ) {
        movement.X += vec.X;
        movement.Y += vec.Z;
    }

    public void setVecMovement( Vector3 vec ) {
        movement.X = vec.X;
        movement.Y = vec.Z;
    }

    public void multiplyMomentum( float x, float y, float z ) {
        momentum.X *= x;
        momentum.Y *= y;
        momentum.Z *= z;
    }

    public void multiplyMovement( float x, float y, float z ) {
        movement.X *= x;
        movement.Y *= y;
        movement.Z *= z;
    }

    public Vector3 sumMovementVectors() {
        
        Vector3 vec = momentum;

        vec.X += movement.X;
        vec.Z += movement.Y;

        return vec;

    }

    public override void update(float delta) {
        
        if( useGravity() ) WorldPhysics.AddGravity( this, delta );


    }




}