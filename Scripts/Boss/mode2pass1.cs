using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mode2pass1 : MonoBehaviour
{
    public GameObject _fade;
    public GameObject _fadeW;
    public GameObject _mode2;
    public GameObject _fire;

    public bool _flash = false;
    public bool _dark = false;
    bool _darking = false;

    private void Update()
    {
        if (_flash == true)
        {
            _flash = false;
            Color fadeA = _fadeW.GetComponent<SpriteRenderer>().color;
            fadeA.a = 1;
            _fadeW.GetComponent<SpriteRenderer>().color = fadeA;
        }

        if (_fadeW.GetComponent<SpriteRenderer>().color.a > 0)
        {
            Color fadeA = _fadeW.GetComponent<SpriteRenderer>().color;
            fadeA.a -= Time.deltaTime;
            _fadeW.GetComponent<SpriteRenderer>().color = fadeA;
        }

        if (_dark == true)
        {
            _darking = true;
            _fire.SetActive(false);
        }

        if (_darking == true)
        {
            Color fadeA = _fade.GetComponent<SpriteRenderer>().color;
            fadeA.a += Time.deltaTime;
            _fade.GetComponent<SpriteRenderer>().color = fadeA;

            if (_fade.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                _dark = false;
                _mode2.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
