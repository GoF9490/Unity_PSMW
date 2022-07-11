using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_circle1 : MonoBehaviour
{
    public float _speed = 1;

    private void Start()
    {
        StartCoroutine(bright());
    }

    IEnumerator bright()
    {
        yield return null;
        Color _sprite = GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a += Time.deltaTime * _speed;

            GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a >= 1)
            {
                _sprite.a = 1;
                GetComponent<SpriteRenderer>().color = _sprite;

                break;
            }
            yield return null;
        }
    }
}
