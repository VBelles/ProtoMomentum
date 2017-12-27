using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneNormalActionState : AirborneActionState {

	public AirborneNormalActionState(PlayerModel player) : base(player) {
    }

    public override void Tick() {
		base.Tick();
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }
}
