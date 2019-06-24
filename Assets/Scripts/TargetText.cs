using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetText : GameplayBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().sortingOrder = 4;
    }

    protected override void OnExitSetPosition()
    {
        Destroy(gameObject);
    }

}
