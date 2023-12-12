using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DroneControls : MonoBehaviour
{
    //Controller vars
    Gamepad controller;

    private Rigidbody _rb;

    private float throttleLevel = 0;
    private Vector3 startPosition;
    private Vector3 startRotation;
    [SerializeField] private float throttleMultiplier = 5; 

    private float tiltAmount = 1;
    private float tiltSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Drone controls calling override");
        controller = DS4.getController();
        _rb = GetComponent<Rigidbody>();
       // startPosition = this.transform;
       startPosition = new Vector3(15, 7, 120);
       startRotation = _rb.transform.rotation.eulerAngles;
    }

    private void Awake()
    {
       // startPosition = this.transform;
        //Debug.Log("Startposition: " + startPosition.position);
    }

    private void Update()
    {
        buttonControls();
    }

    //Controls are physics based, therefore implemented in FixedUpdate.
    private void FixedUpdate()
    {
        stickControls();

    }

    void stickControls()
    {
        

        float yawRotation = 0;
        //Leftstick
        {
            //Yaw left
            if (controller.leftStick.x.ReadValue() < -0.1)
            {
                Debug.Log("yaw left");

                //Current rotation
                Vector3 currentRotation = transform.rotation.eulerAngles;
                //Adding new rotation
                float newRotation = currentRotation.y + controller.leftStick.x.ReadValue();
                //Creating input rotation
                Quaternion newQuaternion = Quaternion.Euler(currentRotation.x, newRotation, currentRotation.z);
                //Rotating
                transform.rotation = newQuaternion;
            }

            //Yaw right
            if (controller.leftStick.x.ReadValue() > 0.1)
            {
                Debug.Log("yaw right.");

                //Current rotation
                Vector3 currentRotation = transform.rotation.eulerAngles;
                //Adding new rotation
                float newRotation = currentRotation.y + controller.leftStick.x.ReadValue();
                //Creating input rotation
                Quaternion newQuaternion = Quaternion.Euler(currentRotation.x, newRotation, currentRotation.z);
                //Rotating
                transform.rotation = newQuaternion;
            }

            //throttle up
            if (controller.leftStick.y.ReadValue() > 0.05)
            {
                //_rb.AddForce(_rb.transform.forward * controller.leftStick.y.ReadValue() / 2, ForceMode.VelocityChange);

                if (throttleLevel < controller.leftStick.y.ReadValue())
                {
                    throttleLevel = controller.leftStick.y.ReadValue();
                }
            }

            if (controller.leftStick.y.ReadValue() < -0.05)
            {
                if (throttleLevel > controller.leftStick.y.ReadValue() && throttleLevel > 0)
                {
                    throttleLevel += controller.leftStick.y.ReadValue();
                }
            }
        }
        
        //RIGHTSTICK
        {
            
            float HorizontalTilt;
            float VerticalTilt;
            //
            if ((controller.rightStick.y.ReadValue() != 0 || controller.rightStick.x.ReadValue() != 0))
            {
                
                float stickValueY = controller.rightStick.y.ReadValue();
                HorizontalTilt = Mathf.Lerp(45, -45, (stickValueY + 1) / 2);


                 transform.localRotation = Quaternion.Euler(HorizontalTilt, 0, 0);
                //transform.rotation.eulerAngles.Set(transform.rotation.x, controller.rightStick.y.ReadValue() * 5, transform.rotation.z);;
                // _rb.transform.Rotate(_rb.transform.rotation.eulerAngles, controller.rightStick.y.ReadValue() * 5);
            
                float stickValueX = controller.rightStick.x.ReadValue();
                VerticalTilt = Mathf.Lerp(45, -45, (stickValueX + 1) / 2);


                transform.localRotation = Quaternion.Euler(HorizontalTilt, 0, VerticalTilt);
                //transform.localRotation = Quaternion.Euler(HorizontalTilt, 0, 0);
            }
        }
        
        //throttleLevel = Mathf.Clamp(throttleLevel, 0f, 100f);
        _rb.AddForce(_rb.transform.up * (throttleLevel / throttleMultiplier), ForceMode.Impulse);


    }

    void buttonControls()
    {
        //Debug.Log("Reading buttons");
        if (controller.buttonSouth.wasPressedThisFrame)
        {
            respawnDrone();
        }
        
    }

    void respawnDrone()
    {
        Debug.Log("Restarting world " + startPosition);
        SceneManager.LoadScene("Forest");
        
        
    }
    
}
