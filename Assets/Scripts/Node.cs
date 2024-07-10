using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    private Color startingColor;

    [SerializeField] private float yPositionOffset;

    private GameObject turret;

    private MeshRenderer meshRend;

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();

        startingColor = meshRend.material.color;
    }

    private void OnMouseDown()
    {
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
        meshRend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        meshRend.material.color = startingColor;
    }
}
