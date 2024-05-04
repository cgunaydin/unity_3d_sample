using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private float unitHealth;
    public float unitMaxHealth;

    public HealthTracker healthTracker;

    void Start()
    {
        unitHealth = unitMaxHealth;
        UpdateHealthUI();
    }

    internal void TakeDamage(int damageToInflict)
    {
        unitHealth -= damageToInflict;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthTracker.UpdateSliderValue(unitHealth, unitMaxHealth);

        if (unitHealth <= 0)
        {
            //Dying logic

            //Destruction or Dying Animation

            //Dying Sound Effects

            Destroy(gameObject);
        }
    }

    //TODO: targetsInRange listelerinden sil
    private void OnDestroy()
    {
        
    }
}
