using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj1PowerState : PowerState {

    public override float groundSpeed => 5f;
    public override float airSpeed => 5f;
    public override float jumpSpeed => 5f;

    public Ssj1PowerState(PlayerModel player) : base(player) {

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