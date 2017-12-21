using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour {

    public bool jumpButtonPressed = false;
    public bool isLongJump = false;

    private Rigidbody rigidbody;

    //Tan sucio como hermoso
    private ActionState currentActionState;
    public enum ActionStates { Grounded, Airbone };
    private Dictionary<ActionStates, ActionState> actionStates;

    private PowerState currentPowerState;
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
        powerStates = new Dictionary<PowerStates, PowerState>()
        {
            //{PowerStates.Basic, ¿?},
            //{PowerStates.Furiosito, ¿?},
            //{PowerStates.Brutal, ¿?}
        };
        SetActionState(ActionStates.Grounded);

    }

    void Update() {


    }

    public void SetActionState(ActionStates state) {
        if (currentActionState != null) {
            currentActionState.OnStateExit(currentActionState);
        }

        ActionState exitingState = currentActionState;
        currentActionState = actionStates[state];

        if (currentActionState != null) {
            currentActionState.OnStateEnter(exitingState);
        }
    }


    public void SetPowerState(PowerState state) {
        if (currentPowerState != null) {
            currentPowerState.OnStateExit(state);
        }

        PowerState exitingState = currentPowerState;
        currentPowerState = state;

        if (currentPowerState != null) {
            currentPowerState.OnStateEnter(exitingState);
        }
    }

    public void SetMovementInput(Vector2 movementInput) {
        currentActionState.SetMovementInput(movementInput);
    }

    public void OnJumpHighButton() {
        currentActionState.OnJumpHighButton();
    }

    public void OnJumpLongButton() {
        currentActionState.OnJumpLongButton();
    }

    public void OnReleaseEnergyButton() {
        energy -= 1000;
    }


}
