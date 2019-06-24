using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparentDisable : MonoBehaviour
{
    Button b;


    private void Awake()
    {
        b = GetComponent<Button>();
    }

    private void Update()
    {
        b.interactable = false;
        //attiva al secondo giro
    }
}
