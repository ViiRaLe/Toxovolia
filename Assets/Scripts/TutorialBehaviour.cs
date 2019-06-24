using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : GameplayBehaviour
{
    Animator ani;


    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    protected override void SelectHerrow()
    {
        ani.SetTrigger("Tap");
    }


    protected override void OnExitSetPosition()
    {
        ani.SetTrigger("Drag");
    }


    protected override void Preparation()
    {
        if (start.y - InputController.WorldPosition.y > 0)
        {
            ani.SetTrigger("Effect");
        }
        else
        {
            ani.SetTrigger("Drag");
        }
    }

    protected override void Throw()
    {
        Destroy(gameObject);
    }


}
