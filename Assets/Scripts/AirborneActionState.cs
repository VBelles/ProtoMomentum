using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneActionState : ActionState {

    float _gravityFallMultiplier = 0f;
    float _gravityLowJumpMultiplier = 0f;
    float _yMaxAirSpeed = 0f;
    float _horizontalAirVelocityThreshold = 0f;
    float _airAcceleration0 = 0f;
    float _airAcceleration1 = 0f;
    float _airAcceleration2 = 0f;
    float _losingMomentumGroundVelocityThreshold = 0f;
    float _xMaxAirSpeed = 0f;
    float _xMaxAirSpeedLongJump = 0f;
    float _highJumpPropelVelocity = 0f;
    float _longJumpXPropelVelocity = 0f;
    float _longJumpYPropelVelocity = 0f;

    float _initialXVelocity = 0f;
    

    public AirborneActionState(PlayerModel player) : base(player)
    {
    }

    public override void Tick()
    {
        if (!_player._isLongJump)
        {
            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_gravityFallMultiplier - 1) * Time.deltaTime;
            }
            else if (_rigidbody.velocity.y > 0 && _player._jumpButtonPressed == false)
            {
                _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (_gravityLowJumpMultiplier - 1) * Time.deltaTime;
            }

            if (Mathf.Abs(_rigidbody.velocity.y) > _yMaxAirSpeed)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, (_rigidbody.velocity.y / Mathf.Abs(_rigidbody.velocity.y)) * _yMaxAirSpeed);
            }
        }
    }

    public override void OnStateEnter(ActionState lastState)
    {
        base.OnStateEnter(lastState);
        _initialXVelocity = _rigidbody.velocity.x;

        GetPowerStateValues();
    }

    public override void OnStateExit(ActionState nextState)
    {
        base.OnStateExit(nextState);
    }

    protected void GetPowerStateValues()
    {
        _gravityFallMultiplier = _player._currentPowerState.GetGravityFallMultiplier();
        _gravityLowJumpMultiplier = _player._currentPowerState.GetGravityLowJumpMultiplier();
        _yMaxAirSpeed = _player._currentPowerState.GetYMaxAirSpeed();
        _horizontalAirVelocityThreshold = _player._currentPowerState.GetHorizontalAirVelocityThreshold();
        _airAcceleration0 = _player._currentPowerState.GetAirAcceleration0();
        _airAcceleration1 = _player._currentPowerState.GetAirAcceleration1();
        _airAcceleration2 = _player._currentPowerState.GetAirAcceleration2();
        _losingMomentumGroundVelocityThreshold = _player._currentPowerState.GetLosingMomentumGroundVelocityThreshold();
        _xMaxAirSpeed = _player._currentPowerState.GetXMaxAirSpeed();
        _xMaxAirSpeedLongJump = _player._currentPowerState.GetXMaxAirSpeedLongJump();
        _highJumpPropelVelocity = _player._currentPowerState.GetHighJumpPropelVelocity();
        _longJumpXPropelVelocity = _player._currentPowerState.GetLongJumpXPropelVelocity();
        _longJumpYPropelVelocity = _player._currentPowerState.GetLongJumpYPropelVelocity();
    }

    public override void MovementInput(float x, float y)
    {
        //a favor del momentum
        if (x * _initialXVelocity > 0)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) < _horizontalAirVelocityThreshold)//Si va lento, acelera menos
            {
                _rigidbody.AddForce(Vector2.right * x * _airAcceleration0);
            }
            else //Si va rápido, acelera más
            {
                _rigidbody.AddForce(Vector2.right * x * _airAcceleration1);
            }
        }
        else if (x * _initialXVelocity < 0 || _initialXVelocity == 0)//en contra del momentum, sin pulsar nada sigue el arco
        {
            if (Mathf.Abs(_rigidbody.velocity.x) >= _horizontalAirVelocityThreshold)//Si va rápuido pierde momentum también rápido
            {
                _rigidbody.AddForce(Vector2.right * x * _airAcceleration1);
            }
            else if (Mathf.Abs(_initialXVelocity) >= _losingMomentumGroundVelocityThreshold)//Si ha empezado el salto yendo a mucha velocidad, puede perder momentum más rápido
            {
                _rigidbody.AddForce(Vector2.right * x * _airAcceleration2);
            }
            else
            {
                _rigidbody.AddForce(Vector2.right * x * _airAcceleration0);
            }
        }

        //Restringir máximo
        if (!_player._isLongJump)//velocidad máxima para salto normal o caída
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > _xMaxAirSpeed)
            {
                _rigidbody.velocity = new Vector2((_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x)) * _xMaxAirSpeed, _rigidbody.velocity.y);
            }
        }
        else //velocidad máxima para salto largo
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > _xMaxAirSpeedLongJump)
            {
                _rigidbody.velocity = new Vector2((_rigidbody.velocity.x / Mathf.Abs(_rigidbody.velocity.x)) * _xMaxAirSpeedLongJump, _rigidbody.velocity.y);
            }
        }

        base.MovementInput(x, y);
    }

    public override void OnJumpHighButton()
    {
        base.OnJumpHighButton();
    }

    public override void OnJumpLongButton()
    {
        base.OnJumpLongButton();
    }
}
