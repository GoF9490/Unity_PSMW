using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowL : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Att_End());
    }

    private void Update()
    {
        transform.Translate(-Vector2.right * Time.deltaTime * 40f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ground2") || collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("EnemyAtt"))
        {
            StartCoroutine(attend());
        }
    }

    IEnumerator Att_End()
    {

        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }

    IEnumerator attend()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }


}
