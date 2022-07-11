using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public bool Cool = false;
    public GameObject arrow;
    public GameObject Player;
    public Rigidbody2D rb;
    IEnumerator _coroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("shero1");

    }

    private void Update()
    {
        if (Cool == false)
        {
            Cool = true;
            _coroutine = Arrow();
            StartCoroutine(_coroutine);

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ground2"))
        {
            rb.velocity = new Vector2(0, 0);
            transform.position = new Vector2(rb.position.x, collision.gameObject.GetComponent<Rigidbody2D>().position.y + 1.7f);
            rb.gravityScale = 0f;

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy_Att"))
        {
            StopCoroutine(_coroutine);
            Player.GetComponent<N_Cha_Controller>().Archer_Count = false;
            Destroy(this.gameObject);

        }

    }

    IEnumerator Arrow()
    {
        yield return new WaitForSeconds(3f);
        GameObject arrow1 = (GameObject)Instantiate(arrow, GetComponent<Transform>().position, Quaternion.identity);
        Cool = false;

    }




}
