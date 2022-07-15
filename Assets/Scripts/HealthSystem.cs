using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    [SerializeField] int maxHealthAmount;

    int currentHealthAmount;

    private void Awake()
    {
        currentHealthAmount = maxHealthAmount;
    }

    public void Damage(int damageAmount)
    {
        currentHealthAmount -= damageAmount;
        currentHealthAmount = Mathf.Clamp(currentHealthAmount, 0, maxHealthAmount);

        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
            OnDied?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return currentHealthAmount == 0;
    }

    public bool IsFullHealth()
    {
        return currentHealthAmount == maxHealthAmount;
    }

    public int GetCurrentHealthAmount()
    {
        return currentHealthAmount;
    }

    public float GetCurrentHealthAmountNormalized()
    {
        return (float)currentHealthAmount / maxHealthAmount;
    }

    public void SetMaxHealthAmount(int maxHealthAmount, bool updateCurrentHealthAmount)
    {
        this.maxHealthAmount = maxHealthAmount;
        if (updateCurrentHealthAmount)
            currentHealthAmount = maxHealthAmount;
    }
}
