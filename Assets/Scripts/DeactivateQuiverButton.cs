using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateQuiverButton : GameplayBehaviour
{
    private static DeactivateQuiverButton instance;
    public static DeactivateQuiverButton Instance
    {
        get
        {
            return instance;
        }
    }


    public float alpha = 0.5f;
    Button b;
    Image i;
    float defAlpha;

    private void Awake()
    {
        b = GetComponent<Button>();
        i = GetComponent<Image>();
        defAlpha = i.color.a;
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ResetState()
    {
        phase = SelectHerrow;
        b.interactable = true;
        i.color = new Color(i.color.r, i.color.g, i.color.b, defAlpha);
    }

    protected override void OnExitSetPosition()
    {
        if (b.IsInteractable())
        {

            i.color = new Color(i.color.r, i.color.g, i.color.b, alpha);
            b.interactable = false;
        }
    }

}
