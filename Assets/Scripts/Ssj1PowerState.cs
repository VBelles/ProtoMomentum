using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj1PowerState : PowerState {

    public override float groundAcceleration => 30f;
    public override float groundReleaseDeceleration => 0.5f;
    public override float groundSkiddingDeceleration => 0.5f;
    public override float maxGroundSpeed => 6f;
    public override float jumpSpeed => 7f;
    public override float shortHopSpeed => 5f;
    public override float longJumpYSpeed => 6f;  
    public override float longJumpForwardSpeed => 9f;
    public override float airAcceleration => 11f;
    public override float backwardsAirDriftFactor => 0.8f;
    public override float sidewaysAirDriftFactor => 0.5f;
    public override float yMaxAirSpeed => 15f;
    public override float gravityFallMultiplier => 2.3f;

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