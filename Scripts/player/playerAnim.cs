using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    public Animator anim;
    public playerPhy _pp;

    private void Awake()
    {
        _pp = GetComponent<playerPhy>();
    }

    private void Update()
    {
        if (_pp._delay == false)
        {
            if (_pp._jump == false && _pp._move == false)// && _pp._delay == false)
            {
                anim.SetBool("Att_end", false);
                anim.SetBool("move", false);
                anim.SetBool("stay", true);
            }
            else

            if (_pp._jump == false && _pp._move == true)// && _pp._delay == false)
            {
                anim.SetBool("stay", false);
                anim.SetBool("move", true);
            }
            else

            {
                anim.SetBool("stay", false);
                anim.SetBool("move", false);
            }
        }

        if (_pp._jump == true)
        {
            anim.SetBool("jump", true);

            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                anim.SetBool("fall", true);
            }
            else

            if (GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                anim.SetBool("fall", false);
            }
        }
        else

        if (_pp._jump == false)
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
            anim.SetBool("J_dash", false);
        }

    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Att1()
    {
        anim.SetBool("Att_end", false);
        anim.SetBool("att1", true);
    }

    public void Att2()
    {
        anim.SetBool("att1", false);
        anim.SetBool("att2", true);
    }

    public void Att3()
    {
        anim.SetBool("att1", false);
        anim.SetBool("att2", false);
        anim.SetBool("att3", true);
    }

    public void Att_end()
    {
        anim.SetBool("att1", false);
        anim.SetBool("att2", false);
        anim.SetBool("att3", false);
        anim.SetBool("Att_end", true);
        anim.SetBool("stay", true);
    }

    public void _stay()
    {
        anim.SetBool("dodge_end", false);
        anim.SetBool("stay", true);
    }

    public void Dodge()
    {
        anim.SetBool("stay", false);
        anim.SetBool("move", false);
        anim.SetBool("jump", false);
        anim.SetBool("fall", false);
        anim.SetBool("Att_end", false);
        anim.SetBool("att1", false);
        anim.SetBool("att2", false);
        anim.SetBool("att3", false);
        anim.SetTrigger("dodge");
    }

    public void Dodge_e()
    {
        anim.SetBool("dodge_end", true);
    }

    public void J_att()
    {
        anim.SetBool("J_att", true);
    }

    public void J_dash()
    {
        anim.SetBool("J_dash", true);
    }

    public void J_att_end()
    {
        anim.SetBool("J_att", false);
    }

    public void hit1()
    {
        anim.SetBool("stay", false);
        anim.SetBool("hit1", true);
    }

    public void _Reset()
    {
        anim.Rebind();
    }
}
