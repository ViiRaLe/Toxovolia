using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsAppearing : MonoBehaviour
{
    public GameObject[] go;

    private void Start()
    {
        StartCoroutine(AppearPoints(go[0], go[1]));
        StartCoroutine(AppearWinner(go[2], go[3]));
    }

    IEnumerator AppearPoints(GameObject go, GameObject go2)
    {
        yield return new WaitForSeconds(0.5f);
        go.gameObject.SetActive(true);
        go2.gameObject.SetActive(true);
    }

    IEnumerator AppearWinner(GameObject go, GameObject go2)
    {
        yield return new WaitForSeconds(1.5f);
        go.gameObject.SetActive(true);
        go2.gameObject.SetActive(true);
    }

}
