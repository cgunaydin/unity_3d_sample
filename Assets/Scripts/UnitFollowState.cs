using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    UnitAttackController attackController;
    NavMeshAgent agent;
    public float attackingDistance = 1.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.transform.GetComponent<UnitAttackController>();
        agent = animator.transform.GetComponent<NavMeshAgent>();

        attackController.SetFollowMaterial();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Idle State'e dönmeli mi
        if (attackController.targetToAttack == null && !attackController.targetsInRange.Any(x => x != null))
        {
            animator.SetBool("isFollowing", false); 
        }
        else
        {
            if (attackController.targetToAttack == null)
            {
                attackController.targetToAttack = attackController.targetsInRange.Where(x => x != null).First();
            }

            //if there is no other direct command to move
            if (!animator.transform.GetComponent<UnitMovement>().isCommandedToMove)
            {
                //Enemy'e doðru yürü
                agent.SetDestination(attackController.targetToAttack.transform.position);
                animator.transform.LookAt(attackController.targetToAttack.transform);

                //Attack State'e dönmeli mi
                float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.transform.position, animator.transform.position);
                if (distanceFromTarget < attackingDistance)
                {
                    agent.SetDestination(animator.transform.position);
                    animator.SetBool("isAttacking", true); //Move to attacking state
                }
            }
        }
    }

    //select new target için distance hesabý yapýlabilir.
    private Transform SelectNewTarget()
    {
        //Remove null objects

        for (int i = attackController.targetsInRange.Count - 1; i >= 0; i--)
        {
            if (attackController.targetsInRange[i] == null)
            {
                attackController.targetsInRange.RemoveAt(i);
            }
        }

        return attackController.targetsInRange.First().transform;

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
