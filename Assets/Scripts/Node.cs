using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    private Color startingColor;

    [SerializeField] private float yPositionOffset;

    private GameObject turret;
    private MeshRenderer meshRend;
    private BuildManager buildManager;


    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        startingColor = meshRend.material.color;

        buildManager = BuildManager.Instance;
    }

    private void OnMouseDown()
    {
        if (buildManager.GetTurretToBuild() == null || EventSystem.current.IsPointerOverGameObject()) return;

        if (turret != null)
        {
            Debug.Log("There is a turret already");
            return;
        }

        GameObject turretToBuild = BuildManager.Instance.GetTurretToBuild();

        turret = Instantiate(turretToBuild, transform.position + new Vector3(0f, yPositionOffset, 0f), transform.rotation);
    }

    private void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null || EventSystem.current.IsPointerOverGameObject()) return;

        meshRend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        meshRend.material.color = startingColor;
    }
}
