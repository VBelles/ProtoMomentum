using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedActionState : ActionState {

    float _groundSkiddingDeceleration = 0f;
    float _groundReleaseDeceleration = 0f;
    float _xMaxGroundSpeed = 0f;
    float _xGroundAcceleration = 0f;
    float _highJumpGroundVelocityThreshold = 0f;
    float _yHighJumpVelocity1 = 0f;
    float _yHighJumpVelocity0 = 0f;
    float _xLongJumpVelocity = 0f;
    float _yLongJumpVelocity = 0f;

    public GroundedActionState(PlayerModel player) : base(player)
    {
    }

    public override void Tick()
    {
    }

    public override void OnStateEnter(ActionState lastState)
    {
        base.OnStateEnter(lastState);
        _player._isLongJump = false;
        GetPowerStateValues();
    }

    public override void OnStateExit(ActionState nextState)
    {
        base.OnStateExit(nextState);
    }

    public override void RefreshPowerState()
    {
        GetPowerStateValues();
    }

    protected void GetPowerStateValues()
    {
        _groundSkiddingDeceleration = _player._currentPowerState.GetGroundSkiddingDeceleration();
        _groundReleaseDeceleration = _player._currentPowerState.GetGroundRealeaseDeceleration();
        _xMaxGroundSpeed = _player._currentPowerState.GetXMaxGroundSpeed();
        _xGroundAcceleration = _player._currentPowerState.GetXGroundAcceleration();
        _highJumpGroundVelocityThreshold = _player._currentPowerState.GetHighJumpGroundVelocityThreshold();
        _yHighJumpVelocity1 = _player._currentPowerState.GetYHighJumpVelocity1();
        _yHighJumpVelocity0 = _player._currentPowerState.GetYHighJumpVelocity0();
        _xLongJumpVelocity = _player._currentPowerState.GetXLongJumpVelocity();
        _yLongJumpVelocity = _player._currentPowerState.GetYLongJumpVelocity();
    }

    public override void MovementInput(float x, float y)
    {
        if (_lastX * x < 0)//Derrape
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * _groundSkiddingDeceleration, _rigidbody.velocity.y);
        }

        if (_lastX != 0 && x == 0)//Parón
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x * _groundReleaseDeceleration, _rigidbody.velocity.y);
        }
        if (Mathf.Abs(_rigidbody.velocity.x) < _xMaxGroundSpeed)//Si no va a velocidad máxima, acelera
        {
            _rigidbody.AddForce(Vector2.right * x * _xGroundAcceleration);
        }

        if (Mathf.Abs(_rigidbody.velocity.x) >= _xMaxGroundSpeed)//Restringe velocidad máxima  
        {
            _rigidbody.velocity = new Vector2((_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x)) * _xMaxGroundSpeed, _rigidbody.velocity.y);
        }

        base.MovementInput(x, y);

    }

    public override void OnJumpHighButton()
    {
        base.OnJumpHighButton();
        if (Mathf.Abs(_rigidbody.velocity.x) > _highJumpGroundVelocityThreshold)
        {
            _rigidbody.velocity += Vector2.up * _yHighJumpVelocity1;
        }
        else
        {
            _rigidbody.velocity += Vector2.up * _yHighJumpVelocity0;
        }
        //Si dejamos el cambio de estado para ground sensor podemos hacer una transición a jump squat antes de saltar, también sirve por si hay algún obstáculo que te impida saltar
    }

    public override void OnJumpLongButton()
    {
        base.OnJumpLongButton();
        _rigidbody.velocity = new Vector2(_player._facingDirection * _xLongJumpVelocity, _yLongJumpVelocity);
        _player._isLongJump = true;//el problema aquí es si no llegara a despegar del suelo, de momento lo hago así
    }
}
