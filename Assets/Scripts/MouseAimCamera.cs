using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = 10f;
    private const float Y_ANGLE_MAX = 45f;

    private Transform target;
    private Transform camTransform;

    private Camera cam;
    
    private float distance = 8f;
    private float maxVerticalOffset = 1.8f;
    private float minVerticalOffset = 0.2f;
    private float deltaAngle;
    private float currentX = 0f;
    private float currentY = 20f;
    private float mouseSensitivityX = 2.5f;
    private float mouseSensitivityY = 1f;
    private float stickSensitivityX = 25f;
    private float stickSensitivityY = 10f;

    private Vector3 verticalOffsetVector;


    void Awake(){
		target = FindObjectOfType<PlayerModel>().transform;
        cam = GetComponent<Camera>();
	}

    void Start() {
        camTransform = transform;
        verticalOffsetVector = new Vector3();
        deltaAngle = Y_ANGLE_MAX - Y_ANGLE_MIN;
    }
     
     void Update(){
        if(Input.GetAxis("Right Stick X") == 0 && Input.GetAxis("Right Stick Y") == 0){
            currentX += Input.GetAxis("Mouse X") * mouseSensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * mouseSensitivityY;
        }else{
            currentX += Input.GetAxis("Right Stick X") * stickSensitivityX;
            currentY += Input.GetAxis("Right Stick Y") * stickSensitivityY;
        }

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
     }

    void LateUpdate() {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * dir;
        CalculateVerticalOffsetVector();
        Debug.Log(verticalOffsetVector);
        camTransform.LookAt(target.position + verticalOffsetVector);//vertical offset para que no esté en el centro exacto
    }

    void CalculateVerticalOffsetVector()
    {
        float currentOffset = ((deltaAngle - (currentY - Y_ANGLE_MIN))/deltaAngle) * (maxVerticalOffset - minVerticalOffset) + minVerticalOffset;
        verticalOffsetVector.Set(0, currentOffset, 0);
    }
}