using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AttackControllerBase : MonoBehaviour
    {
        public Material idleStateMaterial;
        public Material followStateMaterial;
        public Material attackStateMaterial;
        public SphereCollider attackRangeCollider;

        public void SetIdleMaterial()
        {
            GetComponent<Renderer>().material = idleStateMaterial;
        }
        public void SetFollowMaterial()
        {
            GetComponent<Renderer>().material = followStateMaterial;
        }
        public void SetAttackMaterial()
        {
            GetComponent<Renderer>().material = attackStateMaterial;
        }

        private void OnDrawGizmos()
        {
            //Follow Distance / Area
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRangeCollider.radius * 0.2f);

            //Attack Distance / Area
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);

            //Stop Attack Distance / Area
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1.2f);
        }
        protected bool SetTarget(string tagToCompare, Collider other, ref GameObject targetToAttack, ref List<GameObject> targetsInRange)
        {
            if (other.CompareTag(tagToCompare))
            {
                if (!targetsInRange.Contains(other.transform.parent.gameObject))
                {
                    targetsInRange.Add(other.transform.parent.gameObject);
                }

                if (targetToAttack == null)
                {
                    targetToAttack = other.transform.parent.gameObject;
                    return true;
                }
            }

            return false;
        }

        protected bool RemoveTarget(string tagToCompare, Collider other, ref GameObject targetToAttack, ref List<GameObject> targetsInRange)
        {
            if (other.transform.parent != null)
            {
                if (targetsInRange.Contains(other.transform.parent.gameObject))
                {
                    targetsInRange.Remove(other.transform.parent.gameObject);
                }

                if (other.CompareTag(tagToCompare) && targetToAttack != null && targetToAttack == other.transform.parent.gameObject)
                {
                    targetToAttack = null;
                    return true;
                }
            }
            return false;
        }
    }
}
