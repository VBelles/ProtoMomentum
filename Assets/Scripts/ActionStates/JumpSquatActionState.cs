using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSquatActionState : GroundedActionState {

	private int jumpSquatDuration = 3;

	private Coroutine waitCoroutine;
	private float enterVelocity;
	private Vector3 enterDirection = new Vector3();

	public JumpSquatActionState(PlayerModel player) : base(player) {

    }

	public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
		enterDirection = rigidbody.velocity;
		enterVelocity = enterDirection.magnitude;
		enterDirection.Normalize();
		waitCoroutine = player.StartCoroutine(TransitionToState());
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
		player.StopCoroutine(waitCoroutine);
    }

	public override void Tick() {
		rigidbody.AddForce(enterDirection * powerState.groundAcceleration);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, enterVelocity); 
    }

	public override void SetMovementInput(Vector2 movementInput) {
		//base.SetMovementInput(movementInput);
	}

	public override void OnJumpHighButton(){}//no responde al input

	public override void OnJumpLongButton(){}//no responde al input

	IEnumerator TransitionToState(){
		yield return new WaitForSeconds((1/60f)*jumpSquatDuration);
		if(player.jumpButtonPressed){
			rigidbody.velocity += Vector3.up * powerState.jumpSpeed;
		}else{
			rigidbody.velocity += Vector3.up * powerState.shortHopSpeed;
		}
		player.SetActionState(PlayerModel.ActionStates.AirborneNormal);
	}
}
