using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut_choiceUI : MonoBehaviour
{
    public GameObject yes;
    public GameObject no;

    public Color _sprite;

    public bool _activation = false;
    public bool _yes = true;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>().color;
        _sprite.a = 0;
        StartCoroutine(activation());
    }

    void reboot()
    {
        _sprite.a = 0;
        _yes = true;
        _activation = false;
        StartCoroutine(activation());
    }

    private void Update()
    {
        if (_activation == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _yes = true;
            }
            else
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _yes = false;
            }

            if (_yes == true)
            {
                yes.SetActive(true);
                yes.GetComponent<SpriteRenderer>().color = _sprite;
                no.SetActive(false);
            }
            else

            if (_yes == false)
            {
                yes.SetActive(false);
                no.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _activation = false;

                if (_yes == true)
                {
                    GManager.instance.GetComponent<GManager>().choice_true = true;
                }
                else

                if (_yes == false)
                {
                    GManager.instance.GetComponent<GManager>().choice_false = true;
                }

                GManager.instance.GetComponent<GManager>().Fadein(1, 0);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator activation()
    {
        while (true)
        {
            _sprite.a += Time.deltaTime * 0.8f;
            GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a >= 1)
            {
                _sprite.a = 1;
                GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1);
        _activation = true;
    }
}
