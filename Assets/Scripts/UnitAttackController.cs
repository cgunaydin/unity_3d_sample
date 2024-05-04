using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : AttackControllerBase
{
    public GameObject targetToAttack;
    public List<GameObject> targetsInRange;

    public int unitDamage;

    private void OnTriggerEnter(Collider other)
    {
        SetTarget("EnemyCollider", other, ref targetToAttack, ref targetsInRange);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveTarget("EnemyCollider", other, ref targetToAttack, ref targetsInRange);
    }
}
