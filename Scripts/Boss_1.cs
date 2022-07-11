using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss_1 : MonoBehaviour
{

    public GameObject Player;
    public GameObject Turret;
    public GameObject Root;
    public GameObject Seed;
    public float Att;

    public Rigidbody2D rb;
    public bool Pat_Cool = false;
    public int Patton = 0;

    public float Life = 600f;

    public Slider hpslider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Patton = Random.Range(1, 3);

    }

    private void Update()
    {
        if (Pat_Cool == false && Patton == 1)
        {
            Pat_Cool = true;
            StartCoroutine(Root_Patton());
            //Player.GetComponent<Transform>().position.x

        } else

        if (Pat_Cool == false && Patton == 2)
        {
            Pat_Cool = true;
            StartCoroutine(Seed_Patton());

        }

        hpslider.value = Life;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Att = 0f;

        if (other.gameObject.tag.Equals("Att") )
        {

            //rb.velocity = new Vector2(0, 0);
            //rb.AddForce(transform.up * 500f);
            //Att_t = true;
            Att = other.gameObject.GetComponent<att_dmg>().Damage;
            Debug.Log(Att);
            Life = Life - Att;

        } //else

        if (other.gameObject.tag.Equals("SoulAtt")) 
        {
            Att = other.gameObject.GetComponent<att_dmg>().Damage * DataController.instance.GetComponent<DataController>().playerData.soul;
            Debug.Log(Att);
            Life = Life - Att;
        }

    }

    

    IEnumerator Root_Patton()
    {
        yield return new WaitForSeconds(2f);
        GameObject root1 = (GameObject)Instantiate(Root, new Vector2(Player.GetComponent<Transform>().position.x, -3), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        GameObject root2 = (GameObject)Instantiate(Root, new Vector2(Player.GetComponent<Transform>().position.x, -3), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        GameObject root3 = (GameObject)Instantiate(Root, new Vector2(Player.GetComponent<Transform>().position.x, -3), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Patton = Random.Range(1, 3);
        Pat_Cool = false;

    }

    IEnumerator Seed_Patton()
    {
        yield return new WaitForSeconds(2f);
        GameObject seed1 = (GameObject)Instantiate(Seed, Turret.GetComponent<Transform>().position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        GameObject seed2 = (GameObject)Instantiate(Seed, Turret.GetComponent<Transform>().position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        GameObject seed3 = (GameObject)Instantiate(Seed, Turret.GetComponent<Transform>().position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        GameObject seed4 = (GameObject)Instantiate(Seed, Turret.GetComponent<Transform>().position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Patton = Random.Range(1, 3);
        Pat_Cool = false;

    }
}
