using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = -5f;
    private const float Y_ANGLE_MIN_MOVEMENT = 10f;
    private const float Y_ANGLE_MAX = 45f;
    private const float DEFAULT_Y = 20f;

    private Transform target;
    private Transform camTransform;
    private PlayerModel player;

    private Camera cam;
    
    private float maxDistance = 10f;
    private float minDistance = 4f;
    private float maxVerticalOffset = 1.8f;
    private float minVerticalOffset = 0.2f;
    private float deltaAngle;
    private float currentX = 0f;
    private float currentY = 20f;
    private float mouseSensitivityX = 1.5f;
    private float mouseSensitivityY = 0.8f;
    private float stickSensitivityX = 25f;
    private float stickSensitivityY = 10f;
    private float cameraYVelocity = 30f;//grados por segundo

    private bool isPlayerControllingCamera  = false;
    private Vector3 verticalOffsetVector;
    private Vector3 distanceVector;


    void Awake(){
        player = FindObjectOfType<PlayerModel>();
		target = player.transform;
        cam = GetComponent<Camera>();
	}

    void Start() {
        camTransform = transform;
        verticalOffsetVector = new Vector3();
        distanceVector = new Vector3();
        deltaAngle = Y_ANGLE_MAX - Y_ANGLE_MIN;
    }
     
     void Update(){
        if(Input.GetAxis("Right Stick X") == 0 && Input.GetAxis("Right Stick Y") == 0){
            if(Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0){
                isPlayerControllingCamera = false; 
            }else{
                isPlayerControllingCamera = true;
            }
            currentX += Input.GetAxis("Mouse X") * mouseSensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * mouseSensitivityY;
        }else{
            currentX += Input.GetAxis("Right Stick X") * stickSensitivityX;
            currentY += Input.GetAxis("Right Stick Y") * stickSensitivityY;
            isPlayerControllingCamera = true;
        }

        if(player.actionState is IdleActionState){
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }else{
            if(currentY < DEFAULT_Y){
                if(!isPlayerControllingCamera){
                    currentY += cameraYVelocity * Time.deltaTime;
                }
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
            }else{
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN_MOVEMENT, Y_ANGLE_MAX);
            }  
        }
     }

    void LateUpdate() {
        CalculateDistanceVector();
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.position + rotation * distanceVector;
        CalculateVerticalOffsetVector();
        camTransform.LookAt(target.position + verticalOffsetVector);//vertical offset para que no esté en el centro exacto
    }

    void CalculateVerticalOffsetVector(){
        float currentOffset;
        
        currentOffset = ((deltaAngle - (currentY - Y_ANGLE_MIN))/deltaAngle) * (maxVerticalOffset - minVerticalOffset) + minVerticalOffset;
        
        verticalOffsetVector.Set(0, currentOffset, 0);
    }

    void CalculateDistanceVector(){
        float currentDistance = ((currentY - Y_ANGLE_MIN)/deltaAngle) * (maxDistance - minDistance) + minDistance;
        distanceVector.Set(0, 0, -currentDistance);
    }
}