using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class att2 : MonoBehaviour
{
    
    public float Damage = 2.0f;
    public float OOO = 0f;

    void Start()
    {
        //this.gameObject.tag = "Att_End";
        transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
        //Invoke("ATT_EFF", 0.3f);
        StartCoroutine(Att_Eff());

    }

    //    void ATT_EFF()
    //   {
    //       this.gameObject.tag = "Att_End";
    //      StartCoroutine(Att_Eff());
    //
    //   }

    


    IEnumerator Att_Eff()
    {

        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);

    }
    
}


