using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj2PowerState : PowerState {

    public override float groundAcceleration => 45f;
    public override float groundReleaseDeceleration => 0.5f;
    public override float groundSkiddingDeceleration => 0.5f;
    public override float maxGroundSpeed => 9f;
    public override float jumpSpeed => 9f;
    public override float shortHopSpeed => 6f;
    public override float longJumpYSpeed => 7f;  
    public override float longJumpForwardSpeed => 12.5f;
    public override float airAcceleration => 13f;
    public override float backwardsAirDriftFactor => 0.8f;
    public override float sidewaysAirDriftFactor => 0.5f;
    public override float yMaxAirSpeed => 18f;
    public override float gravityFallMultiplier => 2.7f;

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