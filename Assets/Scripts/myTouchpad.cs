using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

 //Uses Touchpad to move
 //Add myTouchpad script to controller either left or right (left preferred)
 //Drag CameraRig (from samplescene) to player in the mytouchpad component script - to attach camerarig to player
 //(Might need) Rigidbody component for rotation and position

 public class myTouchpad : MonoBehaviour
 {
     //Player -CameraRig
     public GameObject player; //not tracked obj
     public SteamVR_Controller.Device device;
     public SteamVR_TrackedObject controller; //Controller
     public Vector2 touchpad;
 
     private float sensitivityX = 2.0F;
     //private float sensitivity = 3.5f
     private Vector3 playerPosition;
 
     void Start()
     {
         /*trackedobj = GetComponent<SteamVR_TrackedObject>();
         device = SteamVR_Controller.Input((int)trackedobj.index);*/
         controller = gameObject.GetComponent<SteamVR_TrackedObject>();
     }
 
     // Update is called once per frame
     void Update()
     {
         device = SteamVR_Controller.Input((int)controller.index);
         //If touchpad is pressed
         //Button 2 on Vive Controller
         if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
         {
             //Reads the Vive touchpad values
             touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
 
 
             //Handle movement via touchpad
             if (touchpad.y > 0.2f || touchpad.y < -0.2f) {
                 //Move Forward
                 player.transform.position -= player.transform.forward * Time.deltaTime * (touchpad.y * 5f);
 
                 //Adjust the height to terrain height based where player positin
                 playerPosition = player.transform.position;
                 playerPosition.y = Terrain.activeTerrain.SampleHeight(player.transform.position);
                 player.transform.position = playerPosition;
             }
 
             //Handles the rotation by touchpad
             if (touchpad.x > 0.25f || touchpad.x < -0.25f) {
                 player.transform.Rotate(0, touchpad.x * sensitivityX, 0);
             }

                //Debug testing x and y location log
             //Debug.Log ("Touchpad X = " + touchpad.x + " : Touchpad Y = " + touchpad.y);
         }
     }
 }