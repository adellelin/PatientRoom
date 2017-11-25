using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.UI;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager instance { get; private set; }

    // hologram being gazed at
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    public GameObject chartingRhythm;
    public GameObject chartingMedication;
    public GameObject chartingIntervention;

    //private GameObject[] toniDeskObject;
    List<GameObject> chartingObjects = new List<GameObject>();

    public GameObject[] chartingObjectContent;

    // Use this for initialization
    void Start () {

        instance = this;

        chartingObjects.Add(chartingRhythm);
        chartingObjects.Add(chartingIntervention);
        chartingObjects.Add(chartingMedication);

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

            for (int i = 0; i < chartingObjects.Count; i++)
            {
                // Debug.Log(FocusedObject);
                // Debug.Log("desk object names: " + toniDeskObjects[i]);

                if (FocusedObject == chartingObjects[i])
                {
                    chartingObjectContent[i].SetActive(true);
                    //toniBio.transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);

                    //Debug.Log("found desk object");
                }
                else
                {
                    chartingObjectContent[i].SetActive(false);
                    //Debug.Log("can't find it");
                }
            }

            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }

    }
}
