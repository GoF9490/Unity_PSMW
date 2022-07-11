using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assassin : MonoBehaviour
{
    public bool DirL = false;
    public GameObject knifeR;
    public GameObject knifeL;
    public GameObject player;
    public Rigidbody2D rb;
    IEnumerator _coroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("shero1");
        if (player.GetComponent<N_Cha_Controller>().CD == Cha_Direction.Left)
        {
            DirL = true;
        }
        _coroutine = att();
        StartCoroutine(_coroutine);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy_Att"))
        {
            StopCoroutine(_coroutine);
            //Player.GetComponent<N_Cha_Controller>().Archer_Count = false;
            Destroy(this.gameObject);

        }
    }

    IEnumerator att()
    {
        yield return new WaitForSeconds(1f);
        //
        yield return new WaitForSeconds(0.3f);
        if (DirL == false)
        {
            GameObject knifer = (GameObject)Instantiate(knifeR, GetComponent<Transform>().position, Quaternion.identity);
        } else

        if (DirL == true)
        {
            GameObject knifel = (GameObject)Instantiate(knifeL, GetComponent<Transform>().position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
