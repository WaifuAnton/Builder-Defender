using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] Transform pfWoodHarvester;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(pfWoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        return mouseWorldPosition;
    }
}
