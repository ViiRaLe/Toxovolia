using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : GameplayBehaviour
{

    private static Timer instance;
    public static Timer Instance
    {
        get
        {
            return instance;
        }
    }

    #region Variables

    private Text text;

    public float countdownValue;
    [HideInInspector]
    public float currCountdownValue;
    bool crRunning = false;
    public Coroutine crTimer;
    #endregion

    #region CoRoutines

    public IEnumerator StartCountdown(float countdownValue)
    {
        while (currCountdownValue >= 0)
        {
            if (text != null)
            {
                text.text = "" + currCountdownValue;
            }
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            if (currCountdownValue <= 0)
            {
                if (text != null)
                {
                    TimerEnd();
                    crRunning = false;
                }
            }
        }
    }

    #endregion


    #region Methods
    public void Reset()
    {
        if (crRunning)
        {
            if (crTimer != null) StopCoroutine(crTimer);
            crRunning = false;
        }
        currCountdownValue = countdownValue;
        phase = SelectHerrow;
        if (!crRunning)
        {
            crTimer = StartCoroutine(StartCountdown(countdownValue));
            crRunning = true;
        }
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
            Debug.LogWarning("Multiple instances of Timer");
        }

        //init vars
        text = GetComponent<Text>();

        currCountdownValue = countdownValue;
        text.text = "" + currCountdownValue;
    }


    protected override void SelectHerrow()
    {
        if (!crRunning)
        {
            crTimer = StartCoroutine(StartCountdown(countdownValue));
            crRunning = true;
        }
    }



    protected override void Throw()
    {
        if (crRunning)
        {
            StopCoroutine(crTimer);
            crRunning = false;
            phase = SelectHerrow;
        }
    }

    #endregion
}
