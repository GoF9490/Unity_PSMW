using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobcol : MonoBehaviour
{
    public bool touch;
    public bool left;
    public bool right;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("boss"))
        {
            touch = true;

            if (collision.transform.position.x > transform.position.x)
            {
                left = true;
                right = false;
            }
            else

            if (collision.transform.position.x < transform.position.x)
            {
                left = false;
                right = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("boss"))
        {
            touch = false;
            left = false;
            right = false;
        }
    }
}
