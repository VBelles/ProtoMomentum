using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingActionState : GroundedActionState {

	private Vector2 nextMovement = new Vector2(); 
	private Vector3 landingVelocity = new Vector3();
	private float impactVelocity = 0;

	private Coroutine waitCoroutine;

	private int landingLag = 1;
	private int landingLagLong = 6;

	public LandingActionState(PlayerModel player) : base(player) {

    }

	public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
		//Guardar velocidad a la que ha llegado al suelo
		landingVelocity = rigidbody.velocity;
		//controlar velocidad, si es que se puede...

		//Coger velocidad de impacto del estado anterior
		if(lastState is AirborneActionState)
		{
			impactVelocity = (lastState as AirborneActionState).fallSpeed;
		}
		waitCoroutine = player.StartCoroutine(TransitionToState());//Necesitamos un monobehaviour para llamar a una coroutine
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
		player.StopCoroutine(waitCoroutine);
    }

	public override void Tick(){
	}

	public override void SetMovementInput(Vector2 movementInput){
		nextMovement = movementInput;
		//no responde al input
	}

	public override void OnJumpHighButton(){}//no responde al input

	public override void OnJumpLongButton(){}//no responde al input

	IEnumerator TransitionToState(){
		if(impactVelocity > -powerState.yMaxAirSpeed){
			yield return new WaitForSeconds((1/60f)*landingLag);
		}else{
			yield return new WaitForSeconds((1/60f)*landingLagLong);
		}
		
		if(nextMovement == Vector2.zero){
			player.SetActionState(PlayerModel.ActionStates.Idle);
		}else if(nextMovement.magnitude < 0.85){
			player.SetActionState(PlayerModel.ActionStates.Walk);
		}else{
			player.SetActionState(PlayerModel.ActionStates.Run);
		}
	}
}
