using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindHandler : MonoBehaviour
{
    public enum CardinalPosition
    {
        E,
        NE,
        N,
        NW,
        W,
        SW,
        S,
        SE
    }

    public float windForceVariation;
    public CardinalPosition windDirectionFrom;
    public CardinalPosition windDirectionTo;

    private float defaultForceMagnitude;


    private AreaEffector2D ae;

    public void AlterWind()
    {
        ae.forceMagnitude = Random.Range(defaultForceMagnitude, defaultForceMagnitude + windForceVariation);
        ae.forceAngle = Random.Range((int)windDirectionFrom, (int)windDirectionTo + 1) * 45;


    }

    public void DisableWind()
    {
        ae.enabled = false;
    }

    public void EnableWind()
    {
        ae.enabled = true;
    }

    public bool IsEnabled()
    {
        return ae.enabled;
    }

    private void Awake()
    {
        ae = GetComponent<AreaEffector2D>();
        defaultForceMagnitude = ae.forceMagnitude;
    }


}
