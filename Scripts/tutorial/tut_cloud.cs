using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut_cloud : MonoBehaviour
{
    public float pos = 23;
    public float f_pos = 0;
    
    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 0.8f);

        if(transform.position.x > pos)
        {
            transform.position = new Vector3(f_pos, transform.position.y, -8f);
        }
    }
}
