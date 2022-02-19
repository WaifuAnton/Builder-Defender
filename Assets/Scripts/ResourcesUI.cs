using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResourcesUI : MonoBehaviour
{
    ResourceTypeListSO resourceTypeList;
    Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>(resourceTypeList.list.Count);
        Transform resourceTemplate = transform.Find("Resource Template");
        int index = 0;

        resourceTemplate.gameObject.SetActive(false);
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -160;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceType.sprite;
            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index++;
        }
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += OnResourceAmountChanged;
        UpdateAmount();
    }

    void OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateAmount();
    }

    void UpdateAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
