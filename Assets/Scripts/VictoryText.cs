using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{

    private void Start()
    {
        if (GameRules.Instance.score[0] == GameRules.Instance.score[1])
        {
            GetComponent<Text>().text = "DRAW";
        }
        else
        {
            GetComponent<Text>().text = GameRules.Instance.score[0] > GameRules.Instance.score[1] ? "P1 WINS" : "P2 WINS";
        }


    }
}
