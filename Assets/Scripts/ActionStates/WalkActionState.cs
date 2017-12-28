using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkActionState : GroundedActionState {//Básicamente se diferencia de run por la animación y el turn around

	public WalkActionState(PlayerModel player) : base(player) {

    }

	public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

	public override void SetMovementInput(Vector2 movementInput) {
		base.SetMovementInput(movementInput);
		if(isRunning){
			player.SetActionState(PlayerModel.ActionStates.Run);
		}
		if(rigidbody.velocity == Vector3.zero && movementInput == Vector2.zero){
			player.SetActionState(PlayerModel.ActionStates.Idle);
		}

		player.lastMovementInput = movementInput;
	}

	public override void OnJumpHighButton(){
		base.OnJumpHighButton();
	}

	public override void OnJumpLongButton(){
		base.OnJumpLongButton();
	}
}
