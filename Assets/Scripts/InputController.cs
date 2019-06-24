using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class InputController : MonoBehaviour
{
    private static InputController instance;
    public static InputController Instance
    {
        get
        {
            return instance;
        }
    }

    private float start;
    private float end;
    public float minSecondsForDrag;

    private static bool pressed, pressing, released, isTap;
    public static bool Pressed
    {
        get
        {
            return pressed;
        }
    }
    public static bool Pressing
    {
        get
        {
            return pressing;
        }
    }
    public static bool Released
    {
        get
        {
            return released;
        }
    }
    public static bool IsTap
    {
        get
        {
            return isTap;
        }
    }

    private static Vector3 worldPosition;
    public static Vector3 WorldPosition
    {
        get
        {
            return worldPosition;
        }
    }

    public static void ForceStopDrag()
    {
        pressed = false;
        pressing = false;
        released = true;
    }

    private void Awake()
    {
        //init Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of InputController");
        }
    }


    // Update is called once per frame
    void Update()
    {


        bool isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();

        foreach(Touch touch in Input.touches) {
            if(touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
                isPointerOverGameObject = true;
            }
        }

        pressed = Input.GetMouseButtonDown(0) && !isPointerOverGameObject;
        pressing = Input.GetMouseButton(0) && !isPointerOverGameObject;
        released = Input.GetMouseButtonUp(0) && !isPointerOverGameObject;
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pressed)
        {
            start = Time.time;
            isTap = false;
        }
        if (released)
        {
            end = Time.time;
            if (end - start < minSecondsForDrag)
            {
                isTap = true;
            }
        }
    }
}
