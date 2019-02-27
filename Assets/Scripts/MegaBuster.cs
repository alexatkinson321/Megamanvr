using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBuster : MonoBehaviour
{
    private SteamVR_TrackedController controller;
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform busterEnd;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    public AudioClip busterSound;
    public AudioClip busterCharge;
    private AudioSource source;
    
    private LineRenderer busterLine;
    private float nextFire;
    public GameObject origin;
    
    void Start()
    {
        busterLine = GetComponent<LineRenderer>();
        //busterSound = GetComponent<AudioSource>();
        source = GetComponent<AudioSource>();    
    }
    
    private void OnEnable(){
        controller = GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += HandleTriggerClicked;

    }

    private void OnDisable(){

        controller.TriggerClicked -= HandleTriggerClicked;


    }


    #region fire
    private void HandleTriggerClicked(object sender, ClickedEventArgs e){
        Fire();

    }

    private void Fire(){
        if (Time.time > nextFire){
            nextFire = Time.time + fireRate;
            StartCoroutine(shotEffect());
            Vector3 rayOrigin = origin.transform.position;
            RaycastHit hit;
            busterLine.SetPosition(0, busterEnd.position);
            if (Physics.Raycast(rayOrigin, origin.transform.forward, out hit, weaponRange)){
                busterLine.SetPosition(1 , hit.point);
            }
        else {
                busterLine.SetPosition(1 , rayOrigin + (origin.transform.forward * weaponRange));
            }
        }
    }
    #endregion

    // void Update()
    // {
    //     if (controller.triggerPressed && Time.time > nextFire){
    //         nextFire = Time.time + fireRate;
    //         StartCoroutine(shotEffect());
    //         Vector3 rayOrigin = origin.transform.position;
    //         RaycastHit hit;
    //         busterLine.SetPosition(0, busterEnd.position);
    //         if (Physics.Raycast(rayOrigin, origin.transform.forward, out hit, weaponRange)){

    //                 busterLine.SetPosition(1 , hit.point);
    //         }
    //         else {
    //                 busterLine.SetPosition(1 , rayOrigin + (origin.transform.forward * weaponRange));

    //         }
    //     }
    // } 

    private IEnumerator shotEffect(){
        source.PlayOneShot(busterSound, 1f);
        busterLine.enabled = true;
        yield return shotDuration;
        busterLine.enabled = false;
    }
}
