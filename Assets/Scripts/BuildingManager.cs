using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    BuildingTypeListSO buildingTypeList;
    BuildingTypeSO buildingType;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        buildingType = buildingTypeList.list[0];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
}
