using UnityEngine;
using System.Collections;

public class StagesceneManager : MonoBehaviour {

    public Camera m_objMainCamera = null;
    public Camera m_objUICamera = null;
    public GameObject m_objStage = null;

    RaycastHit hit;

    float rotationRate = 1.0f;

    private bool wasRotating;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount > 0)
        {
            //    If there are touches...

            Touch theTouch = Input.GetTouch(0);        //    Cache Touch (0)

            Ray ray = Camera.main.ScreenPointToRay(theTouch.position);
            Ray GUIRayq = m_objUICamera.ScreenPointToRay(theTouch.position);

            Debug.LogWarning("!!");

            if (Physics.Raycast(ray, out hit))
            {

                Debug.LogWarning("!!");
                if (Input.touchCount == 1)
                {
                    Debug.LogWarning("!!");

                    if (theTouch.phase == TouchPhase.Began)
                    {
                        Debug.LogWarning("!!");
                        wasRotating = false;
                    }

                    if (theTouch.phase == TouchPhase.Moved)
                    {

                        Debug.LogWarning("!!");
                        m_objStage.transform.Rotate(0, theTouch.deltaPosition.x * rotationRate, 0, Space.World);
                        wasRotating = true;
                    }

                }

            }
        }
        /*
        if (InputHelper.IsPress)
        {
            Debug.LogWarning("!!");
            Ray ray = m_objMainCamera.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitobj;

            if (Physics.Raycast(ray, out hitobj, Mathf.Infinity))
            {
                Vector3 direction = (hitobj.point - m_objStage.transform.position).normalized;

                m_objStage.transform.localRotation = Quaternion.Euler(direction);
            }
        }
         */ 
	}


}
