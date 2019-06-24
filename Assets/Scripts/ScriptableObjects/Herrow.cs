
#define SPINE_OPTIONAL_RENDEROVERRIDE
#define SPINE_OPTIONAL_MATERIALOVERRIDE
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Herrow", menuName = "Toxovolia/Herrow", order = 1)]
public class Herrow : ScriptableObject
{
    public new string name = "Herrow";
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }




    public Rarity rarity;
    public Ability.HerrowAbility ability;
    public SkeletonDataAsset skin;
    public Sprite card;
    public RuntimeAnimatorController controller;
    public int level = 1;
    public int exp = 0;
    public float force = 0f;
    public float agility = 0f;
    public float intelligence = 0f;
    public AudioClip herrowInQuiver;
    public AudioClip herrowThrowed;


}
