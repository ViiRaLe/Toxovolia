using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour
{

    #region Singleton Initialization

    private static TextAppear instance;
    public static TextAppear Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion

    #region Vars
    ObstacleBehaviour contactDone;
    Vector2 startPos;

    GameObject playerIcon1;
    GameObject playerIcon2;

    TextMesh text;

    MeshRenderer rendererText;

    Rigidbody2D body;

    public int baseFontSize;
    public AudioClip[] stepAudio;

    #endregion

    private void Awake()
    {
        //Singleton conditions
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of TextAppear");
        }


        //initializations
        body = GetComponent<Rigidbody2D>();
        playerIcon1 = GameObject.FindWithTag("PlayerIcon1");
        playerIcon2 = GameObject.FindWithTag("PlayerIcon2");
        text = GetComponentInChildren<TextMesh>();
        rendererText = GetComponentInChildren<MeshRenderer>();
        startPos = transform.position;

    }

    private void Start()
    {
        //initializations
        rendererText.sortingOrder = 20;
        ResetText();
    }

    //TEXT RESETTER METHOD
    void ResetText()
    {
        text.fontSize = baseFontSize;
        if (body != null) body.velocity = new Vector2(0, 0);
        transform.position = startPos;
        text.transform.position = startPos;
        text.text = "";
    }

    //TEXT WRITER ON SCREEN
    public void ShowText(string myText, int type)  //0 points, 1 missed, 2 player turn text, 3 out of screen, 4 step text
    {
        switch (type)
        {
            case 0:     ///Points
                {
                    text.text = "+" + myText;
                    transform.position = ScoreSystem.contact;
                    StartCoroutine(ScaleText(10));
                    StartCoroutine(MoveToIcon(GameRules.Instance.currentPlayer, type));

                    //AUTORESET ON END ANIMATION
                    break;
                }

            case 1:     ///Missed
                {
                    text.text = "Missed";
                    transform.position = ScoreSystem.contact;
                    StartCoroutine(ScaleText(5));
                    StartCoroutine(MoveToIcon(GameRules.Instance.currentPlayer, type));

                    //AUTORESET ON END ANIMATION
                    break;
                }

            case 2:     ///Text Player
                {
                    GameRules.Instance.GetComponent<AudioSource>().Stop();
                    GameRules.Instance.GetComponent<AudioSource>().Play();
                    StartCoroutine(TextDelayer(myText, type, 0.7f));
                    break;
                }

            case 3:  //out of screen
                {
                    text.text = "Missed";
                    transform.position = new Vector3(GameRules.Instance.mainCamera.transform.position.x, GameRules.Instance.mainCamera.transform.position.y, 0f);
                    StartCoroutine(ScaleText(5));
                    StartCoroutine(MoveToIcon(GameRules.Instance.currentPlayer, 1));

                    //AUTORESET ON END ANIMATION
                    break;
                }

            case 4:     ///Text Step
                {
                    GameRules.Instance.GetComponent<AudioSource>().Stop();
                    GameRules.Instance.GetComponent<AudioSource>().clip = stepAudio[GameRules.Instance.currentStep];
                    GameRules.Instance.GetComponent<AudioSource>().Play();
                    StartCoroutine(TextDelayer(myText, type, 1.5f));
                    break;
                }
        }
    }

    IEnumerator TextDelayer(string myText, int type, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.text = myText;
        transform.position = new Vector3(GameRules.Instance.mainCamera.transform.position.x, GameRules.Instance.mainCamera.transform.position.y, 0f);
        text.fontSize = 70;

        //RESET TEXT CONDITION
        StartCoroutine(Resetter(1.2f));
    }


    //Coroutines to animate

    IEnumerator ScaleText(int size)
    {
        for (int i = 0; i < size; i++)
        {
            text.fontSize += i;
            yield return new WaitForSeconds(0.01f + i * 0.0001f);
        }
    }

    IEnumerator ReduceText(int size)
    {
        for (int i = 0; i < size; i++)
        {
            text.fontSize -= i;
            yield return new WaitForSeconds(0.01f + i * 0.0001f);
        }

        ResetText();
    }

    IEnumerator MoveToIcon(int player, int type) //0 point, 1 miss
    {
        yield return new WaitForSeconds(0.2f);


        if (type != 1)  //if target hit
        {
            if (player == 0)  //p1
            {
                Vector2 direction = (transform.position - playerIcon1.transform.position).normalized * -90;
                if (body != null) body.velocity = direction;
            }
            else if (player == 1)  //p2
            {
                Vector2 direction = (transform.position - playerIcon2.transform.position).normalized * -90;
                if (body != null) body.velocity = direction;
            }

            yield return new WaitForSeconds(0.8f);
        }
        else  //missed
        {
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(ReduceText(15));
    }

    IEnumerator Resetter(float seconds) //timed resetter
    {
        yield return new WaitForSeconds(seconds);
        ResetText();
    }

}
