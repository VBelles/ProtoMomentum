using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneNormalActionState : AirborneActionState {

    public Vector3 movement = new Vector3();

    private Vector3 airDriftVelocity = new Vector3();
    private float sidewaysdMinAngle = 45;
    private float backwardsdMinAngle = 135;
    private float sidewaysMaxDotProduct;
    private float backwardsMaxDotProduct;

	public AirborneNormalActionState(PlayerModel player) : base(player) {
        sidewaysMaxDotProduct = Mathf.Cos((Mathf.PI/180)*sidewaysdMinAngle);
        backwardsMaxDotProduct = Mathf.Cos((Mathf.PI/180)*backwardsdMinAngle);
    }

    public override void Tick() {
		base.Tick();
    }

    public override void OnStateEnter(ActionState lastState) {
        base.OnStateEnter(lastState);
    }

    public override void OnStateExit(ActionState nextState) {
        base.OnStateExit(nextState);
    }

    public override void SetMovementInput(Vector2 movementInput){
        movementInput.Normalize();
        
        movement.Set(movementInput.x, 0, movementInput.y);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movement.Normalize();

        //factor a movement según el ángulo respecto a facing direction
        if(Vector3.Dot(player.transform.forward, movement) > sidewaysMaxDotProduct){       
            movement *= player.powerState.airAcceleration;//aceleración de frente
        }else if(Vector3.Dot(player.transform.forward, movement) > backwardsMaxDotProduct){
            movement *= player.powerState.airAcceleration * player.powerState.sidewaysAirDriftFactor;//aceleración de lado    
        }else{
            movement *= player.powerState.airAcceleration * player.powerState.backwardsAirDriftFactor;//aceleración para atrás    
        }
         
        rigidbody.AddForce(movement);

        movement.Set(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if(movement.magnitude > player.powerState.maxGroundSpeed){
            movement = Vector3.ClampMagnitude(movement, player.powerState.maxGroundSpeed);
            movement.Set(movement.x, rigidbody.velocity.y, movement.z);
            rigidbody.velocity = movement;
        }
        
    }
}
