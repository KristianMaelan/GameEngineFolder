using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneControls : MonoBehaviour
{
    Gamepad controller;
    [SerializeField] private float RotationScale = 2000;
    private float originalRotScale;
    private bool _BorignalRotScale = true;

    private Transform m_transform;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Calling override");
        controller = DS4.getController();
        m_transform = this.transform;

        //Setting a fallback rotation scale.
        originalRotScale = RotationScale;
    }

    private void Update()
    {
    //Controls handling
    {
        if (controller.buttonWest.isPressed)
        {
            m_transform.rotation = Quaternion.identity;
        }
        else if (controller.buttonSouth.wasPressedThisFrame)
        {
            if (_BorignalRotScale)
            {
                RotationScale *= 0.10f;
                _BorignalRotScale = false;q
            }
            else
            {
                RotationScale = originalRotScale;
                _BorignalRotScale = true;
            }
        }
        else if (controller.buttonNorth.isPressed)
        {
            return;
        }
    }
    
    
    
    m_transform.rotation *= DS4.GetRotation(RotationScale * Time.deltaTime);
    }
}
