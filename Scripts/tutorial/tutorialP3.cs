using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialP3 : MonoBehaviour
{
    public GameObject cui;

    private void Start()
    {
        Invoke("E4", 5);
    }

    void E4()
    {
        GameObject.Find("tutManager").GetComponent<tutManager>().Event4();
    }

    public void Burning()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(burning());
    }

    IEnumerator burning()
    {
        GameObject player = GameObject.Find("shero1");
        player.transform.position = new Vector3(transform.position.x, 3, -1);
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(3f);
        GameObject.Find("tutManager").GetComponent<tutManager>().cui();
        player.GetComponent<playerCon>().enabled = true;
        player.GetComponent<playerPhy>().enabled = true;
        player.GetComponent<playerPhy>()._delay = false;
        player.GetComponent<playerPhy>()._cancel = false;
        player.GetComponent<playerCon>()._lookatme = true;
        Camera.main.GetComponent<Camera>().orthographicSize = 7;
    }
}
