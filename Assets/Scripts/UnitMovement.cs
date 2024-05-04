using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class UnitMovement : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask attackable;
    public LayerMask interactible;

    public bool isCommandedToMove;

    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactible))
            {
                hit.transform.Find("Indicator").gameObject.SetActive(true);
                ObjectSelectionManager.Instance.interactibleObjectsSelected.Add(hit.transform.gameObject);
                isCommandedToMove = true;
                agent.SetDestination(hit.transform.gameObject.transform.position);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                GetComponent<UnitAttackController>().targetToAttack = hit.transform.gameObject;

                isCommandedToMove = true;
                //agent.SetDestination(hit.transform.gameObject.transform.position);
                UnitSelectionManager.Instance.SetAttackMarker(hit.transform.gameObject, true);
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                Debug.Log("bokubok");
                isCommandedToMove = true;
                agent.SetDestination(hit.point);
            }
        }

        //agent reached destination
        if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            isCommandedToMove = false;
        }
    }
}
