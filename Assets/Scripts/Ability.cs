using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : GameplayBehaviour
{
    public enum HerrowAbility
    {
        None,
        Freeze,
        Incorporeal
    }
    public HerrowAbility thisAbility;

    protected override void Throw()
    {
        switch (thisAbility)
        {
            case HerrowAbility.Freeze:
                {
                    foreach (ObstacleBehaviour beh in GameRules.Instance.obstacles)
                    {
                        if (beh.ToString().Contains("MobileObstacle") && beh.GetComponent<Renderer>().isVisible)
                        {
                            beh.GetComponent<Animator>().SetTrigger("Freeze");
                        }
                    }
                    break;
                }
            case HerrowAbility.Incorporeal:
                {
                    foreach (ObstacleBehaviour beh in GameRules.Instance.obstacles)
                    {
                        if ((beh != null) && beh.gameObject.ToString().Contains("Obstacle") && beh.GetComponent<Renderer>().isVisible)
                        {
                            Destroy(beh.GetComponent<Collider2D>());
                        }
                    }
                    break;
                }
        }
    }

}
