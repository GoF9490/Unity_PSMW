using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public GameObject C1;
    public GameObject C2;
    public GameObject C3;

    private void Start()
    {
        StartCoroutine(magic());
    }

    IEnumerator magic()
    {
        yield return new WaitForSeconds(1);
        GameObject cir1 = GameObject.Instantiate(C1, new Vector3(transform.position.x + 1, transform.position.y), Quaternion.identity);

        yield return new WaitForSeconds(2);
        GameObject cir2 = GameObject.Instantiate(C2, new Vector3(transform.position.x + 1, transform.position.y), Quaternion.identity);

        yield return new WaitForSeconds(2);
        GameObject cir3 = GameObject.Instantiate(C3, new Vector3(transform.position.x + 1, transform.position.y), Quaternion.identity);

    }
}
