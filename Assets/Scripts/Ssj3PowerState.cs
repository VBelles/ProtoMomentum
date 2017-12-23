using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj3PowerState : PowerState {

    public override float groundSpeed => 10f;
    public override float groundAcceleration => 30f;
    public override float maxGroundSpeed => 8f;
    public override float airSpeed => 10f;
    public override float jumpSpeed => 10f;

    public Ssj3PowerState(PlayerModel player) : base(player) {

    }

    public override void Tick() {
    }

    public override void OnStateEnter(PowerState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(PowerState nextState) {
        base.OnStateExit(nextState);
    }

}