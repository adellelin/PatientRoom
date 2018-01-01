using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;
using UnityEngine.Video;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager instance { get; private set; }

    // hologram being gazed at
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    public GameObject chartingRhythm;
    public GameObject chartingMedication;
    public GameObject chartingIntervention;
    public GameObject chartingEraser;

    public VideoPlayer videoReadings;
    public GameObject videoReadParent;

    //private GameObject[] toniDeskObject;
    List<GameObject> chartingObjects = new List<GameObject>();

    public GameObject[] chartingObjectContent;

    // Use this for initialization
    void Start () {

        instance = this;

        chartingObjects.Add(chartingRhythm);
        chartingObjects.Add(chartingIntervention);
        chartingObjects.Add(chartingMedication);
        chartingObjects.Add(chartingEraser);
        Debug.Log(chartingObjects.Count);

        videoReadParent.SetActive(false);

        // set up a gesture recognizer to detect select gestures
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // send an onselect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject oldFocusedObject = FocusedObject;

        // do raycast into world based on user's head position
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
            //Debug.Log(FocusedObject);
            //Debug.Log(FocusedObject.GetComponent<Renderer>().material.shader);
            //FocusedObject.GetComponent<Renderer>().material.shader = Shader.Find("HoloToolkit/Wireframe");
        } else
        {
            // if the raycast did no hit holo, clear focused object
            FocusedObject = null;
        }

        // if the focused object changed, start detecting fresh gestures
        if (FocusedObject != oldFocusedObject)
        {
            Debug.Log("focused on " + FocusedObject);
            Debug.Log("focused on " + FocusedObject.GetInstanceID());
            // Debug.Log("count" + chartingObjects.Count);
            for (int i = 0; i < chartingObjects.Count; i++)
            {

                Debug.Log("charted"+chartingObjects[i].GetInstanceID());
                if (FocusedObject.GetInstanceID() == chartingObjects[i].GetInstanceID())
                {
                    Debug.Log(i + "  looking at");
                    if (i == 0)
                    {
                    
                        videoReadings.Play();
                        videoReadParent.SetActive(true);
                    }
                    if (i == 3)
                    {
                        Debug.Log("eraser", chartingObjects[2]);
                        videoReadings.Stop();
                        videoReadParent.SetActive(false);
                    }
                }
            }


            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }

    }
}
