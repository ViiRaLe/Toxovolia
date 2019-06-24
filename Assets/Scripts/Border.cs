using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Border : MonoBehaviour
{
    GameObject player1, player2;
    Image p1, p2;
    Color temp1, temp2;

    private void Awake()
    {
        player1 = GameObject.FindWithTag("PlayerIcon1");
        player2 = GameObject.FindWithTag("PlayerIcon2");
        p1 = player1.GetComponent<Image>();
        p2 = player2.GetComponent<Image>();
        temp1 = p1.color;
        temp2 = p2.color;
    }

    private void Update()
    {
        if (GameRules.Instance.currentPlayer == 0)
        {
            transform.position = player1.transform.position;

            temp2.a = 0.5f;
            p2.color = temp2;
            temp1.a = 1f;
            p1.color = temp1;

        }
        else if (GameRules.Instance.currentPlayer == 1)
        {
            transform.position = player2.transform.position;

            temp1.a = 0.5f;
            p1.color = temp1;
            temp2.a = 1f;
            p2.color = temp2;
        }
    }

}
