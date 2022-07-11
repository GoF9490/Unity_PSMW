using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cha_Anim : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.GetComponentInParent<N_Cha_Controller>().CD == Cha_Direction.Left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else

        if (gameObject.GetComponentInParent<N_Cha_Controller>().CD == Cha_Direction.Right)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
