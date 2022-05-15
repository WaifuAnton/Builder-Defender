using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] float maxConstructionRadius = 25;

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    BuildingTypeSO activeBuildingType;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)
            && !EventSystem.current.IsPointerOverGameObject()
            && activeBuildingType != null
            && CanSpawnBuilding(activeBuildingType, Utils.GetMouseWorldPosition())
            && ResourceManager.Instance.CanAfford(activeBuildingType.constructionResourceCostArray))
        {
            ResourceManager.Instance.SpendResource(activeBuildingType.constructionResourceCostArray);
            Instantiate(activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector2 position)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliders2D = Physics2D.OverlapBoxAll(position + boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = colliders2D.Length == 0;

        if (!isAreaClear)
            return false;

        colliders2D = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in colliders2D)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder == null)
                continue;
            else if (buildingTypeHolder.buildingType == activeBuildingType)
                return false;
        }

        colliders2D = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in colliders2D)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
                return true;
        }
        return false;
    }
}
