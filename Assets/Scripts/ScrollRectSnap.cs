using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    // Public Variables
    public RectTransform panel; // To hold the ScollPanel
    public Button[] bttn;
    public RectTransform center; // Center to compare the distance for each button
    public int startButton;

    // Private Variables
    private float[] distance; // All buttons' distance to the center
    private bool dragging = false; // Will be true, while we drag the panel
    private int bttnDistance; // Will hold the distance between the buttons
    private int minButtonNum; // To hold the number of the button, with smallest distance to center
    private bool messageSend = false;
    private bool targetNearestButton = true; // When true, it will lerp to the nearest button

    private void Start()
    {
        int bttnLength = bttn.Length;
        distance = new float[bttnLength];

        // Get distance between buttons
        panel.anchoredPosition = new Vector2((startButton - 1) * -353f, 0f);
    }

    private void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - bttn[i].transform.position.x);
        }

        if (targetNearestButton)
        {
            float minDistance = Mathf.Min(distance); // Get the min distance

            for (int a = 0; a < bttn.Length; a++)
            {
                if (minDistance == distance[a])
                {
                    minButtonNum = a;
                }

                if (!dragging)
                {
                    LerpToBttn(-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
                }
            }
        }
    }

    void LerpToBttn(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;

        targetNearestButton = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    public void GoToButton(int pos)
    {
        minButtonNum = pos - 1;
        StartCoroutine("LerpBttn");
    }

    IEnumerator LerpBttn()
    {
        while (true)
        {
            dragging = true;
            float position = -bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x;
            float newX = Mathf.Lerp(panel.anchoredPosition.x, position, .876f);
            Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

            panel.anchoredPosition = newPosition;

            dragging = false;
            yield break;
        }
    }
}