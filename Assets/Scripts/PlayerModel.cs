using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour {

    public bool jumpButtonPressed = false;
    public Vector2 lastMovementInput = new Vector2();
    public GroundSensor groundSensor;

    private new Rigidbody rigidbody;

    //Tan sucio como hermoso
    public ActionState actionState { get; private set; }
    public enum ActionStates { 
        Grounded, Airborne, Idle, Landing, Walk, Run, JumpSquat,
        AirborneNormal, TurnAround, JumpSquatLong, AirborneLongJump 
    };
    private Dictionary<ActionStates, ActionState> actionStates;

    public PowerState powerState { get; private set; }
    public enum PowerStates { Basic, Furiosito, Brutal };
    private Dictionary<PowerStates, PowerState> powerStates;

    private int energy = 0;//provisional

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        groundSensor = GetComponentInChildren<GroundSensor>();
    }

    void Start() {
        actionStates = new Dictionary<ActionStates, ActionState>(){
           {ActionStates.Grounded, new GroundedActionState(this)},
           {ActionStates.Airborne, new AirborneActionState(this)},
           {ActionStates.Idle, new IdleActionState(this)},
           {ActionStates.Landing, new LandingActionState(this)},
           {ActionStates.Walk, new WalkActionState(this)},
           {ActionStates.Run, new RunActionState(this)},
           {ActionStates.JumpSquat, new JumpSquatActionState(this)},
           {ActionStates.AirborneNormal, new AirborneNormalActionState(this)},
           {ActionStates.TurnAround, new TurnAroundActionState(this)},
           {ActionStates.JumpSquatLong, new JumpSquatLongActionState(this)},
           {ActionStates.AirborneLongJump, new AirborneLongJumpActionState(this)}
        };
        powerStates = new Dictionary<PowerStates, PowerState>(){
            {PowerStates.Basic, new Ssj1PowerState(this)},
            {PowerStates.Furiosito, new Ssj2PowerState(this)},
            {PowerStates.Brutal, new Ssj2PowerState(this)}
        };
        SetActionState(ActionStates.Idle);
        SetPowerState(PowerStates.Basic);


    }

    void Update() {
        actionState.Tick();
    }

    public void SetActionState(ActionStates state) {
        ActionState exitingState = actionState;
        actionState = actionStates[state];
        //Debug.Log("No longer on " + exitingState?.GetType().Name + " , changed to " + actionState?.GetType().Name);
        gameObject.name = "Jugador - " + actionState.GetType().Name;
        exitingState?.OnStateExit(actionState);
        actionState?.OnStateEnter(exitingState);
    }


    public void SetPowerState(PowerStates state) {
        PowerState exitingState = powerState;
        powerState = powerStates[state];
        exitingState?.OnStateExit(powerState);
        powerState?.OnStateEnter(exitingState);
    }

    public void SetMovementInput(Vector2 movementInput) {
        actionState.SetMovementInput(movementInput);
    }

    public void OnJumpHighButton() {
        actionState.OnJumpHighButton();
    }

    public void OnJumpLongButton() {
        actionState.OnJumpLongButton();
    }

    public void OnReleaseEnergyButton() {
        energy -= 1000;
    }

}
