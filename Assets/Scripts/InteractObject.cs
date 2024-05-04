using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    void Start()
    {
        ObjectSelectionManager.Instance.allInteractibleObjectList.Add(gameObject);
    }
}
