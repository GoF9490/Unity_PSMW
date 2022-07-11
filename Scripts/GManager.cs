using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public GameObject player;
    public GameObject cam;
    public GameObject fade;

    public bool txt_end = false;
    public bool choice_true = false;
    public bool choice_false = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player = GameObject.Find("shero1");
        cam = GameObject.Find("Main Camera");
        fade = GameObject.Find("fade");
    }

    private void Update()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Main Camera");
        }

        if (player == null)
        {
            player = GameObject.Find("shero1");
        }
    }

    public void Cam_size(int camrange, float _speed)
    {
        StartCoroutine(cam_size2(camrange, _speed));
    }

    public void Cam_move(Vector3 toPos, float _speed)
    {
        if (player != null)
        {
            player.GetComponent<playerCon>()._lookatme = false;
        }
        StartCoroutine(cam_move(toPos, _speed));
    }

    public void Fadein(float speed, float alp)
    {
        StartCoroutine(fadein(speed, alp));
    }

    public void Fadeout(float speed, float alp)
    {
        StartCoroutine(fadeout(speed, alp));
    }

    public void Cam_return()
    {
        if (player != null)
        {
            player.GetComponent<playerCon>()._lookatme = true;
            cam.GetComponent<Camera>().orthographicSize = 7;
        }
    }

    IEnumerator cam_size(int camrange)
    {
        while (true)
        {
            cam.GetComponent<Camera>().orthographicSize += Time.deltaTime * 5;

            if (cam.GetComponent<Camera>().orthographicSize > camrange)
            {
                cam.GetComponent<Camera>().orthographicSize = camrange;

                break;
            }
            yield return null;
        }
    }

    IEnumerator cam_size2(int camrange, float _speed)
    {
        float count = 0;
        
        while (true)
        {
            count += Time.deltaTime * _speed;
            float _this = cam.GetComponent<Camera>().orthographicSize;

            cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(_this, camrange, count);

            if (count >= 1)
            {
                cam.GetComponent<Camera>().orthographicSize = camrange;

                break;
            }
            yield return null;
        }
    }

    IEnumerator cam_move(Vector3 toPos, float _speed)
    {
        float count = 0;

        while (true)
        {
            count += Time.deltaTime * _speed;

            cam.transform.position = Vector3.Lerp(cam.transform.position, toPos, count);

            if (count >= 1)
            {
                cam.transform.position = toPos;

                break;
            }
            yield return null;
        }
    }

    IEnumerator fadeout(float speed, float alp)
    {
        yield return null;
        Color _sprite = fade.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a += Time.deltaTime * speed;
            fade.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a >= alp)
            {
                _sprite.a = alp;
                fade.GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }

    IEnumerator fadein(float speed, float alp)
    {
        yield return null;
        Color _sprite = fade.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            _sprite.a -= Time.deltaTime * speed;
            fade.GetComponent<SpriteRenderer>().color = _sprite;

            if (_sprite.a <= alp)
            {
                _sprite.a = alp;
                fade.GetComponent<SpriteRenderer>().color = _sprite;
                break;
            }
            yield return null;
        }
    }
}
