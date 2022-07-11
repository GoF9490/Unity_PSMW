using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialP2 : MonoBehaviour
{
    public Animator _animator;
    public GameObject _tutM;
    public GameObject _burn;


    private void Awake()
    {
        _tutM = GameObject.Find("tutManager");
        StartCoroutine(breath());
    }

    public void _Down()
    {
        StartCoroutine(_down());
    }

    public void Burning()
    {
        GameObject burn1 = GameObject.Instantiate(_burn, transform.position, Quaternion.identity);
        burn1.name = "tutorialP3";
        Destroy(gameObject);
    }

    IEnumerator breath()
    {
        yield return new WaitForSeconds(2);
        _animator.SetBool("breath", true);
        _tutM.GetComponent<tutManager>().Event1();
    }

    IEnumerator _down()
    {
        yield return new WaitForSeconds(2);
        _animator.SetBool("down", true);

        yield return new WaitForSeconds(2);
        _tutM.GetComponent<tutManager>().Event2();
    }
}
