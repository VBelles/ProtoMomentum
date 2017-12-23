using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerState {
    //Ground Movement
    public virtual float groundAcceleration {get; protected set;}
    public virtual float groundReleaseDeceleration {get; protected set;}
    public virtual float groundSkiddingDeceleration {get; protected set;}

    //Air movement
    public virtual float jumpSpeed  {get; protected set;}//provisional
    public virtual float gravityFallMultiplier {get; protected set;}

    //Max values
    public virtual float maxGroundSpeed {get; protected set;}
    public virtual float yMaxAirSpeed {get; protected set;}
    
    

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
