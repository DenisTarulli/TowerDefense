using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed;
    [SerializeField] private float panBorderThickness;
    [SerializeField] private float verticalCompensation;
    [SerializeField, Range(200f, 500f)] private float scrollSpeed;
    [SerializeField] private float minOrthoSize;
    [SerializeField] private float maxOrthoSize;
    [SerializeField] private Transform moveOrientation;

    private Vector3 centerPosition;
    private bool movementToggled;
    private float orthographicSize;
    private float startingOrthoSize;

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        startingOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        orthographicSize = startingOrthoSize;
        centerPosition = transform.position;
        movementToggled = true;
    }

    private void Update()
    {
        if (GameManager.GameIsOver)
        {
            RecenterCamera();
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            RecenterCamera();
        }

        ToggleMovement();
        CameraMovement();
        CameraZoom();
    }

    private void CameraMovement()
    {
        if (!movementToggled) return;

        if (Input.GetKey(KeyCode.W) )//|| Input.mousePosition.y >= Screen.height - panBorderThickness)
            transform.Translate(panSpeed * Time.deltaTime * verticalCompensation * moveOrientation.forward, Space.World);

        if (Input.GetKey(KeyCode.S) )//|| Input.mousePosition.y <= panBorderThickness)
            transform.Translate(panSpeed * Time.deltaTime * verticalCompensation * -moveOrientation.forward, Space.World);

        if (Input.GetKey(KeyCode.D) )//|| Input.mousePosition.x >= Screen.width - panBorderThickness)
            transform.Translate(panSpeed * Time.deltaTime * moveOrientation.right, Space.World);

        if (Input.GetKey(KeyCode.A) )//|| Input.mousePosition.x <= panBorderThickness)
            transform.Translate(panSpeed * Time.deltaTime * -moveOrientation.right, Space.World);
    }

    private void CameraZoom()
    {
        if (!movementToggled) return;

        float scroll = Input.mouseScrollDelta.y;

        orthographicSize -= scroll * scrollSpeed * Time.deltaTime;
        orthographicSize = Mathf.Clamp(orthographicSize, minOrthoSize, maxOrthoSize);

        virtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    private void RecenterCamera()
    {
        transform.position = centerPosition;
        orthographicSize = startingOrthoSize;
        virtualCamera.m_Lens.OrthographicSize = startingOrthoSize;
    }

    private void ToggleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (movementToggled)
                movementToggled = false;
            else
                movementToggled = true;
        }
    }
}
