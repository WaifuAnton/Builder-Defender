using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/BuildingTypeList")]
public class BuildingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO> list;
}
