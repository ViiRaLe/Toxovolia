using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalistaBehaviour : GameplayBehaviour
{
    Transform parent;
    Vector3 rot;
    Vector3 scale;
    Animator ani;
    AudioSource audio;

    public AudioClip[] sounds;


    /*protected override void StartSetPosition()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }*/


    protected override void OnExitSetPosition()
    {
        transform.parent = null;

    }


    protected override void Preparation()
    {
        ani.SetFloat("Blend", Mathf.Clamp(((start.y - InputController.WorldPosition.y) / maxDrag.y), 0, 1));
        transform.parent = parent;
        transform.rotation = new Quaternion();
        transform.localScale = scale;
        /*if (!audio.isPlaying)
        {
            audio.Play();
        }*/
    }

    protected override void Throw()
    {
        audio.clip = sounds[0];
        audio.loop = false;
        audio.Play();
    }


    private void Awake()
    {
        ani = GetComponent<Animator>();
        parent = transform.parent;
        scale = transform.localScale;
        audio = GetComponent<AudioSource>();
    }



}
