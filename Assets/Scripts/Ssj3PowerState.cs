using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj3PowerState : PowerState {

    public override float groundAcceleration => 60f;
    public override float groundReleaseDeceleration => 0.5f;
    public override float groundSkiddingDeceleration => 0.5f;
    public override float maxGroundSpeed => 13f;
    public override float jumpSpeed => 12f;
    public override float shortHopSpeed => 7f;
    public override float longJumpYSpeed => 8f;  
    public override float longJumpForwardSpeed => 17f;
    public override float airAcceleration => 15f;
    public override float backwardsAirDriftFactor => 0.8f;
    public override float sidewaysAirDriftFactor => 0.5f;
    public override float yMaxAirSpeed => 21f;
    public override float gravityFallMultiplier => 3.1f;

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