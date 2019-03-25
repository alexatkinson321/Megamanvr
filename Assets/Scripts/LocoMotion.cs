using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


//Locomotion movement: Move controllers up and down to transverse level
//How to use:
//Add locomotion script component to camera rig
//(Might need to) Create empty gameobject on scene and attach component Locomotion
//Drag CameraRig to Camera Rig in Loco Script
//Drag Camera(eye) to headset
//Drag controller(left) and (right) to Left and Right Loco Script

public class LocoMotion : MonoBehaviour
{

    //Control over full headset
    public GameObject headset; //not tracked obj
    public GameObject CameraRig;
    public SteamVR_TrackedObject Left; //Left controller
    public SteamVR_TrackedObject Right; //Right controller

    private Vector3 CameraRigloc;  //location of camerarig
    private Vector3 prevLeft; //previous position of left controller
    private Vector3 prevRight; //previous position of right controller


    // Start is called before the first frame update
    void Start()
    {
        SetPrevState();
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity where its moving
        Vector3 rightVal = Right.transform.position - prevRight;
        Vector3 leftVal = Right.transform.position - prevRight;

        float totalVal =+ rightVal.magnitude*0.8f + leftVal.magnitude*0.8f;
        //Getting input when key pressed in SteamVR (Inputs from both controllers)
        var device = SteamVR_Controller.Input((int)Left.index);
        var device2 = SteamVR_Controller.Input((int)Right.index);
        //CameraRig.transform.GetComponent;
        //if total value is >= 
        if(totalVal >= 0.05)
        {
            //Uses Grip button - 8 on controller diagram
            //When either or grip buttons used
            if(device.GetTouch(SteamVR_Controller.ButtonMask.Grip) || device2.GetTouch(SteamVR_Controller.ButtonMask.Grip))
             //if(device.GetTouch(SteamVR_Controller.ButtonMask.Grip) && device2.GetTouch(SteamVR_Controller.ButtonMask.Grip))
            //Move in direction of headset facing
            //Alter value for speed transition
            CameraRig.transform.position += headset.transform.forward / 8;
        }

        //at end of every update setprev state resetting old state
        SetPrevState(); 
        //Debug
        //Debug.Log(totalVal);
    }

    //Prev positions of controllers
    void SetPrevState()
    {
        //Get Current positions - call at end of frame
        prevLeft = Left.transform.position;
        prevRight = Right.transform.position;
    }
}
