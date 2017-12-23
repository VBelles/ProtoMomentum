using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionState {

    protected Vector2 movementInput = new Vector2();
    protected Vector2 lastMovementInput = new Vector2();

    protected ActionState lastState;
    protected ActionState nextState;
    protected PlayerModel player;
    protected Rigidbody rigidbody;
    protected Transform transform;

    public ActionState(PlayerModel player) {
        this.player = player;
        rigidbody = player.GetComponent<Rigidbody>();
        transform = player.GetComponent<Transform>();
    }

    public abstract void Tick();

    public virtual void OnStateEnter(ActionState lastState) { this.lastState = lastState; }

    public virtual void OnStateExit(ActionState nextState) { this.nextState = nextState; }

    public virtual void RefreshPowerState() { }

    public virtual void SetMovementInput(Vector2 movementInput) {
        lastMovementInput = this.movementInput;
        this.movementInput = movementInput;
    }

    public virtual void OnJumpHighButton() { }

    public virtual void OnJumpLongButton() { }
}
