using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Ability))]
public class ArrowBehaviour : GameplayBehaviour
{

    #region Private Variables

    private Rigidbody2D body;
    private Vector3 startPosition;
    private Collider2D coll;
    private float forceToApply = 0f;
    private Animator ani;
    private Renderer rnd;
    private bool waitHisDeath = false;
    private float forceIncrement;
    private float intIncrement;
    private float effectIncrement;
    private AudioSource audioSource;
    #endregion

    #region Public Variables
    //rigidbody vars
    public float gravity = 0f;
    public float drag = 0.2f;
    public float mass = 0.1f;
    public float angularDrag = 0f;

    //out of level sound
    public AudioClip outOfLevelSound;
    public AudioClip collisionSound;

    

    #endregion

    #region States Methods

    protected override void OnExitSetPosition()
    {
        //drag start info and saving position
        start = InputController.WorldPosition;
        startPosition = transform.position;
        ani.SetTrigger("Tap");
    }



    protected override void SetThrow()
    {
        //get drag infos
        end = InputController.WorldPosition;
        result = (end - start);

        //set variables for flight
        if (result.y < 0)
        {
            result.y = -result.y / maxDrag.y;
            result.x = Mathf.Clamp(result.x / maxDrag.x, -1, 1);
            startPosition = transform.position;
        }
    }

    protected override void Throw()
    {
        //adding Rigidbody2D
        body = gameObject.AddComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Dynamic;
        body.gravityScale = gravity;
        body.drag = drag;
        body.mass = mass;
        body.angularDrag = angularDrag;

        //get Params
        foreach (Herrow h in GameRules.Instance.herrows)
        {
            if (h.controller == ani.runtimeAnimatorController)
            {
                forceIncrement = h.force;
                effectIncrement = h.agility;
                intIncrement = h.intelligence;//not implemented yet
                //Ciao alberto, se stai leggendo queste righe...beh so che si poteva implementare in un altro modo
                //ma non c' ho tempo
                switch (h.ability)
                {
                    case Ability.HerrowAbility.None:
                        {
                            audioSource.clip = PopupQuiver.Instance.GetHerrow("Filosofo").thisHerrow.herrowThrowed;
                            break;
                        }
                    case Ability.HerrowAbility.Freeze:
                        {
                            HerrowClicked hc = PopupQuiver.Instance.DisableHerrow("Medusa");
                            audioSource.clip = hc.thisHerrow.herrowThrowed;
                            break;
                        }
                    case Ability.HerrowAbility.Incorporeal:
                        {
                            HerrowClicked hc = PopupQuiver.Instance.DisableHerrow("Ade");
                            audioSource.clip = hc.thisHerrow.herrowThrowed;
                        break;
                        }
                }
                audioSource.Play();
            }
        }


        //throw Arrow
        transform.parent = null;
        ani.SetTrigger("Fly");
        result.y = Mathf.Clamp(result.y, 0, 1);
        forceToApply = result.y * force;
        if (Mathf.Abs(forceToApply) > force) forceToApply = force;
        body.AddRelativeForce(new Vector2(0, forceToApply + forceIncrement), ForceMode2D.Force);

        //disable herrow


    }




    protected override void ArrowFlight()
    {
        //make arrow follow target
        Vector2 actualSpeed = body.velocity;
        float rot = Mathf.Atan2(actualSpeed.y, actualSpeed.x) * Mathf.Rad2Deg;
        if (rot != 0)
        {
            rot -= 90;
        }
        if ((body.position.x > startPosition.x && end.x < start.x) || (body.position.x < startPosition.x && end.x > start.x) || (body.velocity.y > 0))
        {
            if (Mathf.Abs(end.x - start.x) > rangeDisableEffect && forceToApply >= minForceForEffect)
            {
                //adding effect
                body.AddRelativeForce(new Vector2(result.x * (effectForce + effectIncrement), 0), ForceMode2D.Force);
                //follows direction
                body.rotation = rot;
            }
        }
    }

    #endregion

    //handles out of screen arrow
    public void OutOfLevel()
    {
        SoundsHandler.Instance.audioSource.clip =outOfLevelSound;
        SoundsHandler.Instance.audioSource.Play();


        //get 0 point
        TextAppear.Instance.ShowText("", 3);
        ObstacleBehaviour.bossAnimator.GetComponent<Collider2D>().enabled = false;

        //animate boss
        ObstacleBehaviour.bossAnimator.SetBool("TurnCompleted", true);

        //destroy arrow
        Destroy(gameObject);
    }

    private void notRender()
    {
        rnd.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (body != null && (collision.tag == "Target" || collision.tag == "Obstacle" || collision.tag == "MinTarget"))
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                //bleah
                audioSource.Stop();
                audioSource.clip = collisionSound;
                audioSource.Play();
            }
            //arrow animation
            if (ani != null)
            {
                ani.SetTrigger("Collision");
            }



            //arrow stop
            body.velocity = Vector2.zero;
            phase = null;
            body.drag = 1000;
            coll.enabled = false;
        }
    }

    public void HideAndDestroy()
    {
        Instantiate(GameRules.Instance.hiderEffect, new Vector2(transform.position.x, transform.position.y - 0.2f), Quaternion.identity, transform);
        Invoke("notRender", GameRules.Instance.hiderEffectDuration * 0.5f);
        Destroy(gameObject, GameRules.Instance.hiderEffectDuration);

    }

    protected override void Start()
    {
        base.Start();
        ani = GetComponentInChildren<Animator>();
        rnd = GetComponentInChildren<Renderer>();
        coll = GetComponentInChildren<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        //handle out of Screen
        if (!rnd.isVisible && phase != null && phase.Method.Name == "ArrowFlight")
        {
            OutOfLevel();
        }

        if (rnd.isVisible && Timer.Instance.currCountdownValue < 0 && phase!=null && phase.Method.Name!="Preparation")
        {
            Destroy(this);
            Destroy(GetComponent<Ability>());
            Destroy(GetComponent<ResetActive>());
            //ObstacleBehaviour.bossAnimator.SetBool("TurnCompleted",true);
        }





        //death phases
        if (!waitHisDeath && (ani.runtimeAnimatorController != null && ani.GetCurrentAnimatorStateInfo(0).IsName("StartPuff")))
        {
            //arrow Destroy
            HideAndDestroy();

            //move Boss
            ObstacleBehaviour.bossAnimator.SetBool("TurnCompleted", true);

            //stay relaxed and wait
            waitHisDeath = true;
        }
    }


   

}
