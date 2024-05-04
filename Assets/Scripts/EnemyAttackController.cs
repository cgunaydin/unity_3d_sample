using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : AttackControllerBase
{
    public GameObject targetToAttack;
    public List<GameObject> targetsInRange;

    public int unitDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (SetTarget("PlayerCollider", other, ref targetToAttack, ref targetsInRange))
        {
            gameObject.transform.GetComponent<SimplePatrol>().enemySpotted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(RemoveTarget("PlayerCollider", other, ref targetToAttack, ref targetsInRange))
        {
            gameObject.transform.GetComponent<SimplePatrol>().enemySpotted = false;
        }
    }
}
