using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerState {
    //Ground Movement
    protected float groundSpeed = 5f;
    protected float airSpeed = 1f;
    protected float jumpSpeed = 5f;

    protected PowerState lastState;
    protected PowerState nextState;
    protected PlayerModel player;


    public PowerState(PlayerModel player) {
        this.player = player;
    }

    public abstract void Tick();

    public virtual void OnStateEnter(PowerState lastState) { this.lastState = lastState; }

    public virtual void OnStateExit(PowerState nextState) { this.nextState = nextState; }

    public virtual float GetGroundSpeed() { return groundSpeed; }
    public virtual float GetAirSpeed() { return airSpeed; }
    public virtual float GetJumpSpeed() { return jumpSpeed; }


}
