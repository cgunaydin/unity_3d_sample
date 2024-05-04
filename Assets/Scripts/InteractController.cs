using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public Transform targetToInteract;

    //destination bazlý interactible gibi bir þey yapýlabilir. hatta buradan kaldýrýlýp unitMovement içerisine taþýnabilinir.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactible") && targetToInteract == null)
        {
            targetToInteract = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactible") && targetToInteract != null)
        {
            targetToInteract = null;
        }
    }
}
