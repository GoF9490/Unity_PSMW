using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tut_end : MonoBehaviour
{
    public GameObject _fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            StartCoroutine(fadeout(0.5f, 1));
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

            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Test");
        }
    }
}
