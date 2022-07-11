using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialP : MonoBehaviour
{
    public Animator _animator;
    public GameObject _tut;
    public GameObject _tut2;
    public GameObject _tutM;
    public int _count = 0;

    public bool end;

    private void Start()
    {
        StartCoroutine(move());
    }


    IEnumerator move()
    {
        if (_count == 14)
        {
            _tutM = GameObject.Find("tutManager");
            _tutM.GetComponent<tutManager>().MoveEnd();
        }

        if (_count < 17)
        {
            yield return new WaitForSeconds(1.7f);
            GameObject tut1 = (GameObject)Instantiate(_tut, new Vector3(transform.position.x + 0.19f, transform.position.y, -1), Quaternion.identity);
            tut1.GetComponent<tutorialP>()._count = _count + 1;

            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);
        }

        if (_count == 17)
        {
            yield return new WaitForSeconds(1.7f);
            GameObject tut1 = (GameObject)Instantiate(_tut2, new Vector3(transform.position.x + 0.19f, transform.position.y, -1), Quaternion.identity);
            tut1.name = "tutorialP2";

            yield return new WaitForSeconds(0.05f);
            Destroy(gameObject);

        }
    }
}
