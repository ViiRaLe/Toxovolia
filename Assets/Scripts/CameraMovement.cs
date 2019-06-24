using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float horizontalVelocity = 2f;
    public float verticalVelocity = 1.5f;
    public float horizontalDiff = 5.63f;
    public float verticalDiff = 10f;
    public delegate void CameraMove();
    public CameraMove moveCamera;
    private Vector3 currentPos;


    private void MoveHorizontal(int dir)
    {
        PopupQuiver.Instance.gameObject.SetActive(false);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, transform.position.x + horizontalDiff * dir, Time.deltaTime * horizontalVelocity), transform.position.y, transform.position.z);
    }

    public void Right()
    {
        MoveHorizontal(1);
    }

    public void Left()
    {
        MoveHorizontal(-1);
    }

    public void Up()
    {
        PopupQuiver.Instance.gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y + verticalDiff, Time.deltaTime * verticalVelocity), transform.position.z);
    }

    public void Wait()
    {
        currentPos = transform.position;
    }




    private void Awake()
    {
        currentPos = transform.position;
        moveCamera = Wait;

    }



    // Update is called once per frame
    void Update()
    {
        moveCamera();
        if (transform.position.x > currentPos.x + horizontalDiff)
        {
            transform.position = new Vector3(currentPos.x + horizontalDiff, transform.position.y, transform.position.z);
            moveCamera = Wait;
            GameRules.Instance.quiver.SetActive(true);
            Timer.Instance.Reset();
        }

        if (transform.position.x < currentPos.x - horizontalDiff)
        {
            transform.position = new Vector3(currentPos.x - horizontalDiff, transform.position.y, transform.position.z);
            moveCamera = Wait;
            GameRules.Instance.quiver.SetActive(true);
            Timer.Instance.Reset();
        }
        if (transform.position.y > currentPos.y + verticalDiff)
        {
            transform.position = new Vector3(transform.position.x, currentPos.y + verticalDiff, transform.position.z);
            moveCamera = Wait;
            GameRules.Instance.quiver.SetActive(true);
            Timer.Instance.Reset();
        }

        if (transform.position.y < currentPos.y - verticalDiff)
        {
            transform.position = new Vector3(transform.position.x, currentPos.y - verticalDiff, transform.position.z);
            moveCamera = Wait;
            GameRules.Instance.quiver.SetActive(true);
            Timer.Instance.Reset();
        }
        if (moveCamera.Method.Name != "Wait")
        {
            InputController.Instance.enabled = false;
        }
        else
        {
            InputController.Instance.enabled = true;
        }

    }
}
