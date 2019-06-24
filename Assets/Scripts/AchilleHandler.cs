using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchilleHandler : MonoBehaviour
{
    private Vector2 startPos;
    Rigidbody2D body;
    Vector2 finalPos;
    public float speed;
    public GameObject hand;
    private bool enter = true;
    private bool enter2 = true;
    public GameObject tap, drag, release;

    private void Awake()
    {
        startPos = transform.position;
        body = GetComponent<Rigidbody2D>();
        finalPos = GameObject.FindWithTag("achillePos").transform.position;
    }

    private void Update()
    {
        if (enter)
        {
            if (transform.position.x < finalPos.x)
            {
                body.velocity = new Vector2(speed, 0);
            }
            else
            {
                body.velocity = new Vector2(0, 0);
                hand.SetActive(true);
                enter = false;
            }
        }

        if (enter2)
        {
            if (hand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("tap"))
            {
                tap.SetActive(true);
            }
            else if (hand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("drag"))
            {
                tap.SetActive(false);
                drag.SetActive(true);
            }
            else if (hand.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("scroll dx e sx"))
            {
                drag.SetActive(false);
                release.SetActive(true);
                enter2 = false;
            }
        }

        if (GameRules.Instance.currentPlayer == 1)
        {
            tap.SetActive(false);
            drag.SetActive(false);
            release.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
