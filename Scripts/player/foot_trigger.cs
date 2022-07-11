using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foot_trigger : MonoBehaviour
{
    Rigidbody2D rb;
    playerPhy _pp;
    public bool ne = false;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        _pp = GetComponentInParent<playerPhy>();
    }

    private void Update()
    {
        if (_pp._jump == true)
        {
            ne = true;
        }

        if (_pp._jump == false)
        {
            ne = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //점프 상태 초기화
        if (collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("Ground2"))
        {
            if (ne == true && rb.velocity.y <= 0)
            {
                _pp._jump = false;
                ne = false;
                _pp.Land();
            }
        }
    }
}
