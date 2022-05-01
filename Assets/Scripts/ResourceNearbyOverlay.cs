using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    const int TEXT_INDEX = 1, ICON_INDEX = 2;

    ResourceGeneratorData resourceGeneratorData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt(100f * nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        transform.GetChild(TEXT_INDEX).GetComponent<TextMeshPro>().SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        this.resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);
        transform.GetChild(ICON_INDEX).GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
