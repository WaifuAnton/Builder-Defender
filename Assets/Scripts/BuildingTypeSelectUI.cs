using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] Sprite arrowSprite;
    Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;
    Transform arrowButton;

    private void Awake()
    {
        Transform buttonTemplate = transform.Find("Button Template");
        buttonTemplate.gameObject.SetActive(false);

        int index = 0;
        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);
        float offsetAmount = 130;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
        arrowButton.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        index++;

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        foreach (BuildingTypeSO buildingType in buildingTypeList.list)
        {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);
            offsetAmount = 130;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            buttonTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            buttonTransformDictionary[buildingType] = buttonTransform;
            index++;
        }
    }

    private void Update()
    {
        UpdateActiveBuildingTypeButton();
    }

    void UpdateActiveBuildingTypeButton()
    {
        arrowButton.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys)
        {
            Transform buttonTransform = buttonTransformDictionary[buildingType];
            buttonTransform.Find("Selected").gameObject.SetActive(false);
        }
        
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (activeBuildingType == null)
            arrowButton.Find("Selected").gameObject.SetActive(true);
        else
            buttonTransformDictionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
    }
}
