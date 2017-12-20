using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerState {
    //Ground Movement
    protected float _xGroundAcceleration;
    protected float _groundReleaseDeceleration;
    protected float _groundSkiddingDeceleration;
    //High Jump initial condition and impulse
    protected float _highJumpGroundVelocityThreshold;
    protected float _yHighJumpVelocity0;
    protected float _yHighJumpVelocity1;
    //High Jump and falling air drift
    protected float _horizontalAirVelocityThreshold;
    protected float _losingMomentumGroundVelocityThreshold;
    protected float _airAcceleration0;
    protected float _airAcceleration1;
    protected float _airAcceleration2;
    //Long Jump
    protected float _xLongJumpVelocity;
    protected float _yLongJumpVelocity;
    //Gravity Modifiers
    protected float _gravityLowJumpMultiplier;
    protected float _gravityFallMultiplier;
    //Maximum Speeds
    protected float _xMaxGroundSpeed;
    protected float _xMaxAirSpeed;
    protected float _xMaxAirSpeedLongJump;
    protected float _yMaxAirSpeed;
    //Propel on enemy speeds
    protected float _highJumpPropelVelocity;
    protected float _longJumpXPropelVelocity;
    protected float _longJumpYPropelVelocity;


    protected PowerState _lastState;
    protected PowerState _nextState;
    protected PlayerModel _player;


    public PowerState(PlayerModel player)
    {
        _player = player;
    }

    public abstract void Tick();

    public virtual void OnStateEnter(PowerState lastState) { _lastState = lastState; }

    public virtual void OnStateExit(PowerState nextState) { _nextState = nextState; }

    public virtual float GetXGroundAcceleration() { return _xGroundAcceleration; }
    public virtual float GetGroundRealeaseDeceleration() { return _groundReleaseDeceleration; }
    public virtual float GetGroundSkiddingDeceleration() { return _groundSkiddingDeceleration; }

    public virtual float GetHighJumpGroundVelocityThreshold() { return _highJumpGroundVelocityThreshold; }
    public virtual float GetYHighJumpVelocity0() { return _yHighJumpVelocity0; }
    public virtual float GetYHighJumpVelocity1() { return _yHighJumpVelocity1; }

    public virtual float GetHorizontalAirVelocityThreshold() { return _horizontalAirVelocityThreshold; }
    public virtual float GetLosingMomentumGroundVelocityThreshold() { return _losingMomentumGroundVelocityThreshold; }
    public virtual float GetAirAcceleration0() { return _airAcceleration0; }
    public virtual float GetAirAcceleration1() { return _airAcceleration1; }
    public virtual float GetAirAcceleration2() { return _airAcceleration2; }

    public virtual float GetXLongJumpVelocity() { return _xLongJumpVelocity; }
    public virtual float GetYLongJumpVelocity() { return _yLongJumpVelocity; }

    public virtual float GetGravityLowJumpMultiplier() { return _gravityLowJumpMultiplier; }
    public virtual float GetGravityFallMultiplier() { return _gravityFallMultiplier; }

    public virtual float GetXMaxGroundSpeed() { return _xMaxGroundSpeed; }
    public virtual float GetXMaxAirSpeed() { return _xMaxAirSpeed; }
    public virtual float GetXMaxAirSpeedLongJump() { return _xMaxAirSpeedLongJump; }
    public virtual float GetYMaxAirSpeed() { return _yMaxAirSpeed; }

    public virtual float GetHighJumpPropelVelocity() { return _highJumpPropelVelocity; }
    public virtual float GetLongJumpXPropelVelocity() { return _longJumpXPropelVelocity; }
    public virtual float GetLongJumpYPropelVelocity() { return _longJumpYPropelVelocity; }
}
