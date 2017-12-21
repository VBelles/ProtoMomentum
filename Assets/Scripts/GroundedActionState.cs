using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedActionState : ActionState {

    private PowerState powerState;

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
        rigidbody.velocity = new Vector3(movementInput.x * powerState.groundSpeed, rigidbody.velocity.y, movementInput.y * powerState.groundSpeed);


    }

    public override void OnJumpHighButton() {
        base.OnJumpHighButton();
        rigidbody.velocity += Vector3.up * powerState.jumpSpeed;
    }

    public override void OnJumpLongButton() {
        base.OnJumpLongButton();
        //rigidbody.velocity = new Vector2(player.facingDirection.x * xLongJumpVelocity, _yLongJumpVelocity, player.facingDirection.y * xLongJumpVelocity);
        player.isLongJump = true;
    }
}
