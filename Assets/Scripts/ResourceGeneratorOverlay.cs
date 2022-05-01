using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    const int BAR_INDEX = 1, TEXT_INDEX = 2, ICON_INDEX = 3;

    ResourceGenerator resourceGenerator;

    private void Start()
    {
        resourceGenerator = transform.GetComponentInParent<ResourceGenerator>();
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetGeneratorData();

        transform.GetChild(ICON_INDEX).GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.GetChild(BAR_INDEX).localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
        transform.GetChild(TEXT_INDEX).GetComponent<TextMeshPro>    ().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        transform.GetChild(BAR_INDEX).localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
