using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] 
    CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float zoomAmount = 2;
    [SerializeField] float zoomSpeed = 5;
    [SerializeField] float minOrthographicSize = 10;
    [SerializeField] float maxOrthographicSize = 30;

    float orthographicSize;
    float targetOrthographicSize;

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 moveDir = new Vector2(x, y).normalized;
        transform.position += moveSpeed * Time.deltaTime * (Vector3)moveDir;
    }

    void HandleZoom()
    {
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
