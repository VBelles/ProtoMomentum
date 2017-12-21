using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedActionState : ActionState {

    private float speed = 10;
    private float jumpVelocity = 10;

    public GroundedActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
        player.isLongJump = false;
        GetPowerStateValues();
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

    public override void RefreshPowerState() {
        GetPowerStateValues();
    }

    protected void GetPowerStateValues() {

    }

    public override void SetMovementInput(Vector2 movementInput) {
        base.SetMovementInput(movementInput);
        rigidbody.velocity = new Vector3(movementInput.x * speed, rigidbody.velocity.y, movementInput.y * speed);


    }

    public override void OnJumpHighButton() {
        base.OnJumpHighButton();
        rigidbody.velocity += Vector3.up * jumpVelocity;
        //Si dejamos el cambio de estado para ground sensor podemos hacer una transición a jump squat antes de saltar, también sirve por si hay algún obstáculo que te impida saltar
    }

    public override void OnJumpLongButton() {
        base.OnJumpLongButton();
        //rigidbody.velocity = new Vector2(player.facingDirection.x * xLongJumpVelocity, _yLongJumpVelocity, player.facingDirection.y * xLongJumpVelocity);
        player.isLongJump = true;//el problema aquí es si no llegara a despegar del suelo, de momento lo hago así
    }
}
