using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject Player;
    public float dirValue;
    Rigidbody2D rb;


    

    private void Start()
    {

        Player = GameObject.Find("shero1");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot());


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && Player.GetComponent<N_Cha_Controller>().Cha_Invulnerable == false)
        {
            Player.GetComponent<N_Cha_Controller>().Cha_Life = Player.GetComponent<N_Cha_Controller>().Cha_Life - 10;
            Destroy(gameObject);

        }

    }

    IEnumerator Shoot()
    {
        Vector2 pos;
        pos = Player.GetComponent<Transform>().position - transform.position;

        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        //transform.Rotate(0, 0, angle); // 회전

        yield return new WaitForSeconds(1f);
        rb.velocity = pos.normalized * 50f; // 발사
        StartCoroutine(End());

    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }

}
