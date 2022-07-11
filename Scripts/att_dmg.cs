using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class att_dmg : MonoBehaviour
{
    public float Damage = 0;

    public bool end = false;
    public bool projectile = false;
    public bool coloff = false;

    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("shero1");
        Damage = Damage * player.GetComponent<playerCon>()._soul;
    }

    private void Update()
    {
        if (end == true)
        {
            Destroy(gameObject);
        }

        if (player.GetComponent<playerPhy>()._cancel == true && projectile == false)
        {
            Destroy(gameObject);
        }

        if (coloff == false && GetComponent<BoxCollider2D>().enabled == false)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
