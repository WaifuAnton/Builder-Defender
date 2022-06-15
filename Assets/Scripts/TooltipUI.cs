using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField] RectTransform canvasRectTransform;
    TextMeshProUGUI textMeshPro;
    RectTransform backgroundRectTransform;
    RectTransform rectTransform;
    TooltipTimer tooltipTimer;

    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();
        if (tooltipTimer == null)
            return;
        tooltipTimer.timer -= Time.deltaTime;
        if (tooltipTimer.timer <= 0)
            Hide();
    }

    void SetText(string tooltipText, TooltipTimer tooltipTimer = null)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }

    public void Show(string toolTipText, TooltipTimer tooltipTimer = null)
    {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(toolTipText);
        HandleFollowMouse();
    }

    void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float timer;
    }
}
