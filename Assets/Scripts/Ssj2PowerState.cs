using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj2PowerState : PowerState {

    public override float groundAcceleration => 30f;
    public override float groundReleaseDeceleration => 0.5f;
    public override float groundSkiddingDeceleration => 0.5f;
    public override float maxGroundSpeed => 8f;
    public override float jumpSpeed => 10f;
    public override float shortHopSpeed => 5f;
    public override float airAcceleration => 11f;
    public override float backwardsAirDriftFactor => 1f;
    public override float sidewaysAirDriftFactor => 1f;
    public override float yMaxAirSpeed => 15f;
    public override float gravityFallMultiplier => 2.3f;

    public Ssj2PowerState(PlayerModel player) : base(player) {

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