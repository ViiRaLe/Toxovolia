using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionerAnimHandler : GameplayBehaviour
{

    private Animator ani;
    private Rigidbody2D body;
    private float startXPos;


    public bool stopAcceleration = false;


    protected override void StartSetPosition()
    {
        //set acceleration 
        if (velocity >= velocityMax)
        {
            velocity = velocityMax;
        }
        else if (!stopAcceleration)
        {
            velocity += vAcceleration;
        }
        else
        {
            if (!GameRules.Instance.quiver.activeSelf)
            {
                stopAcceleration = false;
            }
        }

        //set direction
        if ((dir == 1 && transform.position.x >= startXPos + movementOffset) || (dir == -1 && transform.position.x <= startXPos - movementOffset))
        {
            dir = -dir;
        }
        //apply velocity
        applyVel = velocity * dir;
        //move Balista horizontally
        body.velocity = new Vector2(applyVel, 0);
    }

    protected override void OnExitSetPosition()
    {
        velocity = 0;
        vAcceleration = 0;
        velocityMax = 0;
        //stop horizontal movement
        body.velocity = Vector2.zero;

        //enable rotation(-45 to 45)
        ani.enabled = true;
    }

    protected override void Preparation()
    {
        //increase velocity of animation if is applied more force
        float sp = Mathf.Clamp(((start.y - InputController.WorldPosition.y) / maxDrag.y), 0, 1);
        if (sp > minDragForMultiplier) sp = (sp / minDragForMultiplier) * animationMultiplier;
        else sp = 0;
        ani.speed = animationDefault;
        ani.speed += sp;
    }

    protected override void Throw()
    {
        //disable animation movement
        ani.enabled = false;
    }



    private void Awake()
    {
        ani = GetComponent<Animator>();
        ani.enabled = false;
        body = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        startXPos = transform.position.x;
    }





}
