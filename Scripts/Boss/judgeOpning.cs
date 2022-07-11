using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judgeOpning : MonoBehaviour
{
    public GameObject _judge;
    public GameObject _player;
    public GameObject _fade;
    public GameObject _playerhp;
    public GameObject _bosshp;

    private void Awake()
    {
        StartCoroutine(opning());
    }

    IEnumerator opning()
    {
        yield return StartCoroutine(fadein(0.5f, 0.05f));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(fadeout(0.7f, 1f));
        _judge.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(fadein(0.3f, 0.05f));
        yield return new WaitForSeconds(0.5f);
        _playerhp.SetActive(true);
        _bosshp.SetActive(true);
        _player.GetComponent<playerPhy>().enabled = true;
        _judge.GetComponent<judgement>()._delay = false;
        Destroy(gameObject);
    }

    public IEnumerator fadein(float speed, float alp)
    {
        yield return null;
        Color _sprite = _fade.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a -= Time.deltaTime * speed;
            _fade.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a <= alp)
            {
                _sprite.a = 0;
                _fade.GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }

    public IEnumerator fadeout(float speed, float alp)
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
}
