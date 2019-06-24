using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{
    private Text text;
    public bool p1;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (p1)
        {
            text.text = " " + GameRules.Instance.score[0];
        }
        else
        {
            text.text = GameRules.Instance.score[1] + " ";
        }
    }
}
