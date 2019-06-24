using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class HerrowClicked : MonoBehaviour
{
    Sprite curr;
    [HideInInspector]
    public bool[] used;
    [HideInInspector]
    public Button b;
    [HideInInspector]
    public Herrow thisHerrow;



    public void Clicked()
    {
        foreach (Herrow h in GameRules.Instance.herrows)
        {
            if (h.card == curr)
            {
                thisHerrow=h;
                foreach (GameplayBehaviour beh in GameRules.Instance.allBehaviours)
                {
                    if (beh.ToString().Contains("Arrow") && beh.isActiveAndEnabled)
                    {
                        beh.GetComponentInChildren<SkeletonAnimator>().skeletonDataAsset = h.skin;
                        beh.GetComponentInChildren<SkeletonRenderer>().Initialize(true);
                        beh.GetComponentInChildren<Animator>().runtimeAnimatorController = h.controller;
                        beh.GetComponent<Ability>().thisAbility = h.ability;
                        GetComponentInParent<AudioSource>().Play();
                        AudioSource soundEmitter = beh.GetComponent<AudioSource>();
                        ArrowBehaviour a = beh.GetComponent<ArrowBehaviour>();
                        switch (h.ability)
                        {
                            case Ability.HerrowAbility.None:
                                {
                                    soundEmitter.clip = a.sounds[0];
                                    break;
                                }
                            case Ability.HerrowAbility.Freeze:
                                {
                                    soundEmitter.clip = a.sounds[4];
                                    break;
                                }
                            case Ability.HerrowAbility.Incorporeal:
                                {
                                    soundEmitter.clip = a.sounds[2];
                                    break;
                                }
                        }
                        soundEmitter.Play();
                        //if (h.rarity!=Herrow.Rarity.Common) {
                        //    used[GameRules.Instance.currentPlayer]=true;

                        //}
                        popupQuiver pq = gameObject.GetComponentInParent<popupQuiver>();
                        if (pq != null)
                        {
                            pq.gameObject.SetActive(false);
                        }

                        break;
                    }
                }
            }
        }
    }


    private Herrow SetCurrentHerrow() {
        foreach(Herrow h in GameRules.Instance.herrows) {
            if(h.card == curr) {
                return h;
            }
        }
        return null;
    }
    

    private void Start()
    {
        thisHerrow=SetCurrentHerrow();
        Debug.Assert(thisHerrow!=null);
        curr = GetComponent<Image>().sprite;
        b = GetComponent<Button>();
        used = new bool[GameRules.Instance.nPlayer];
        for (int i = 0; i < used.Length; i++)
        {
            used[i] = false;
        }
    }

    private void Update()
    {
        if (used[GameRules.Instance.currentPlayer])
        {
            b.interactable = false;
        }
        else
        {
            b.interactable = true;
        }
    }

}
