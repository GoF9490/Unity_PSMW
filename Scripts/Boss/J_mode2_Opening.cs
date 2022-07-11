using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_mode2_Opening : MonoBehaviour
{
    public bool _open1 = false;
    public GameObject _end;

    private void Update()
    {
        if (GetComponent<SpriteRenderer>().color.a < 1 && _open1 == false)
        {
            Color fadeA = GetComponent<SpriteRenderer>().color;
            fadeA.a += Time.deltaTime * 0.1f;
            GetComponent<SpriteRenderer>().color = fadeA;
        }

        if (GetComponent<SpriteRenderer>().color.a >= 1)
        {
            _open1 = true;
        }

        if (GetComponent<SpriteRenderer>().color.a > 0 &&_open1 == true)
        {
            Color fadeA = GetComponent<SpriteRenderer>().color;
            fadeA.a -= Time.deltaTime * 0.5f;
            GetComponent<SpriteRenderer>().color = fadeA;
        }

        if (GetComponent<SpriteRenderer>().color.a <= 0 && _open1 == true)
        {
            _end.SetActive(true);
        }
    }
}
