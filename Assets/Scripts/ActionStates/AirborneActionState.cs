﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneActionState : ActionState {

    protected PowerState powerState;
    public float fallSpeed = 0;//Esto debería estar en airborne long jump y airborne normal, ya que se tiene que resetear si hay propel, por ejemplo (o simplemente hacer fallSpeed = 0 en propel)

    public AirborneActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
        if (rigidbody.velocity.y < 0){
            rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (powerState.gravityFallMultiplier - 1) * Time.deltaTime;
        }
        if (Mathf.Abs(rigidbody.velocity.y) > powerState.yMaxAirSpeed){
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, (rigidbody.velocity.y / Mathf.Abs(rigidbody.velocity.y)) * powerState.yMaxAirSpeed, rigidbody.velocity.z);
        }
        if(rigidbody.velocity.y < fallSpeed){
            fallSpeed = rigidbody.velocity.y;
        }
    }

    public override void OnStateEnter(ActionState lastState)  {
        base.OnStateEnter(lastState);
        this.powerState = player.powerState;
        player.StartCoroutine(MakeSureIsAirborne());
        fallSpeed = 0;
        GetPowerStateValues();
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

    protected void GetPowerStateValues() {

    }

    public override void SetMovementInput(Vector2 movementInput) {
        base.SetMovementInput(movementInput);
    }

    public override void OnJumpHighButton() {
        base.OnJumpHighButton();
    }

    public override void OnJumpLongButton() {
        base.OnJumpLongButton();
    }

    IEnumerator MakeSureIsAirborne(){
        yield return new WaitForFixedUpdate();
		//Si no ha conseguido salir del suelo vamos a landing
		if(player.groundSensor.IsTouchingGround()){
			Debug.Log("Aterrizando porque no ha llegado a salir del suelo");
			player.SetActionState(PlayerModel.ActionStates.Landing);
		}
    }
}
