using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut_soul : MonoBehaviour
{
    public GameObject tut_manager;

    public bool enter = false;

    private void Update()
    {
        if (transform.localScale.x > 0.05 && enter == true)
        {
            transform.localScale -= new Vector3(0.02f, 0.02f, 0);
        }
        else

        if (transform.localScale.x <= 0.05 && enter == true)
        {
            tut_manager.GetComponent<tutManager>().Event3();
            Destroy(gameObject);
        }
    }

    public void Soul()
    {
        StartCoroutine(soul());
    }

    IEnumerator soul()
    {
        yield return StartCoroutine(fadeout(1, 1));
        yield return StartCoroutine(fadein(1, 0.5f));
        enter = true;
        yield return StartCoroutine(fadeout(0.2f, 1));
    }

    IEnumerator fadeout(float speed, float alp)
    {
        yield return null;
        Color _sprite = GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a += Time.deltaTime * speed;
            GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a >= alp)
            {
                _sprite.a = alp;
                GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }

    IEnumerator fadein(float speed, float alp)
    {
        yield return null;
        Color _sprite = GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a -= Time.deltaTime * speed;
            GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a <= alp)
            {
                _sprite.a = alp;
                GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }
}
