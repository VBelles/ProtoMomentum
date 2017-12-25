using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleActionState : GroundedActionState {

	public IdleActionState(PlayerModel player) : base(player) {

    }

	public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

	public override void SetMovementInput(Vector2 movementInput){
		if(movementInput.magnitude != 0){
			if(movementInput.magnitude < 0.85f){
				player.SetActionState(PlayerModel.ActionStates.Walk);
			}else{
				player.SetActionState(PlayerModel.ActionStates.Run);
			}	
		movementInput.Normalize();
		movement.Set(movementInput.x, 0, movementInput.y);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movementInput.Normalize();
        rigidbody.AddForce(movement * powerState.groundAcceleration);
		}
		if(Mathf.Abs(rigidbody.velocity.magnitude) > maxWalkingVelocity){
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxWalkingVelocity);   
        }
	}

	public override void OnJumpHighButton(){
		base.OnJumpHighButton();
	}

	public override void OnJumpLongButton(){
		base.OnJumpLongButton();
	}
}
