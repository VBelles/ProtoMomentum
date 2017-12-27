using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionState : GroundedActionState {

	public RunActionState(PlayerModel player) : base(player) {

    }

	public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

	public override void SetMovementInput(Vector2 movementInput) {
		base.SetMovementInput(movementInput);
		if(rigidbody.velocity == Vector3.zero && movementInput == Vector2.zero){
			player.SetActionState(PlayerModel.ActionStates.Idle);
		}else if(rigidbody.velocity.magnitude <= maxWalkingVelocity && movementInput.magnitude <= walkingJoystickMaxTilt){
			player.SetActionState(PlayerModel.ActionStates.Walk);
		}
	}

	public override void OnJumpHighButton(){
		base.OnJumpHighButton();
	}

	public override void OnJumpLongButton(){
		base.OnJumpLongButton();
	}
}
