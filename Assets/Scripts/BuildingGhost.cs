using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    const int SPRITE_INDEX = 0, OVERLAY_INDEX = 1;

    ResourceNearbyOverlay resourceNearbyOverlay;
    GameObject spriteGameObject;

    private void Awake()
    {
        resourceNearbyOverlay = transform.GetChild(OVERLAY_INDEX).GetComponent<ResourceNearbyOverlay>();
        spriteGameObject = transform.GetChild(SPRITE_INDEX).gameObject;
        Hide();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += OnActiveBuildingTypeChanged;
    }

    private void Update()
    {
        transform.position = Utils.GetMouseWorldPosition();
    }

    void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    void Hide()
    {
        spriteGameObject.SetActive(false);
    }

    void OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        if (e.activeBuildingType == null)
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(e.activeBuildingType.sprite);
            resourceNearbyOverlay.Show(e.activeBuildingType.resourceGeneratorData);
        }
    }
}
