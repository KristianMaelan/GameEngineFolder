using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneControls : MonoBehaviour
{
    //Controller vars
    Gamepad controller;

    private Rigidbody _rb;

    private float throttleLevel = 0;
    private Vector3 startPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Drone controls calling override");
        controller = DS4.getController();
        _rb = GetComponent<Rigidbody>();
        startPosition = _rb.transform.position;
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
        //Leftstick
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
        if (controller.leftStick.y.ReadValue() > 0.15)
        {
            //_rb.AddForce(_rb.transform.forward * controller.leftStick.y.ReadValue() / 2, ForceMode.VelocityChange);

            if (throttleLevel < controller.leftStick.y.ReadValue())
            {
                throttleLevel = controller.leftStick.y.ReadValue();
            }
        }

        if (controller.leftStick.y.ReadValue() < -0.15)
        {
            if (throttleLevel > controller.leftStick.y.ReadValue())
            {
                throttleLevel = controller.leftStick.y.ReadValue();
            }
        }
        _rb.AddForce(_rb.transform.up * throttleLevel / 2, ForceMode.VelocityChange);
        
    }

    void buttonControls()
    {
        if (controller.startButton.wasPressedThisFrame)
        {
            throttleLevel = 0;
            Debug.Log("Respawning drone");
            _rb.transform.position = startPosition;
        }
        
    }
}
