using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutManager : MonoBehaviour
{
    public GameObject _cam;
    public GameObject _fade;
    public GameObject _fadeW;
    public GameObject _text;
    public GameObject _choicetext;
    public GameObject _choiceUI;
    public GameObject _Psoul;
    public GameObject _cloud;
    public GameObject _cui;

    public bool _moveEnd;

    private void Awake()
    {
        StartCoroutine(camMove());
        StartCoroutine(fadein());
    }

    public void MoveEnd()
    {
        _moveEnd = true;
        GManager.instance.GetComponent<GManager>().Cam_size(7, 0.2f);
        GManager.instance.GetComponent<GManager>().Cam_move(new Vector3(10, 7, -10), 0.2f);
    }

    public void Event1()
    {
        GameObject _text1 = (GameObject)Instantiate(_text, transform.position, Quaternion.identity);
        _text1.GetComponent<textManager>()._tut1 = true;
    }

    public void Event2()
    {
        StartCoroutine(event2());
    }

    public void Event3()
    {
        GameObject _text1 = (GameObject)Instantiate(_text, transform.position, Quaternion.identity);
        _text1.GetComponent<textManager>()._tut2 = true;
    }

    public void Event4()
    {
        Color _sprite = _fadeW.GetComponent<SpriteRenderer>().color;
        _sprite.a = 1;
        _fadeW.GetComponent<SpriteRenderer>().color = _sprite;
        StartCoroutine(fadeinW(0.5f, 0.05f));
        GameObject.Find("tutorialP3").GetComponent<tutorialP3>().Burning();
        Destroy(_cloud);
        GameObject.Find("amad_aurora").SetActive(false);
    }

    public void cui()
    {
        _cui.SetActive(true);
    }

    IEnumerator camMove()
    {
        float count = 0;

        while (true)
        {
            count += Time.deltaTime * 0.0002f;
            Vector3 toPos = new Vector3(0, 3, -10);

            _cam.transform.position = Vector3.Lerp(_cam.transform.position, toPos, count);

            if (_moveEnd == true)
            {
                break;
            }
            yield return null;
        }
    }

    IEnumerator fadein()
    {
        Color _sprite = _fade.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a -= Time.deltaTime * 0.1f;
            _fade.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a <= 0.5f)
            {
                break;
            }
            yield return null;
        }

        yield return null;

        while (true)
        {
            _sprite.a -= Time.deltaTime * 0.025f;
            _fade.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a <= 0.05f)
            {
                _sprite.a = 0;
                _fade.GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }

    IEnumerator fadeout(float speed, float alp)
    {
        yield return null;
        Color _sprite = _fade.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a += Time.deltaTime * speed;
            _fade.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a >= alp)
            {
                _sprite.a = alp;
                _fade.GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }

    IEnumerator fadeinW(float speed, float alp)
    {
        yield return null;
        Color _sprite = _fadeW.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a -= Time.deltaTime * speed;
            _fadeW.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a <= alp)
            {
                _sprite.a = 0;
                _fadeW.GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }

    IEnumerator event2()
    {
        yield return StartCoroutine(fadeout(0.5f, 0.5f));
        yield return new WaitForSeconds(1f);
        _choicetext.SetActive(true);

        while (true)
        {
            if (GManager.instance.GetComponent<GManager>().choice_true == true)
            {
                _Psoul.GetComponent<tut_soul>().Soul();
                break;
            }
            else

            if (GManager.instance.GetComponent<GManager>().choice_false == true)
            {
                SceneManager.LoadScene("GameOver");
                break;
            }
            yield return null;
        }
    }
}
