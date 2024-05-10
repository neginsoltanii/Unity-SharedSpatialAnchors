using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobeControllerScript : MonoBehaviour
{
    
    public DataManager dataManager;

    public float rotateSpeed = 10f; // degrees per second
    float step;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //// Check if there is a connected gamepad
        //if (Gamepad.current != null)
        //{
        //    Vector2 rotationInput = Gamepad.current.rightStick.ReadValue();
        //  //  Vector2 rotationInput = new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"));
        //
        //
        //    // Rotate around the Y axis
        //    transform.Rotate(0, rotationInput.x * rotateSpeed * Time.deltaTime, 0);
        //}


        // Documentation from: https://developer.oculus.com/documentation/unity/unity-tutorial-basic-controller-input/


        //
        //if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            //// Testing send data to Data UI Manager
            //DataFormatWorld data = new DataFormatWorld();
            //data.countryName = "Sweden";
            //data.co2emissions = 2300;

            // Read a value from the CSV file
            DataFormatWorld data = dataManager.GetTestValueFromYear();
            dataManager.SetDataToUI(data);
        }




        /// Rotate with the thumbsticks
        
        // Calculate a proportion in the rotation (degrees per second)
        step = rotateSpeed * Time.deltaTime;
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft))
        {
            transform.Rotate(0, step, 0);
            Debug.Log("Right Thumbstick detected - Left");
        }

        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight))
        {
            transform.Rotate(0, -step, 0);
            Debug.Log("Right Thumbstick detected - Right");
        }

    }
        
}
