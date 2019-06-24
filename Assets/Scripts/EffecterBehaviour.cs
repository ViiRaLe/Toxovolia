using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class EffecterBehaviour : GameplayBehaviour
{
    private Renderer rend;
    private Animator ani;







    protected override void Preparation()
    {
        //effecter inclination relative to x drag
        float blend = Mathf.Clamp((InputController.WorldPosition.x - start.x) / maxDrag.x, -1, 1);
        float actualForce = Mathf.Clamp((start.y - InputController.WorldPosition.y) / maxDrag.y, 0, 1);
        ani.SetFloat("Blend", blend);
        Material mat = rend.material;
        mat.SetFloat("_Threshold", actualForce);


    }



    protected override void Throw()
    {
        Destroy(gameObject);
    }




    protected override void Start()
    {
        base.Start();
        ani = GetComponentInChildren<Animator>();
        rend = GetComponentInChildren<Renderer>();
    }
}





