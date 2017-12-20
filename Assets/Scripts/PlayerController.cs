using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;
    private PlayerModel playerModel;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerModel = GetComponent<PlayerModel>();
    }

    void Update()
    {
        //Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.velocity = movement * speed + new Vector3(0, rb.velocity.y, 0);

        //Jump
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }float x = 0, y = 0;//float por si hubiera mando o para normalizar

        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.JoystickButton0))
        {
            playerModel._jumpButtonPressed = true;
        }
        else
        {
            playerModel._jumpButtonPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.JoystickButton0))//Salto normal
        {
            playerModel.OnJumpHighButton();
        }

        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton2))//Salto largo
        {
            playerModel.OnJumpLongButton();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            y = -1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            y = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 1;
        }

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        playerModel.DirectionBindings(x, y);

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            playerModel.OnReleaseEnergyButton();
        }
    }

}
