using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss_1_Pt : MonoBehaviour
{
    public bool start = false;
    public bool up = false;
    public bool down = false;
    public Transform tf; 



    private void Start()
    {
        tf = GetComponent<Transform>();
        StartCoroutine(start_pt());
    }

    private void FixedUpdate()
    {
        if (start == true)
        {
            if (up == false)
            {
                transform.Translate(Vector2.up * Time.deltaTime * 50f);

            }

            if (tf.position.y > 4f)
            {
                up = true;
                tf.position = new Vector2(tf.position.x, 4f);
                StartCoroutine(Att_End());

            }

            if (down == true)
            {
                transform.Translate(Vector2.down * Time.deltaTime * 70f);

            }

            if (tf.position.y < -4f)
            {
                Destroy(this.gameObject);

            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<N_Cha_Controller>().Cha_Invulnerable == false)
        {
            collision.GetComponent<N_Cha_Controller>().Cha_Life = collision.GetComponent<N_Cha_Controller>().Cha_Life - 10;

        }

    }

    IEnumerator start_pt()
    {
        yield return new WaitForSeconds(1f);
        start = true;
    }

    IEnumerator Att_End()
    {
        yield return new WaitForSeconds(0.2f);
        down = true;

    }

}
