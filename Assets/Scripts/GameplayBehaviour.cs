using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(ResetActive))]
public class GameplayBehaviour : MonoBehaviour
{

    protected delegate void GameplayPhase();
    protected GameplayPhase phase;
    protected int dir = 1;
    protected float applyVel = 0;

    public static bool collided = false;


    protected float velocityMax;
    protected float velocity;
    protected float vAcceleration;
    protected float movementOffset;

    protected Vector2 maxDrag;
    protected float effectForce;
    protected float force;
    protected float minForceForEffect;
    protected float rangeDisableEffect;
    protected float animationDefault;
    protected float animationMultiplier;
    protected float minDragForMultiplier;


    protected static Vector2 start, end, result;


    protected virtual void SelectHerrow() { }

    /**
       Automatic movement of Balista in xaxis
    */
    protected virtual void StartSetPosition() { }

    /**
       Released finger
    */
    protected virtual void OnExitSetPosition() { }


    /**
       Selecting force and effect
    */
    protected virtual void Preparation() { }

    /**
       Selected force and effect
    */
    protected virtual void SetThrow() { }

    /**
       if correct, Throw Arrow
    */
    protected virtual void Throw() { }

    /**
        Arrow flight
    */
    protected virtual void ArrowFlight() { }



    protected void TimerEnd()
    {
        //finger "release"
        switch (phase.Method.Name)
        {

            case "SelectHerrow":
            case "StartSetPosition":
            case "SetPosition":
            case "OnExitSetPosition":
            case "StartPreparation":
                {
                    //METTI A VIDEO TIME'S UP
                    ObstacleBehaviour.bossAnimator.SetBool("TurnCompleted", true);
                    //Timer.Instance.Reset();
                    break;
                }
            case "Preparation":
                {
                    InputController.ForceStopDrag();
                    break;
                }
        }
    }



    // Use this for initialization
    protected virtual void Start()
    {
        //set start phase
        phase = SelectHerrow;

        //Set all variables declared in BalistaTuning
        velocity = BalistaTuning.Instance.velocity;
        velocityMax = BalistaTuning.Instance.velocityMax;
        vAcceleration = BalistaTuning.Instance.vAcceleration;
        movementOffset = BalistaTuning.Instance.movementOffset;
        maxDrag = BalistaTuning.Instance.maxDrag;
        effectForce = BalistaTuning.Instance.effectForce;
        force = BalistaTuning.Instance.force;
        minForceForEffect = BalistaTuning.Instance.minForceForEffect;
        rangeDisableEffect = BalistaTuning.Instance.rangeDisableEffect;
        animationDefault = BalistaTuning.Instance.animationDefault;
        animationMultiplier = BalistaTuning.Instance.animationMultiplier;
        minDragForMultiplier = BalistaTuning.Instance.minDragForMultiplier;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (phase != null)
        {

            phase();

        }
    }



    private void LateUpdate()
    {
        if (phase != null)
        {
            //handle change state
            switch (phase.Method.Name)
            {

                case "SelectHerrow":
                    {
                        if (!GameRules.Instance.quiver.activeSelf && Timer.Instance.crTimer != null)
                        {
                            phase = StartSetPosition;
                        }
                        break;
                    }
                case "StartSetPosition":
                    {
                        if (InputController.Pressed)
                        {
                            phase = OnExitSetPosition;
                        }
                        break;
                    }
                case "OnExitSetPosition":
                    {
                        phase = Preparation;
                        break;
                    }
                case "Preparation":
                    {
                        if (InputController.Released)
                        {
                            phase = SetThrow;
                        }
                        break;
                    }
                case "SetThrow":
                    {
                        if (end.y - start.y < 0 && !InputController.IsTap)
                        {
                            phase = Throw;
                        }
                        else
                        {
                            phase = StartSetPosition;
                        }
                        break;
                    }
                case "Throw":
                    {
                        phase = ArrowFlight;
                        break;
                    }
                case "ArrowFlight":
                    {

                        break;
                    }
            }
        }
    }








}
