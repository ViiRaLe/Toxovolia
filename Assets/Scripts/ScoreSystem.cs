using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*NEED TO REWORK
NO MORE ON SCREEN SCORE BUT POINT POP-UP
DOUBLE SCORE (2 Players)
*/

public class ScoreSystem
{
    [HideInInspector]
    public static double score;
    [HideInInspector]
    public static Vector2 contact;


    public static void AddScore(Vector2 contactDone, int player, int minTarget, int minPoints)    //(minTarget)0 COLPITO TARGET ; 1 COLPITO RESTO CORPO 
    {
        contact = contactDone;

        if (minTarget == 0)  // TARGET
        {
            score = System.Math.Round(GameRules.Instance.herrowLevel.Evaluate(1) * GameRules.Instance.basePoints.Evaluate(contactDone.x) * GameRules.Instance.stepMultiplier.Evaluate(GameRules.Instance.currentStep), 2) * 100;
            TextAppear.Instance.ShowText(score.ToString(), 0);
            GameRules.Instance.score[player] += score;
            Debug.Log(GameRules.Instance.score[player]);
        }
        else if (minTarget == 1)   // MIN POINTS
        {
            score = System.Math.Round(GameRules.Instance.herrowLevel.Evaluate(1) * minPoints * GameRules.Instance.stepMultiplier.Evaluate(GameRules.Instance.currentStep), 2) * 100;
            TextAppear.Instance.ShowText(score.ToString(), 0);
            GameRules.Instance.score[player] += score;

        }
    }

    public static void Missed(Vector2 contactDone ,int player)
    {
        contact = contactDone;

        TextAppear.Instance.ShowText("", 1);
    }

    void UpdateScore(int player, double scoreUpd)
    {

    }
}
