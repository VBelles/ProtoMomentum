using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerState {
    //Ground Movement
    public virtual float groundSpeed {get; protected set;}
    public virtual float groundAcceleration {get; protected set;}
    public virtual float maxGroundSpeed {get; protected set;}
    
    public virtual float airSpeed {get; protected set;}
    public virtual float jumpSpeed  {get; protected set;}

    protected PowerState lastState;
    protected PowerState nextState;
    protected PlayerModel player;

    public PowerState(PlayerModel player) {
        this.player = player;
    }

    public abstract void Tick();

    public virtual void OnStateEnter(PowerState lastState) { this.lastState = lastState; }

    public virtual void OnStateExit(PowerState nextState) { this.nextState = nextState; }

}
