using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    private Color startingColor;

    [SerializeField] private float yPositionOffset;
    private Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    private MeshRenderer meshRend;
    private BuildManager buildManager;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        startingColor = meshRend.material.color;

        positionOffset = new(0f, yPositionOffset, 0f);

        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (!buildManager.CanBuild || EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null)
        {
            Debug.Log("There is a turret already");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild || EventSystem.current.IsPointerOverGameObject()) return;

        meshRend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        meshRend.material.color = startingColor;
    }
}
