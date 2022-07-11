using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public Rigidbody2D rb;
    public float M_hp = 10.0f;
    public bool Att_t = false;

    public bool cooldown = false;
    public bool Mob_Right = false;
    public bool Mob_Such = false;
    public bool Mob_Move = true;
    public GameObject player;


    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        

    }

    void Update()
    {
        //플레이어 인식
        if (player.GetComponent<Rigidbody2D>().transform.position.x - rb.transform.position.x > -10f && player.GetComponent<Rigidbody2D>().transform.position.x - rb.transform.position.x < 10f)
        {

            Mob_Such = true;

        }

        //좌우 방향
        if (Mob_Such == true)
        {
            if (player.GetComponent<Rigidbody2D>().transform.position.x < rb.transform.position.x)
            {
                Mob_Right = false;

            }
            else

            if (player.GetComponent<Rigidbody2D>().transform.position.x > rb.transform.position.x)
            {
                Mob_Right = true;

            }

        }

        //몹 이동
        if (Mob_Move == true && cooldown == false)
        {
            Mob_Move = false;

            if (Mob_Such == false)
            {

                float Ran_Move = Random.Range(-1f, 1f);

                if (Ran_Move > 0f)
                {
                    Mob_Right = true;

                }
                else

                if (Ran_Move < 0f)
                {
                    Mob_Right = false;

                }

            }

            if (Mob_Right == true)
            {
                rb.AddForce(new Vector2(400f, 0f));

            } else

            if (Mob_Right == false)
            {
                rb.AddForce(new Vector2(-400f, 0f));

            }

            StartCoroutine(MoveCool());

        }


        //몹 제거
        if(M_hp <= 0f)
        {
            Destroy(this.gameObject);
            
            
        }

        //몹 공격
        if (cooldown == false && Mob_Right == false)
        {



            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 5f, 1 << LayerMask.NameToLayer("Player"));

            Debug.DrawRay(transform.position, Vector2.left * 5f, Color.red);

            if (hit)
            {

                cooldown = true;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2( 500f, 0f));
                StartCoroutine(BodyAttackL());



            }

        } else

        if (cooldown == false && Mob_Right == true)
        {



            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.left, 5f, 1 << LayerMask.NameToLayer("Player"));

            Debug.DrawRay(transform.position, -Vector2.left * 5f, Color.red);

            if (hit)
            {

                cooldown = true;
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-500f, 0f));
                StartCoroutine(BodyAttackR());



            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Att") && Att_t == false)
        {

            rb.velocity = new Vector2(0, 0);
            rb.AddForce(transform.up * 500f);
            Att_t = true;
            M_hp =M_hp - other.gameObject.GetComponent<att_dmg>().Damage;
            
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Att_End"))
        {
            Att_t = false;
        }
    }

    //IEnumerater
    IEnumerator BodyAttackL()
    {
        yield return new WaitForSeconds(0.8f);

        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(-800f, 800f));

        StartCoroutine(Cooldown());

    }

    IEnumerator BodyAttackR()
    {
        yield return new WaitForSeconds(0.8f);

        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(800f, 800f));

        StartCoroutine(Cooldown());

    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        cooldown = false;

    }

    IEnumerator MoveCool()
    {
        yield return new WaitForSeconds(3f);
        Mob_Move = true;

    }



}
