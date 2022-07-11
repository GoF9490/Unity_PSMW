using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha_trigger : MonoBehaviour
{
    playerPhy _pp;
    Rigidbody2D rb;

    private void Start()
    {
        _pp = GetComponentInParent<playerPhy>();
        rb = GetComponentInParent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            if (_pp._jump == true && rb.velocity.y >= 0f)
            {
                gameObject.transform.parent.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }

        if (collision.gameObject.tag.Equals("Ground2"))
        {
            if (_pp._jump == true && rb.velocity.y >= -40f)
            {
                gameObject.transform.parent.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {


            //gameObject.transform.parent.GetComponent<CapsuleCollider2D>().isTrigger = false;

        }

        if (collision.gameObject.tag.Equals("Ground2"))
        {


            gameObject.transform.parent.GetComponent<CapsuleCollider2D>().isTrigger = false;

        }
    }
}
