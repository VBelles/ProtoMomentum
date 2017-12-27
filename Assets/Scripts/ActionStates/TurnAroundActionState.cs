using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAroundActionState : GroundedActionState {

	private Coroutine waitCoroutine;
	private int turnAroundLag = 3;

	public TurnAroundActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
		//Rotar hasta el ángulo de salida en "turnAroundLag" tiempo
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
		waitCoroutine = player.StartCoroutine(TransitionToState());
		rigidbody.velocity = Vector3.zero;
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
		player.StopCoroutine(waitCoroutine);
    }

	public override void SetMovementInput(Vector2 movementInput){}//Ni caso a este input, me da asco (escupe)

	IEnumerator TransitionToState(){
		yield return new WaitForSeconds((1/60f)*turnAroundLag);
		rigidbody.AddForce(movement * powerState.groundAcceleration);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxWalkingVelocity);
		player.SetActionState(PlayerModel.ActionStates.Walk);
	}
}
