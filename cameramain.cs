using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramain : MonoBehaviour
{
    public static Transform Target;
    private float mouseX;
    private float mouseY;
    public float sensitivityMouse = 2f;
    public float sensitivityScroll = 100f;
    public float distance = 20f;
    public float mindistance = 2f;
    public float maxdistance = 50f;
    private Quaternion cameraRotation;
    private Vector3 cameraPosition;
    private bool is360mode = false;
    private GameObject Plane;

    void Start()// Use this for initialization
    {
        Target = GameObject.Find("Body").GetComponent <Transform>();
        Plane = GameObject.Find("Plane");
        cameractrl(is360mode);
    }
    void OnGUI()//OnGUI is called for rendering and handling GUI events.
    {
        if (Input.GetMouseButton(0)&&UIbtn.isOnui ==false)
            cameractrl(is360mode);
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            cameradistance();
        Event doubleClick = Event.current;
        if (doubleClick.isMouse && (doubleClick.clickCount == 2))
            Focus();
    }
    private void cameractrl(bool mode)//攝影機環繞
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;
        if (!mode)
            mouseY = 20;
        cameraRotation = Quaternion.Euler(mouseY, mouseX, 0);
        cameraPosition = cameraRotation * new Vector3(0, 0, -distance) + Target.position;
        transform.rotation = cameraRotation;
        transform.position = cameraPosition;
    }
    private void cameradistance()//調整畫面的距離
    {
        distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityScroll * Time.deltaTime;
        distance = Mathf.Clamp(distance, mindistance, maxdistance);
        transform.position = cameraRotation * new Vector3(0, 0, -distance) + Target.position;
    }
    private void Focus()
    {
        Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(MouseRay, out hit))
            Target = hit.transform;
    }//攝影機目標
    private void btn360mode()
    {
        if (is360mode)
        {
            is360mode = false;
            Plane.SetActive(true);
            foreach (GameObject otherObj in UIbtn.objCtrl)
            {
                if(otherObj!=null)
                    otherObj.SetActive(true);
            }
            cameractrl(is360mode);
        }
        else
        {
            is360mode = true;
            Plane.SetActive(false);
            foreach (GameObject otherObj in UIbtn.objCtrl)
            {
                if (otherObj != null)
                    otherObj.SetActive(false);
            }
            Target.parent.gameObject.SetActive(true);
        }
    }//攝影機360度模式開關
}
