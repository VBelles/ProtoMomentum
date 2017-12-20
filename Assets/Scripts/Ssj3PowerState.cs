using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ssj3PowerState : PowerState {

    public Ssj3PowerState(PlayerModel player) : base(player)
    {
        //Ground Movement
        _xGroundAcceleration = 30f;
        _groundReleaseDeceleration = 0.2f;
        _groundSkiddingDeceleration = 0.2f;
        //High Jump initial condition and impulse
        _highJumpGroundVelocityThreshold = 0f;
        _yHighJumpVelocity0 = 13.5f;
        _yHighJumpVelocity1 = 15f;
        //High Jump and falling air drift
        _horizontalAirVelocityThreshold = 0f;
        _losingMomentumGroundVelocityThreshold = 0f;
        _airAcceleration0 = 18f;
        _airAcceleration1 = 24f;
        _airAcceleration2 = 21f;
        //Long Jump
        _xLongJumpVelocity = 28f;
        _yLongJumpVelocity = 10f;
        //Gravity Modifiers
        _gravityLowJumpMultiplier = 2f;
        _gravityFallMultiplier = 2.3f;
        //Maximum Speeds
        _xMaxGroundSpeed = 18f;
        _xMaxAirSpeed = 18f;
        _xMaxAirSpeedLongJump = 28f;
        _yMaxAirSpeed = 25f;
        //Propel on enemy speeds
        _highJumpPropelVelocity = 18f;
        _longJumpXPropelVelocity = 18f;
        _longJumpYPropelVelocity = 4f;
    }

    public override void Tick()
    {
    }

    public override void OnStateEnter(PowerState lastState)
    {
        base.OnStateEnter(lastState);
        _player.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
        _player._changeToPowerState = 0;
    }

    public override void OnStateExit(PowerState nextState)
    {
        base.OnStateExit(nextState);
    }
}
