using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha_trigger2 : MonoBehaviour
{
    N_Cha_Controller chacon;
    Rigidbody2D rb;

    private void Start()
    {
        chacon = GetComponentInParent<N_Cha_Controller>();
        rb = GetComponentInParent<Rigidbody2D>();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {


            gameObject.transform.parent.GetComponent<BoxCollider2D>().isTrigger = false;

        }

        if (collision.gameObject.tag.Equals("Ground2"))
        {


            gameObject.transform.parent.GetComponent<BoxCollider2D>().isTrigger = false;

        }
    }
}
