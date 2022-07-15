using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
    public int maxHealthAmount;

    public string GetConstructionResourceCostString()
    {
        StringBuilder str = new StringBuilder();
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
            str
                .Append("<color=#")
                .Append(resourceAmount.resourceType.colorHex)
                .Append('>')
                .Append(resourceAmount.resourceType.nameShort)
                .Append(resourceAmount.amount)
                .Append("</color> ");
        return str.ToString();
    }
}
