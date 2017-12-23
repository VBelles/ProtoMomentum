using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private PlayerModel playerModel;
    void Start() {
        rb = GetComponent<Rigidbody>();
        playerModel = GetComponent<PlayerModel>();
    }

    void Update() {


        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.JoystickButton0)) {
            playerModel.jumpButtonPressed = true;
        } else {
            playerModel.jumpButtonPressed = false;
        }

        //Salto normal
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
            playerModel.OnJumpHighButton();
        }

        //Salto largo
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton2)) {
            playerModel.OnJumpLongButton();
        }

        //Release energy
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button4)) {
            playerModel.OnReleaseEnergyButton();
        }

        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        playerModel.SetMovementInput(movement);

    }

}
