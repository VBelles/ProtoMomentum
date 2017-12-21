using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneActionState : ActionState {


    public AirborneActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {

    }

    public override void OnStateEnter(ActionState lastState)  {
        base.OnStateEnter(lastState);

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
