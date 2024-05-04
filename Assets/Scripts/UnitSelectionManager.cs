using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask ground;
    public LayerMask clickable;
    public LayerMask attackable;
    public LayerMask interactible;

    public GameObject groundMarker;
    public GameObject attackMarker;

    public bool attackCursorVisible = false;

    private Camera cam;

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

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        CheckMouseButtonLeft();

        CheckMouseButtonRight();
    }

    private void CheckMouseButtonLeft()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetAttackMarker();
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //clickable bir objeye týklandýðýnda
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else //clickable bir objeye týklanMAdýðýnda
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }
            }
        }
    }

    private void CheckMouseButtonRight()
    {
        if (Input.GetMouseButtonDown(1) && unitsSelected.Any())
        {
            SetAttackMarker();
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //attackable bir objeye týklandýðýnda
            if (AtLeastOneOffensiveUnit(unitsSelected) && Physics.Raycast(ray, out hit, Mathf.Infinity, attackable))
            {
                //var target = hit.transform.gameObject;
                //SetAttackMarker(target, true);
                //SetAttackTargetToUnits(target);
            }
            else
            {
                //clickable bir objeye týklandýðýnda
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    groundMarker.transform.position = hit.point;

                    groundMarker.SetActive(false);
                    groundMarker.SetActive(true);

                    SetAttackTargetToUnits(null);
                }
            }
        }
        else
        {
        }
    }

    private void SetAttackTargetToUnits(GameObject target)
    {
        foreach (GameObject unit in unitsSelected)
        {
            if (unit.GetComponent<UnitAttackController>())
            {
                unit.GetComponent<UnitAttackController>().targetToAttack = target;
                unit.GetComponent<UnitMovement>().isCommandedToMove = false;
            }
        }
    }

    public void SetAttackMarker(GameObject target = null, bool active = false)
    {
        attackMarker.SetActive(active);
        AttackMarkerFollow attackMarkerFollow = attackMarker.GetComponent<AttackMarkerFollow>();

        if (target != null)
        {
            attackMarkerFollow.target = target.transform;
            attackMarker.transform.position = target.transform.position;
        }
        else
        {
            attackMarkerFollow.target = null;
        }
    }

    //TODO: tüm objeyi dönmesine gerek yok. selected unit iþaretlenirken bakýlabilir. ya da objeye attacker unit tag'i falan eklenebilir.
    private bool AtLeastOneOffensiveUnit(List<GameObject> unitsSelected)
    {
        foreach (GameObject unit in unitsSelected)
        {
            if (unit.GetComponent<UnitAttackController>())
            {
                return true;
            }
        }

        return false;
    }

    //TODO: yine burada tüm objeyi dönmesine gerek yok. baþka ne yapýlabilir?
    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            SelectUnit(unit, false);
        }

        groundMarker.SetActive(false);

        unitsSelected.Clear();
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitsSelected.Add(unit);
        SelectUnit(unit, true);
    }

    private void MultiSelect(GameObject unit)
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
        else
        {
            SelectUnit(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    internal void DragSelect(GameObject unit)
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            SelectUnit(unit, true);
        }
    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        TriggerSelectionIndicator(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
    }

    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.Find("Indicator").gameObject.SetActive(isVisible);
    }
}
