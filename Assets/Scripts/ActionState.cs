using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionState{

    protected float _lastX = 0, _lastY = 0;

    protected ActionState _lastState;
    protected ActionState _nextState;
    protected PlayerModel _player;
    protected Rigidbody2D _rigidbody;
    protected Transform _transform;

    public ActionState(PlayerModel player)
    {
        _player = player;
        _rigidbody = _player.GetComponent<Rigidbody2D>();
        _transform = _player.GetComponent<Transform>();
    }

    public abstract void Tick();

    public virtual void OnStateEnter(ActionState lastState) { _lastState = lastState; }

    public virtual void OnStateExit(ActionState nextState) { _nextState = nextState; }

    public virtual void RefreshPowerState() { }

    public virtual void MovementInput(float x, float y)
    {
        _lastX = x;
        _lastY = y;
    }

    public virtual void OnJumpHighButton() { }

    public virtual void OnJumpLongButton() { }  
}