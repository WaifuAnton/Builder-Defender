using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    const float precisionMultiplier = 5;

    [SerializeField] float positionOffsetY;
    [SerializeField] bool runOnce;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * precisionMultiplier);
        if (runOnce)
            Destroy(this);
    }
}
