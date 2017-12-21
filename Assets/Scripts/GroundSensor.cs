using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour {

    private PlayerModel player;
    private int groundColliders = 0;

    void Awake() {
        player = GetComponent<PlayerModel>();
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Ground")) {
            Debug.Log("Entering new ground");
            if (groundColliders == 0) {
                player.SetActionState(PlayerModel.ActionStates.Grounded);
            }
            groundColliders++;
            //Debug.Log("Number of grounds colliding: " + _groundColliders);
        }

    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.CompareTag("Ground")) {
            //Debug.Log("Leaving one ground");
            groundColliders--;
            if (groundColliders == 0) {
                player.SetActionState(PlayerModel.ActionStates.Airbone);
                Debug.Log("Airborne");
            }
            //Debug.Log("Number of grounds colliding: " + _groundColliders);
        }
    }

}
