using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class DS4 : MonoBehaviour
{
    
    //private float scale = 1;
    
    
    public static Gamepad controller = null;
    private static bool isControllerbound = false;

    private static ButtonControl X_Gyro = null;
    private static ButtonControl Y_Gyro = null;
    private static ButtonControl Z_Gyro = null;

    
    
    public static Gamepad getController()
    {
        if (!isControllerbound)
        {
            
        Debug.Log("Controller override step1");
        //Using system.IO to read the entire JSON file to replace the normal JSON file.
        string newCon = System.IO.File.ReadAllText("Assets/Scripts/Controls/DS4Custom.json");
        
        //Overriding the old layout
        InputSystem.RegisterLayoutOverride(newCon, "Newoverride");

        //replacing old controller with new controller with new layout
        var DualShock = Gamepad.current;
        DS4.controller = DualShock;

        Debug.Log("Controller override complete");
        bindController(DS4.controller);

        isControllerbound = true;
        return DualShock;
        }
        else
        {
            var DualShock = Gamepad.current;
            return DualShock;
        }
    }

    static float rawData(float inputData)
    {
        
        if (inputData > 0.5)
        {
            return 1 - inputData;
        }
        return -inputData;
    }

    private static void bindController(Gamepad controller)
    {
     X_Gyro = controller.GetChildControl<ButtonControl>("gyro X 14");
     Y_Gyro = controller.GetChildControl<ButtonControl>("gyro Y 16");
     Z_Gyro = controller.GetChildControl<ButtonControl>("gyro Z 18");   
        
    }

    public static Quaternion GetRotation(float scale = 1)
    {
        float x = rawData(X_Gyro.ReadValue()) * scale;
        float y = rawData(Y_Gyro.ReadValue()) * scale;
        float z = rawData(Z_Gyro.ReadValue()) * scale * -1; // Z Value from DS4 is opposite from Unity Z Axis


        return quaternion.Euler(x, y, z);
    }
}
