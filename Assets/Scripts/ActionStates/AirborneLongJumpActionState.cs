using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneLongJumpActionState : AirborneActionState {

	public Vector3 movement = new Vector3();

    private Vector3 airDriftVelocity = new Vector3();
    private float sidewaysdMinAngle = 45;
    private float backwardsdMinAngle = 135;
    private float sidewaysMaxDotProduct;
    private float backwardsMaxDotProduct;

	public AirborneLongJumpActionState(PlayerModel player) : base(player) {
		sidewaysMaxDotProduct = Mathf.Cos((Mathf.PI/180)*sidewaysdMinAngle);
        backwardsMaxDotProduct = Mathf.Cos((Mathf.PI/180)*backwardsdMinAngle);
    }

    public override void Tick() {
		if (Mathf.Abs(rigidbody.velocity.y) > powerState.yMaxAirSpeed){
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, (rigidbody.velocity.y / Mathf.Abs(rigidbody.velocity.y)) * powerState.yMaxAirSpeed, rigidbody.velocity.z);
        }
        if(rigidbody.velocity.y < fallSpeed){
            fallSpeed = rigidbody.velocity.y;
        }
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

	public override void SetMovementInput(Vector2 movementInput){
        movementInput.Normalize();
        
        movement.Set(movementInput.x, 0, movementInput.y);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();

        //factor a movement según el ángulo respecto a facing direction
        if(Vector3.Dot(player.transform.forward, movement) > sidewaysMaxDotProduct){       
            movement *= powerState.airAcceleration * powerState.sidewaysAirDriftFactor;//aquí vamos a poner un factor más bajo de lo normal
        }else if(Vector3.Dot(player.transform.forward, movement) > backwardsMaxDotProduct){
            movement *= powerState.airAcceleration * powerState.sidewaysAirDriftFactor;//aceleración de lado    
        }else{
            movement *= powerState.airAcceleration * 1;
        }
         
        rigidbody.AddForce(movement);

        movement.Set(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if(movement.magnitude > powerState.longJumpForwardSpeed){
            movement = Vector3.ClampMagnitude(movement, powerState.longJumpForwardSpeed);
            movement.Set(movement.x, rigidbody.velocity.y, movement.z);
            rigidbody.velocity = movement;
        }
        
    }
}
