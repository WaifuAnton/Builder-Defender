using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    const int BAR_INDEX = 2;

    HealthSystem healthSystem;
    Transform barTransform;

    private void Start()
    {
        barTransform = transform.GetChild(BAR_INDEX); barTransform = transform.GetChild(BAR_INDEX);
        healthSystem = GetComponentInParent<HealthSystem>();

        healthSystem.OnDamaged += OnDamaged;
        UpdateBar();
        UpdateHealthBarVisibility();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetCurrentHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisibility()
    {
        gameObject.SetActive(!healthSystem.IsFullHealth());
    }

    private void OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateHealthBarVisibility();
    }
}
