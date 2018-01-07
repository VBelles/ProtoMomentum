using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedActionState : ActionState {

    protected PowerState powerState;
    public Vector3 movement = new Vector3();
    protected Vector2 currentVelocityNormalized = new Vector2();
    protected Vector2 movementInputFromCameraPOV = new Vector2();
    protected bool isRunning = false;
    protected float maxWalkingVelocity = 4f;
    protected float walkingJoystickMaxTilt = 0.85f;
    protected float turnAroundMinAngle = 120;
    protected float turnAroundMaxDotProduct;

    public GroundedActionState(PlayerModel player) : base(player) {
        turnAroundMaxDotProduct = Mathf.Cos((Mathf.PI/180)*turnAroundMinAngle);
    }

    public override void Tick() {
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
        RefreshPowerState();
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

    public override void RefreshPowerState() {
        this.powerState = player.powerState;
    }     

    public override void SetMovementInput(Vector2 movementInput) {
        base.SetMovementInput(movementInput);
        if(movementInput.magnitude < walkingJoystickMaxTilt){//} && rigidbody.velocity.magnitude <= maxWalkingVelocity + 1){
            isRunning = false;
        }else{
            isRunning = true;
        }
  
        movementInput.Normalize();

        movement.Set(movementInput.x, 0, movementInput.y);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();
        
        movementInputFromCameraPOV.Set(movement.x, movement.z);
        currentVelocityNormalized.Set(rigidbody.velocity.x, rigidbody.velocity.z);
        currentVelocityNormalized.Normalize();
        //Debug.Log("last: " + currentVelocityNormalized + " , current: " + movementInputFromCameraPOV);

        if(Vector2.Dot(currentVelocityNormalized, movementInputFromCameraPOV) > turnAroundMaxDotProduct){//si la velocidad y el nuevo input (en referencia a la cámara) están a 120 grados o más -> turn around
            if(movementInput != Vector2.zero){
                float step = 6f * Time.deltaTime;
                player.transform.forward = Vector3.RotateTowards(player.transform.forward, movement, step, 0.0f);
                rigidbody.AddForce(player.transform.forward * powerState.groundAcceleration);//Girar hasta estar en la misma dirección que movement y add force siempre para adelante  
            }else{
                if(player.lastMovementInput != Vector2.zero){
                    rigidbody.velocity *= powerState.groundReleaseDeceleration;
                } 
            }
            if(isRunning)
            {
                if(Mathf.Abs(rigidbody.velocity.magnitude) > powerState.maxGroundSpeed){
                    //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, powerState.maxGroundSpeed);
                    rigidbody.velocity = player.transform.forward * powerState.maxGroundSpeed;
                }
            }else{
                if(Mathf.Abs(rigidbody.velocity.magnitude) > maxWalkingVelocity){
                    //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxWalkingVelocity);
                    rigidbody.velocity = player.transform.forward * maxWalkingVelocity;   
                }
            }
        }else{
            //Debug.Log("Turn around, velocity = " + rigidbody.velocity.magnitude);
            player.SetActionState(PlayerModel.ActionStates.TurnAround);
        }
    }

    public override void OnJumpHighButton() {
        base.OnJumpHighButton();
        player.SetActionState(PlayerModel.ActionStates.JumpSquat);
    }

    public override void OnJumpLongButton() {
        base.OnJumpLongButton();
        player.SetActionState(PlayerModel.ActionStates.JumpSquatLong);
    }

    public virtual void OnLeavingGround() {
        player.SetActionState(PlayerModel.ActionStates.AirborneNormal);
     }
}
