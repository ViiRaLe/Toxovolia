using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindSystem : MonoBehaviour
{

    private Text windText;
    private Rigidbody2D arrow;

    private void Awake()
    {
        arrow = GetComponentInChildren<Rigidbody2D>();
        windText = GetComponentInChildren<Text>();
    }





    private void Update()
    {
        if (GameRules.Instance.wind != null)
        {
            arrow.rotation = 180 + GameRules.Instance.wind.GetComponent<AreaEffector2D>().forceAngle;
            windText.text = " \n" + Mathf.Round((GameRules.Instance.wind.GetComponent<AreaEffector2D>().forceMagnitude) * 100);
        }
    }

}
