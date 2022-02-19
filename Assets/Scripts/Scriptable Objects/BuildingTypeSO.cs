using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
}
