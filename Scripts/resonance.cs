using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resonance : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        StartCoroutine(boom());
    }

    IEnumerator boom()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
