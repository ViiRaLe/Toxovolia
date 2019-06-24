using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasuryButton : MonoBehaviour
{
    public GameObject popupTreasury;

    void OnClickRemove()
    {
        popupTreasury.SetActive(true);
    }

    void OnClickTransparent()
    {
        popupTreasury.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
