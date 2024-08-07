using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [SerializeField] private GameObject buildEffect;
    [SerializeField] private float buildEffectDuration;

    private TurretScriptableObject turretToBuild;
    private Node selectedNode;
    [SerializeField] private NodeUI nodeUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    public bool CanBuild { get => turretToBuild != null; }
    public bool HasEnoughMoney { get => PlayerStats.Money >= turretToBuild.cost; }

    public void SelectNode(Node node)
    {
        if (node == selectedNode)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretScriptableObject turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public void DeselectTurret()
    {
        turretToBuild = null;

        DeselectNode();
    }

    public TurretScriptableObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
