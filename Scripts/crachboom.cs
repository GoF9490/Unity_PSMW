using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crachboom : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(end());
    }

    IEnumerator end()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
