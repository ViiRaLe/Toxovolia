using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : StateMachineBehaviour
{
    SpriteRenderer[] target;
    public float waitForDisplayEnd = 1;//err in multiple instances of EnemyLogic
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        target = animator.GetComponentsInChildren<SpriteRenderer>();
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("TurnCompleted") || stateInfo.IsName("Hit"))
        {
            //destroy target
            if (target != null)
            {
                foreach (SpriteRenderer sr in target)
                {
                    Destroy(sr.gameObject);
                }
                target = null;
            }
            //set movement
            if (!GameRules.Instance.AllPlayersPlayedStep())
            {
                animator.SetTrigger("Hmove");
            }
            else
            {
                if (GameRules.Instance.IsNotLastStep())
                {
                    animator.SetTrigger("Vmove");
                }
                else
                {
                    animator.SetTrigger("Die");
                    GameRules.Instance.EndMatch(waitForDisplayEnd);
                }
            }
        }
    }





}
