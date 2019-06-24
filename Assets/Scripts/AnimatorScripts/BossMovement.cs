using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : StateMachineBehaviour
{
    public float speed = 1f;
    public float waitForMoveCamera = 0.5f;
    private Renderer rnd;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rnd = animator.GetComponent<Renderer>();
        if (!GameRules.Instance.IsPlayerOne() && stateInfo.IsName("HorizontalMovement"))
        {
            speed = -speed;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float speedToApply = speed * Time.deltaTime;
        if (stateInfo.IsName("HorizontalMovement"))
        {
            animator.transform.Translate(speedToApply, 0, 0);
        }
        else if (stateInfo.IsName("VerticalMovement"))
        {
            animator.transform.Translate(0, speedToApply, 0);
        }
        if (!rnd.isVisible)
        {

            GameRules.Instance.ChangeTurn(waitForMoveCamera);
            Destroy(animator.gameObject);
        }
    }
}
