using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject Player;
    public GameObject crackboom;
    public float dirValue;
    Rigidbody2D rb;

    public void Shoot()
    {
        Player = GameObject.Find("shero1");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(shoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            GameObject crack1 = (GameObject)Instantiate(crackboom, new Vector2(transform.position.x, 0), Quaternion.identity);
            Destroy(gameObject);
        }
        else

        if (collision.gameObject.tag.Equals("Wall"))
        {
            if (transform.position.x < 0)
            {
                GameObject crack1 = (GameObject)Instantiate(crackboom, new Vector2(-20, transform.position.y), Quaternion.FromToRotation(Vector2.up, Vector2.right));
                Destroy(gameObject);
            }
            else

            if (transform.position.x > 0)
            {
                GameObject crack1 = (GameObject)Instantiate(crackboom, new Vector2(20, transform.position.y), Quaternion.FromToRotation(Vector2.up, Vector2.left));
                Destroy(gameObject);
            }
        }
    }

    IEnumerator shoot()
    {
        Vector2 pos;
        pos = Player.GetComponent<Transform>().position - transform.position;

        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        transform.Rotate(0, 0, angle + 90); // 회전

        yield return new WaitForSeconds(1f);
        rb.velocity = pos.normalized * 50f; // 발사

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
