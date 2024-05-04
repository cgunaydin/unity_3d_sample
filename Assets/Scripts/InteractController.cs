using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public Transform targetToInteract;

    //destination bazl� interactible gibi bir �ey yap�labilir. hatta buradan kald�r�l�p unitMovement i�erisine ta��nabilinir.
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
