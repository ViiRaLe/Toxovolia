using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupQuiver : MonoBehaviour
{
    private static PopupQuiver instance;
    public static PopupQuiver Instance
    {
        get
        {
            return instance;
        }
    }

    private HerrowClicked[] herrowsInQuiver;
    public HerrowClicked[] HerrowsInQuiver {
        get {
            return herrowsInQuiver;
        }
    }

    public GameObject HerrowCard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("too much Quivers");
        }
        herrowsInQuiver=GetComponentsInChildren<HerrowClicked>();
    }
    
    
    public HerrowClicked GetHerrow(string name) {
        foreach(HerrowClicked hc in herrowsInQuiver) {
            if(hc.GetComponent<Image>().sprite.name == name) {
                return hc;
            }
        }
        return null;
    }

    public HerrowClicked DisableHerrow(string name) {
        HerrowClicked hc = GetHerrow(name);
        Debug.Assert(hc!=null);
        hc.used[GameRules.Instance.currentPlayer] = true;
        return null;
    }
    

    private void Start()
    {
        Transform grid = GetComponentInChildren<HorizontalLayoutGroup>().transform;
        //instantiate cards
        for (int i = 0; i < GameRules.Instance.herrows.Length; i++)
        {
            GameObject go = Instantiate(HerrowCard, grid);
            go.GetComponent<Image>().sprite = GameRules.Instance.herrows[i].card;


        }

    }




    // Update is called once per frame
    void Update()
    {
        //stop current balista acceleration
        foreach (GameplayBehaviour beh in GameRules.Instance.allBehaviours)
        {
            if (beh.ToString().Contains("PlayerInteractible") && beh.isActiveAndEnabled)
            {
                DirectionerAnimHandler current = (DirectionerAnimHandler)beh;
                current.stopAcceleration = true;
            }
        }



    }

}