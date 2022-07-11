using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yes_bright : MonoBehaviour
{
    public Color _sprite;

    public bool up = false;

    public void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>().color;
    }

    public void Update()
    {
        if (_sprite.a >= 1)
        {
            up = false;
        }

        if (_sprite.a <= 0.7f)
        {
            up = true;
        }

        if (up == false)
        {
            _sprite.a -= Time.deltaTime * 0.3f;
            GetComponent<SpriteRenderer>().color = _sprite;
        }
        else

        if (up == true)
        {
            _sprite.a += Time.deltaTime * 0.3f;
            GetComponent<SpriteRenderer>().color = _sprite;
        }
    }
}
