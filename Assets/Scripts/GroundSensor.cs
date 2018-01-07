using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour {

    private PlayerModel player;
    private int groundColliders = 0;

    void Awake() {
        player = GetComponentInParent<PlayerModel>();
    }

    void Update() {
        if(groundColliders < 0){
            Debug.LogWarning("Menos de 0 ground colliders, lo mínimo debería ser 0",this);
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Ground")) {
            //Debug.Log("Entering new ground");
            if (groundColliders == 0) {
                player.SetActionState(PlayerModel.ActionStates.Landing);
            }
            groundColliders++;
            //Debug.Log("Number of grounds colliding: " + groundColliders);
        }

    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.CompareTag("Ground")) {
            //Debug.Log("Leaving one ground");
            groundColliders--;
            if (groundColliders == 0) {
                if(player.actionState is GroundedActionState){
                    (player.actionState as GroundedActionState).OnLeavingGround();  
                }
            }
            //Debug.Log("Number of grounds colliding: " + groundColliders);
        }
    }

    public bool IsTouchingGround(){
        if(groundColliders > 0) return true;
        else return false;
    }

}
