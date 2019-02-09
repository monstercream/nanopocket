using UnityEngine;
using System.Collections;

public class CubeRotate : MonoBehaviour
{

    public Camera m_objUICamera = null;

    public float rotateRate;
    public Texture2D Thumb;
    private Vector2 PrevPoint;
    private string TouchStatus;
    // Use this for initialization
    void Start()
    {
        TouchStatus = "Idle";
    }

    // Update is called once per frame
    void Update()
    {
        if (InputHelper.IsPressed)
        {
            Ray uiray = m_objUICamera.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hituiobj;

            if (Physics.Raycast(uiray, out hituiobj, Mathf.Infinity))
            {
                TouchStatus = "Touch Began";
                PrevPoint = hituiobj.point;
            }
        }

        if (InputHelper.IsPress)
        {
            Ray uiray = m_objUICamera.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hituiobj;

            if (Physics.Raycast(uiray, out hituiobj, Mathf.Infinity))
            {
                Vector2 CurPrevPoint = hituiobj.point * rotateRate;

                TouchStatus = "Touch Moved";
                Debug.Log(PrevPoint + " -> " + hituiobj.point);

                PrevPoint = PrevPoint * rotateRate;

                gameObject.transform.Rotate(CurPrevPoint.y - PrevPoint.y
                    , -(CurPrevPoint.x - PrevPoint.x)
                    , 0);

                PrevPoint = hituiobj.point;
            }
        }

        /*{

         if (Input.touchCount == 1)
         {
             if (Input.GetTouch(0).phase == TouchPhase.Began)
             {
                 TouchStatus = "Touch Began";
                 PrevPoint = Input.GetTouch(0).position;
             }
 
             if (Input.GetTouch(0).phase == TouchPhase.Moved)
             {
                 TouchStatus = "Touch Moved";
                 Debug.Log(PrevPoint + " -> " + Input.GetTouch(0).position);
 
                 gameObject.transform.Rotate(Input.GetTouch(0).position.y - PrevPoint.y
                     , - (Input.GetTouch(0).position.x - PrevPoint.x)
                     , 0);
 
                 PrevPoint = Input.GetTouch(0).position;
             }
         }
         else
         {
             gameObject.transform.Translate(0, 0, 0);
             TouchStatus = "Idle";
         }
         * */
    }
    void OnGUI()
    {
        Rect pos = new Rect(0, 0, 100, 100);
        GUI.Label(pos, "Touch Status : " + TouchStatus);

        Rect touchPos = new Rect(PrevPoint.x - 5, Screen.height - (PrevPoint.y - 5), 10, 10);
        //GUI.DrawTexture(touchPos, Thumb);
    }
}