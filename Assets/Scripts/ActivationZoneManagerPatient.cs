using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationZoneManagerPatient : MonoBehaviour
{
    public GameObject bedActivationZonePosition;       //Public variable to store a reference to the painting game object
    public Light bedActivationZoneLightObject;
    public Light bedZoneLight2;

    /*
    public GameObject deskActivationZonePosition;       //Public variable to store a reference to the desk game object
    public Light deskActivationZoneLightObject;

  
    private Vector3 deskActivationZoneOffset;         //Private variable to store the offset distance between the desk and camera

    //AudioClip paintingAudio;
    AudioSource bedAudio;
    public GameObject pendulum;

    public GameObject exitZonePosition;
    private float exitZoneOffset;
    public ExitZoneManagerTom exitManagerTom;
    */
    private Vector3 viewerTransform;                // check viewer starting pos
    private float bedActivationZoneOffset;         //Private variable to store the offset distance between the painting and camera
    private bool bedAudioTriggered = false;
    public float lightIntensity;
    public float lightIntensity2;

    // public Text PositionCamera;

    // Use this for initialization
    void Start()
    {
        lightIntensity = bedActivationZoneLightObject.GetComponent<Light>().intensity;
        lightIntensity2 = bedZoneLight2.GetComponent<Light>().intensity;
        bedActivationZoneLightObject.enabled = false;
        bedZoneLight2.enabled = false;
        //deskActivationZoneLightObject.enabled = false;
        //pendulum.SetActive(false);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        viewerTransform = transform.position;
        Debug.Log("viewerPosition: " + viewerTransform);
        //bedAudio = bedActivationZonePosition.GetComponent<AudioSource>();
        //bedActivationZoneLightObject = GetComponent<Light>();
      
        
        //Debug.Log("paintingaudio length" + bedAudio.clip.length);
    }

    // Update is called once per frame
    void Update()
    {

        lightIntensity = bedActivationZoneLightObject.GetComponent<Light>().intensity;
        lightIntensity2 = bedZoneLight2.GetComponent<Light>().intensity;

        //Dynamic evaluation of distance to painting activation zone
        //paintingActivationZoneOffset = transform.position - paintingActivationZonePosition.transform.position;
        //Debug.Log(paintingActivationZoneOffset);
        bedActivationZoneOffset = Vector3.Distance(transform.position, bedActivationZonePosition.transform.position);
        //Debug.Log(paintingActivationZoneOffset);
        if (bedActivationZoneOffset < 2f )
        {
            bedActivationZoneLightObject.enabled = true;
            bedZoneLight2.enabled = true;
            //Debug.Log("light intensity " + lightIntensity);
            bedActivationZoneLightObject.GetComponent<Light>().intensity = 5.0f / bedActivationZoneOffset;
            bedZoneLight2.GetComponent<Light>().intensity = 5.0f / bedActivationZoneOffset;
            //pendulum.SetActive(true);
            //pendulum.transform.Rotate(0, 0, 1f);
            /*
            if (!bedAudioTriggered)
            {
                playPaintingAudio();
                bedAudioTriggered = true;
            }*/
        }
        else
        {
            bedActivationZoneLightObject.enabled = false;
            bedZoneLight2.enabled = false;
            //bedAudio.Pause();
            //pendulum.SetActive(false);
            //bedAudioTriggered = false;
        }
        /*
        //Dynamic evaluation of distance to desk activation zone
        deskActivationZoneOffset = transform.position - deskActivationZonePosition.transform.position;

        if (Mathf.Abs(deskActivationZoneOffset.x) < 2f)
        {
            deskActivationZoneLightObject.enabled = true;
        }
        else
        {
            deskActivationZoneLightObject.enabled = false;
        }

        exitZoneOffset = Vector3.Distance(transform.position, exitZonePosition.transform.position);
        //Debug.Log(paintingActivationZoneOffset);
        if (exitZoneOffset < 2f)
        {

            exitManagerTom.exitTomScene();
        }
        else { }
        */
    }

    private void playPaintingAudio()
    {

      //  bedAudio.Play();
        //Invoke("SculptureAppear", paintingAudio.clip.length);
        //Invoke("SculptureAppear", 10);
    }
    /*
    void SculptureAppear()
    {
        sculptureActivationZonePosition.SetActive(true);
        //paintingAudio.Stop();
    }
    */
}
