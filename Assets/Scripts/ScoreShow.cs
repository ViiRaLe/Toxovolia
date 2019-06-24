using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour
{
    [Range(1, 2)]
    public int player;


    private void Start()
    {
        GetComponent<Text>().text = GameRules.Instance.score[player - 1].ToString();
    }

}
