using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    GameObject spriteGameObject;

    private void Awake()
    {
        spriteGameObject = transform.GetChild(0).gameObject;
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
            Hide();
        else
            Show(e.activeBuildingType.sprite);
    }
}
