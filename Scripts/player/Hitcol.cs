using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitcol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("dot"))
        {
            GetComponentInParent<playerCon>()._dot = true;
            int damage = collision.gameObject.GetComponent<eavalue>().damage;
            float interval = collision.gameObject.GetComponent<eavalue>().interval;
            GetComponentInParent<playerCon>().Dot(damage, interval);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("dot") && GetComponentInParent<playerCon>()._dot == true)
        {
            GetComponentInParent<playerCon>()._dot = false;
        }
    }
}
