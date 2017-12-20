using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj2PowerState : PowerState {

    public Ssj2PowerState(PlayerModel player) : base(player)
    {
        //Ground Movement
        _xGroundAcceleration = 30f;
        _groundReleaseDeceleration = 0.25f;
        _groundSkiddingDeceleration = 0.25f;
        //High Jump initial condition and impulse
        _highJumpGroundVelocityThreshold = 0f;
        _yHighJumpVelocity0 = 10.5f;
        _yHighJumpVelocity1 = 12f;
        //High Jump and falling air drift
        _horizontalAirVelocityThreshold = 0f;
        _losingMomentumGroundVelocityThreshold = 0f;
        _airAcceleration0 = 12f;
        _airAcceleration1 = 18f;
        _airAcceleration2 = 15f;
        //Long Jump
        _xLongJumpVelocity = 19f;
        _yLongJumpVelocity = 8.5f;
        //Gravity Modifiers
        _gravityLowJumpMultiplier = 2f;
        _gravityFallMultiplier = 2.3f;
        //Maximum Speeds
        _xMaxGroundSpeed = 12f;
        _xMaxAirSpeed = 12f;
        _xMaxAirSpeedLongJump = 19f;
        _yMaxAirSpeed = 20f;
        //Propel on enemy speeds
        _highJumpPropelVelocity = 15f;
        _longJumpXPropelVelocity = 15f;
        _longJumpYPropelVelocity = 4f;
    }

    public override void Tick()
    {
    }

    public override void OnStateEnter(PowerState lastState)
    {
        base.OnStateEnter(lastState);
        _player.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1, 0.55f, 0));
        _player._changeToPowerState = 0;
    }

    public override void OnStateExit(PowerState nextState)
    {
        base.OnStateExit(nextState);
    }
}
