﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour {

    private const float Y_ANGLE_MIN = -5f;
    private const float Y_ANGLE_MIN_MOVEMENT = 10f;
    private const float Y_ANGLE_MAX = 55f;
    private const float DEFAULT_Y = 30f;

    private Transform target;
    private Transform camTransform;
    private PlayerModel player;

    private Camera cam;
    
    private float maxDistance = 8f;//10f;
    private float minDistance = 2f;//4f;
    private float maxVerticalOffset = 1.5f;//1.8f;
    private float minVerticalOffset = 0.8f;//0.2f;
    private float deltaAngle;
    private float currentX = 0f;
    private float currentY = 20f;
    private float mouseSensitivityX = 1.5f;
    private float mouseSensitivityY = 0.8f;
    private float stickSensitivityX = 25f;
    private float stickSensitivityY = 10f;
    private float cameraYVelocity = 30f;//grados por segundo
    private float cameraXZVelocity = 35f;
    private int timeToChangeDirection = 90;

    private bool isPlayerControllingCamera  = false;
    private Vector3 verticalOffsetVector;
    private Vector3 distanceVector;
    private Coroutine moveCameraCoroutine;
    private bool isCoroutineRunning = false;
    private int rotateDirection = 1;
    private bool justChangedDirection = false;
    private bool canChangeDirection = true;

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
        currentX = currentX % 360;

        if(isPlayerControllingCamera && moveCameraCoroutine != null && isCoroutineRunning == true){
            StopCoroutine(moveCameraCoroutine);
            isCoroutineRunning = false;
        }

        if(player.actionState is IdleActionState){
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }else{
            if(currentY < Y_ANGLE_MIN_MOVEMENT){
                if(!isPlayerControllingCamera && !isCoroutineRunning){
                    moveCameraCoroutine = StartCoroutine(MoveTowardsAngleAtVelocity(DEFAULT_Y, cameraYVelocity));
                }
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
            }else{
                currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN_MOVEMENT, Y_ANGLE_MAX);
            }  
        }
     }

    void LateUpdate() {
        CalculateDistanceVector();
        CenterOnBack();
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

    IEnumerator MoveTowardsAngleAtVelocity(float targetAngle, float velocity){
        if(velocity !=0){
            isCoroutineRunning = true;
            if((currentY > targetAngle && velocity < 0) || currentY < targetAngle && velocity > 0){
                if(velocity < 0){
                    while(currentY > targetAngle){
                        currentY += velocity * Time.deltaTime;
                        yield return 0;
                    }
                }else{
                    while(currentY < targetAngle){
                        currentY += velocity * Time.deltaTime;
                        yield return 0;
                    }
                }
                
            }
        }
        isCoroutineRunning = false;
    }

    void CenterOnBack(){
        if(!isPlayerControllingCamera && !(player.actionState is IdleActionState)){
            float targetX = Mathf.Atan2(player.transform.forward.x,player.transform.forward.z) * 180 / Mathf.PI;//Arcotangente de lado opuesto partido por lado contiguo
            currentX = (currentX + 360) % 360;//sumo 360 para evitar los negativos
            targetX = (targetX + 360) % 360;
            float targetMinusCurrent = targetX - currentX;

            if(canChangeDirection){
                if(targetMinusCurrent > 180 || (targetMinusCurrent < 0 && targetMinusCurrent > -180)){
                    if(rotateDirection == 1){ justChangedDirection = true; }
                    rotateDirection = -1; 
                }else{
                    if(rotateDirection == -1){ justChangedDirection = true; }
                    rotateDirection = 1; 
                }
                if(justChangedDirection){
                    justChangedDirection = false;
                    canChangeDirection = false;
                    StartCoroutine(CounterToEnableChangeDirection());
                }
            }

            currentX += rotateDirection * cameraXZVelocity * Time.deltaTime;
            if(Mathf.Abs(currentX - targetX) < cameraXZVelocity/50){ currentX = targetX; }  
        }
    }

    IEnumerator CounterToEnableChangeDirection(){
        yield return new WaitForSeconds(timeToChangeDirection/60f);
        Debug.Log("Can Change direction");
        canChangeDirection = true;
    }
}