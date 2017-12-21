using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour {

    public bool jumpButtonPressed = false;
    public bool isLongJump = false;

    private Rigidbody rigidbody;

    //Tan sucio como hermoso
    public ActionState actionState { get; private set; }
    public enum ActionStates { Grounded, Airbone };
    private Dictionary<ActionStates, ActionState> actionStates;

    public PowerState powerState { get; private set; }
    public enum PowerStates { Basic, Furiosito, Brutal };
    private Dictionary<PowerStates, PowerState> powerStates;

    private int energy = 0;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();

    }

    void Start() {
        actionStates = new Dictionary<ActionStates, ActionState>(){
           {ActionStates.Grounded, new GroundedActionState(this)},
           {ActionStates.Airbone, new AirborneActionState(this)}
        };
        powerStates = new Dictionary<PowerStates, PowerState>(){
            {PowerStates.Basic, new Ssj1PowerState(this)},
            {PowerStates.Furiosito, new Ssj2PowerState(this)},
            {PowerStates.Brutal, new Ssj2PowerState(this)}
        };
        SetActionState(ActionStates.Grounded);
        SetPowerState(PowerStates.Basic);


    }

    void Update() {


    }

    public void SetActionState(ActionStates state) {
        ActionState exitingState = actionState;
        actionState = actionStates[state];
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
