using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetActive : MonoBehaviour
{
    private Renderer rnd;
    private GameplayBehaviour[] beh;


    // Use this for initialization
    void Start()
    {
        rnd = gameObject.GetComponentInChildren<Renderer>();
        beh = gameObject.GetComponents<GameplayBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameplayBehaviour b in beh)
        {
            if (!b.enabled && rnd.isVisible && GameRules.Instance.mainCamera.moveCamera.Method.Name == "Wait")
            {
                b.enabled = true;
                if (GameRules.Instance.wind != null) GameRules.Instance.wind.EnableWind();
            }

        }


    }
}
