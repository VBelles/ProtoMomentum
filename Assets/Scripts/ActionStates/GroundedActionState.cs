using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedActionState : ActionState {

    protected PowerState powerState;
    protected Vector3 movement = new Vector3();
    protected bool isRunning = false;
    protected float maxWalkingVelocity = 4f;

    public GroundedActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
        player.isLongJump = false;
        RefreshPowerState();
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

    public override void RefreshPowerState() {
        this.powerState = player.powerState;
    }     

    public override void SetMovementInput(Vector2 movementInput) {
        base.SetMovementInput(movementInput);
        if(movementInput.magnitude < 0.85f && rigidbody.velocity.magnitude <= maxWalkingVelocity + 1){
            isRunning = false;
        }else{
            isRunning = true;
        }
  
        movementInput.Normalize();

        if(lastMovementInput != Vector2.zero && movementInput == Vector2.zero){//Parón
            rigidbody.velocity *= powerState.groundReleaseDeceleration;
        }

        movement.Set(movementInput.x, 0, movementInput.y);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movementInput.Normalize();
        rigidbody.AddForce(movement * powerState.groundAcceleration);

        if(isRunning)
        {
            if(Mathf.Abs(rigidbody.velocity.magnitude) > powerState.maxGroundSpeed){
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, powerState.maxGroundSpeed);
            }
        }else{
            if(Mathf.Abs(rigidbody.velocity.magnitude) > maxWalkingVelocity){
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxWalkingVelocity);   
            }
        }
    }

    public override void OnJumpHighButton() {
        base.OnJumpHighButton();
        //rigidbody.velocity += Vector3.up * powerState.jumpSpeed;
        player.SetActionState(PlayerModel.ActionStates.JumpSquat);//Lo cambiamos aquí para que no se clampee el valor, luego aquí cambiará a jump squat, no a airborne
    }

    public override void OnJumpLongButton() {
        base.OnJumpLongButton();
        //rigidbody.velocity = new Vector2(player.facingDirection.x * xLongJumpVelocity, _yLongJumpVelocity, player.facingDirection.y * xLongJumpVelocity);
        player.isLongJump = true;
    }
}
