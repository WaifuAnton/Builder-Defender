using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
            resourceAmountDictionary[resourceType] = 0;
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
            if (resourceAmountDictionary[resourceAmount.resourceType] < resourceAmount.amount)
                return false;
        return true;
    }

    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
    }
}
