using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Vr;

public class Hand : MonoBehaviour
{
    //Short Grab/Pickup C# script
    //picking up object
    public SteamVR_Action_Boolean m_GrabAction = null;
    //Steam VR pose
private SteamVr_Behaviour_Pose m_Pose = null;
    //Fixed joint
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractable = new List<Interactable>();
    
    private void Awake(){
        //Get components
        m_Pose = GetComponent<SteamVr_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        //Down
        if(m_GrabAction.GetStateDown(m_Pose.inputSource)){
            //testing
            //print(m_Pose.inputSource + " Trigger Down);
            Pickup();
        }

        //Up
        if(m_GrabAction.GetStateUp(m_Pose.inputSource)){
            //testing
            //print(m_Pose.inputSource + " Trigger Up);
            Drop();
        }

    }

    //Add Unity Funtions
    //On Trigger actions
    private void OnTriggerEnter(Collider other){
        //Tag check
        if(!other.gameObject.CompareTag("Interactable"))
        return;

        //Add to contactInteractable list
        m_ContactInteractable.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other){
        //Tag check
        if(!other.gameObject.CompareTag("Interactable"))
        return;

        //Remove from contactInteractable list
        m_ContactInteractable.Remove(other.gameObject.GetComponent<Interactable>());
    }

    //Pickup Function
    public void Pickup(){
        
    }

    //Drop Function
    public void Drop(){

    }

    private Interactable GetNearestInteractable(){
        return null;
    }
}
