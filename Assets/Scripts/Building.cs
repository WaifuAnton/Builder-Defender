using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    HealthSystem healthSystem;
    BuildingTypeSO buildingType;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem.SetMaxHealthAmount(buildingType.maxHealthAmount, true);
    }

    private void Start()
    {
        healthSystem.OnDied += OnDied;
    }

    void OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
