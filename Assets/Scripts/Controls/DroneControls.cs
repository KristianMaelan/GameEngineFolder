using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneControls : MonoBehaviour
{
    Gamepad controller;
    [SerializeField] private float RotationScale = 2000;

    private Transform m_transform;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Calling override");
        controller = DS4.getController();
        m_transform = this.transform;
    }

    private void Update()
    {

        if (controller.buttonWest.isPressed)
        {
            m_transform.rotation = Quaternion.identity;
        }
        m_transform.rotation *= DS4.GetRotation(RotationScale * Time.deltaTime);
    }
}
