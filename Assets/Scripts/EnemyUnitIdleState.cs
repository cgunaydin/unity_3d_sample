using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitIdleState : StateMachineBehaviour
{
    EnemyAttackController attackController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<EnemyAttackController>();
        attackController.SetIdleMaterial();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if there is an available target
        if (attackController.targetToAttack != null)
        {
            //transition to follow state
            animator.SetBool("isFollowing", true);
        }
    }
}
