using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigball : MonoBehaviour
{
    public GameObject jj;
    public bool ll = false;
    public bool rr = false;

    private void Start()
    {
        jj = GameObject.Find("judgement");

        if (transform.position.x > 0)
        {
            StartCoroutine(moveL());
        }
        else

        if (transform.position.x < 0)
        {
            StartCoroutine(moveR());
        }
    }

    private void Update()
    {
        if (ll == true)
        {
            transform.Translate(Vector2.left * Time.deltaTime * 60);
        }
        else

        if (rr == true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 60);
        }
    }

    IEnumerator moveL()
    {
        yield return new WaitForSeconds(0.5f);
        ll = true;

        yield return new WaitForSeconds(1f);
        jj.GetComponent<judgement>().Recall();
        Destroy(gameObject);

    }

    IEnumerator moveR()
    {
        yield return new WaitForSeconds(0.5f);
        rr = true;

        yield return new WaitForSeconds(1f);
        jj.GetComponent<judgement>().Recall();
        Destroy(gameObject);
    }
}

