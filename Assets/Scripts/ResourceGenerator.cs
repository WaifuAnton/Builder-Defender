using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    ResourceGeneratorData resourceGeneratorData;
    float timer;
    float timerMax;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        Collider2D[] colliders2D = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;

        foreach (Collider2D collider2D in colliders2D)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null && resourceNode.resourceType == resourceGeneratorData.resourceType)
                nearbyResourceAmount++;
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);

        if (nearbyResourceAmount == 0)
            enabled = false;
        else
            timerMax = (resourceGeneratorData.timerMax / 2)
                + resourceGeneratorData.timerMax
                * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }
}
