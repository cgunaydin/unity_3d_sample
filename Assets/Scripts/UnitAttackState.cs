using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    UnitAttackController attackController;

    public float stopAttackingDistance = 1.2f;

    public float attackRate = 2f;
    public float attackTimer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        attackController = animator.GetComponent<UnitAttackController>();
        attackController.SetAttackMaterial();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attackController.targetToAttack != null && !animator.transform.GetComponent<UnitMovement>().isCommandedToMove)
        {
            LookAtTarget();

            //Keep moving towards enemy
            agent.SetDestination(attackController.targetToAttack.transform.position);

            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = 1f / attackRate;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

            //Should unit still attack
            float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.transform.position, animator.transform.position);
            if (distanceFromTarget > stopAttackingDistance || attackController.targetToAttack == null)
            {
                agent.SetDestination(animator.transform.position);
                animator.SetBool("isAttacking", false); //Stop from attacking state
            }
        }
        else
        {
            animator.SetBool("isAttacking", false); //Stop from attacking state
        }
    }

    private void Attack()
    {
        var damageToInflict = attackController.unitDamage;

        attackController.targetToAttack.GetComponent<EnemyUnit>().TakeDamage(damageToInflict);
    }

    private void LookAtTarget()
    {
        Vector3 direction = attackController.targetToAttack.transform.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
