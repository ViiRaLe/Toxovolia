using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    SpriteRenderer s;
    float defAlpha;
    public float alphavVar = 0.01f;
    int dir = 1;

    private void Awake()
    {
        s = GetComponent<SpriteRenderer>();
        defAlpha = s.color.a;
    }

    private void Update()
    {
        if (s.color.a <= 0 || s.color.a >= defAlpha)
        {
            dir = -dir;
        }
        s.color = new Color(s.color.r, s.color.g, s.color.b, s.color.a + alphavVar * dir);
    }
}
