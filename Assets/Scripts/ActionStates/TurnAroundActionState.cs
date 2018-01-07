using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundActionState : GroundedActionState {

	private Coroutine waitCoroutine;
	private int turnAroundLag = 6;

	private Vector3 exitDirection = new Vector3();

	public TurnAroundActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
		//Rotar hasta el ángulo de salida en "turnAroundLag" tiempo
		player.transform.forward = Vector3.RotateTowards(player.transform.forward, exitDirection, Mathf.PI*60*Time.deltaTime/turnAroundLag , 0.0f);
	}

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
		if(lastState is GroundedActionState){
			exitDirection = (lastState as GroundedActionState).movement;
		}
		waitCoroutine = player.StartCoroutine(TransitionToState());
		rigidbody.velocity *= powerState.groundSkiddingDeceleration;
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
		player.StopCoroutine(waitCoroutine);
    }

	public override void SetMovementInput(Vector2 movementInput){}//Ni caso a este input, me da asco (escupe)

	IEnumerator TransitionToState(){
		yield return new WaitForSeconds((1/60f)*turnAroundLag);
		rigidbody.AddForce(exitDirection * powerState.groundAcceleration);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxWalkingVelocity);
		player.SetActionState(PlayerModel.ActionStates.Walk);
	}
}
