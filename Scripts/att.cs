using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class att : StateMachineBehaviour
{
    public float Damage = 2.0f;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.tag = "Att_End";
        base.OnStateExit(animator, stateInfo, layerIndex);
        Destroy(animator.gameObject);
    }
}
