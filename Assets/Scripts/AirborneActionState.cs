using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneActionState : ActionState {

    private PowerState powerState;

    public AirborneActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
        if (rigidbody.velocity.y < 0){
                rigidbody.velocity += Vector3.up * Physics2D.gravity.y * (powerState.gravityFallMultiplier - 1) * Time.deltaTime;
        }
        if (Mathf.Abs(rigidbody.velocity.y) > powerState.yMaxAirSpeed)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, (rigidbody.velocity.y / Mathf.Abs(rigidbody.velocity.y)) * powerState.yMaxAirSpeed, rigidbody.velocity.z);
        }
    }

    public override void OnStateEnter(ActionState lastState)  {
        base.OnStateEnter(lastState);
        this.powerState = player.powerState;
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
}
