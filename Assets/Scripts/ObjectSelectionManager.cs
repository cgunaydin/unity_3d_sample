using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelectionManager : MonoBehaviour
{
    public static ObjectSelectionManager Instance { get; set; }

    public List<GameObject> allInteractibleObjectList = new List<GameObject>();
    public List<GameObject> interactibleObjectsSelected = new List<GameObject>();

    public LayerMask ground;
    public LayerMask clickable;
    public LayerMask attackable;
    public LayerMask interactible;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            DeselectAll();
        }

    }

    private void DeselectAll()
    {
        foreach (var unit in interactibleObjectsSelected)
        {
            TriggerSelectionIndicator(unit, false);
        }

        interactibleObjectsSelected.Clear();
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.Find("Indicator").gameObject.SetActive(isVisible);
    }
}
