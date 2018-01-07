using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSquatLongActionState : GroundedActionState {

	private int jumpSquatDuration = 4;

	private Coroutine waitCoroutine;

	public JumpSquatLongActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
		waitCoroutine = player.StartCoroutine(TransitionToState());
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
		player.StopCoroutine(waitCoroutine);
    }

	public override void SetMovementInput(Vector2 movementInput) {
		//base.SetMovementInput(movementInput);
	}

	public override void OnJumpHighButton(){}//no responde al input

	public override void OnJumpLongButton(){}//no responde al input

	IEnumerator TransitionToState(){
		yield return new WaitForSeconds((1/60f)*jumpSquatDuration);
		rigidbody.velocity = Vector3.up * powerState.longJumpYSpeed + player.transform.forward * powerState.longJumpForwardSpeed;
		player.SetActionState(PlayerModel.ActionStates.AirborneLongJump);
	}
}
