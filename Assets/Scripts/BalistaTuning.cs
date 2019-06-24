using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaTuning : MonoBehaviour
{
    protected static BalistaTuning instance;

    public static BalistaTuning Instance
    {
        get
        {
            return instance;
        }
    }

    public float velocityMax = 5f;
    public float velocity = 0f;
    public float vAcceleration = 0.01f;
    public float movementOffset = 1.5f;
    public Vector2 maxDrag;
    public float effectForce = 0.2f;
    public float force = 1f;
    public float minForceForEffect = 10f;
    public float rangeDisableEffect = 1f;
    public float animationDefault = 1f;
    public float animationMultiplier = 1f;
    [Range(0, 1)]
    public float minDragForMultiplier = 0.5f;


    void Awake()
    {
        //evito errori di più instanze
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of BalistaTuning");
        }

    }
}
