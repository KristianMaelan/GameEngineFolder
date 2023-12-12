using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = System.Numerics.Vector3;

public class CameraControls : MonoBehaviour
{
    
    Gamepad controller;
    private Camera underBodyCam;
    
    [SerializeField] private float gyroSensitivity = 100;
    [SerializeField] private float originalFocalLength = 20;
    
    private float originalGyroSens;
    //private bool _BorignalRotScale = true;
    
    private Transform underBody_transform;
    private Transform originalTransform;
    private Rigidbody _rbDrone;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Camera controller calling override");
        controller = DS4.getController();
        
        //underBodyCam = GetComponent<Camera>();
        originalGyroSens = gyroSensitivity;
        underBody_transform = this.transform;
        originalTransform = underBody_transform;
        underBodyCam = GetComponent<Camera>();

        _rbDrone = GetComponentInParent<Rigidbody>();


        //Drone = GetComponentInParent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            //COMPASS
            if (controller.buttonWest.wasPressedThisFrame)
            {
                Debug.Log("Resetting camera rotation");

                underBodyCam.transform.rotation = transform.parent.rotation;
                //underBodyCam.transform.up = _rbDrone.transform.forward;

                //underBody_transform.forward = _rbDrone.transform.forward;

                //underBodyCam.transform.rotation = underBody_transform.rotation;
            }
            else if (controller.buttonNorth.isPressed)
            {
                gyroSensitivity = originalGyroSens;
                underBodyCam.focalLength = originalFocalLength;
               
                Debug.Log("Camera settings reset.");
                return;
            }
            
            //D PAD
            if (controller.dpad.up.wasPressedThisFrame)
            {
                gyroSensitivity = gyroSensitivity * 2;
                    
                Debug.Log("Gyro sensitivity doubled, is now: " + gyroSensitivity);
            }
            else if (controller.dpad.down.wasPressedThisFrame)
            {
                gyroSensitivity = gyroSensitivity / 2;
                    
                Debug.Log("Gyro sensitivity halved, is now: " + gyroSensitivity);
                
                return;
            }
            
            //TRIGGERS AND BUMPERS
            if (controller.rightShoulder.isPressed && underBodyCam.focalLength < 120)
            {
                Debug.Log("Increasing Focal length");
                underBodyCam.focalLength += 1;
            }
            else if (controller.leftShoulder.isPressed && underBodyCam.focalLength > 15)
            {
                Debug.Log("Decreasing Focal length");
                underBodyCam.focalLength -= 1;

            }
        }
        underBody_transform.rotation *= DS4.GetRotation(gyroSensitivity * Time.deltaTime);
    }
    
    }

