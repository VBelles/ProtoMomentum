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


        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton0)) {
            playerModel.jumpButtonPressed = false;
        }

        //Salto normal
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
            playerModel.OnJumpHighButton();
            playerModel.jumpButtonPressed = true;
        }

        //Salto largo
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton2)) {
            playerModel.OnJumpLongButton();
        }

        //Release energy
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button4)) {
            playerModel.OnReleaseEnergyButton();
        }

        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //movement.Normalize();//Si fem normalize aquí no podem fer que camini
        playerModel.SetMovementInput(movement);

    }

}
