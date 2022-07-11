using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tut_ctxt : MonoBehaviour
{
    public GameObject _choiceUI;

    public Text _text;

    public Color _sprite;

    public bool up = false;

    public void Awake()
    {
        _sprite = _text.color;
        StartCoroutine(bright());
    }

    IEnumerator bright()
    {
        while (true)
        {
            _sprite.a += Time.deltaTime * 0.8f;
            _text.color = _sprite;

            if (_sprite.a >= 1)
            {
                _sprite.a = 1;
                _text.color = _sprite;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(2);

        while (true)
        {
            _sprite.a -= Time.deltaTime * 0.8f;
            _text.color = _sprite;

            if (_sprite.a <= 0)
            {
                _sprite.a = 0;
                _text.color = _sprite;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1);
        _choiceUI.SetActive(true);
        Destroy(gameObject);
    }
}
