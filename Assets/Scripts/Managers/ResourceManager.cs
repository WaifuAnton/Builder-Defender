using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDisctionary;

    private void Awake()
    {
        resourceAmountDisctionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
            resourceAmountDisctionary[resourceType] = 0;
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDisctionary[resourceType] += amount;
    }
}
