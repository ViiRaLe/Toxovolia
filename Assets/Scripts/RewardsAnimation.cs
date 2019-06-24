using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsAnimation : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject center;
    public GameObject exit;

    private GameObject lastButton;
    private GameObject exitButton;
    private int i = 0;

    private Coroutine anIn, anOut;

    private bool running = false;
    private bool finisher = true;
    private bool enter = false;
    private bool enter2 = true;
    public GameObject victory;
    private bool touched = false;

    private void LateUpdate()
    {
        if (victory.activeSelf)
        {
            enter = true;
        }

        if (enter && enter2)
        {
            exitButton = buttons[0];
            OnTap();
            enter2 = false;
        }

        if (running)
        {
            anIn = StartCoroutine(AnimationIn(buttons[i]));
        }

        if (lastButton != null && lastButton.transform.position.x <= center.transform.position.x)
        {
            StopAllCoroutines();
            i++;
            exitButton = lastButton;
            lastButton = null;
            touched = false;
        }

        if (exit != null && exitButton != null && exitButton.transform.position.x <= exit.transform.position.x)
        {
            StopCoroutine(anOut);
        }

        if (buttons[3].transform.position.x <= center.transform.position.x)
        {
            finisher = false;
            buttons[4].SetActive(true);
            buttons[5].SetActive(true);
        }

    }

    public void OnTap()
    {
        if (finisher)
        {
            if (!touched)
            {
                touched = true;
                anOut = StartCoroutine(AnimationOut(exitButton));
                running = true;
            }
        }
    }


    IEnumerator AnimationIn(GameObject go)
    {
        lastButton = go;
        go.transform.position += new Vector3(-0.1f, 0, 0);
        yield return new WaitForSeconds(0.001f);
        anIn = StartCoroutine(AnimationIn(lastButton));
    }

    IEnumerator AnimationOut(GameObject go)
    {
        running = false;
        go.transform.position += new Vector3(-0.1f, 0, 0);
        yield return new WaitForSeconds(0.001f);
        anOut = StartCoroutine(AnimationOut(exitButton));
    }

}
