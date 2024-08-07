using UnityEngine;

[CreateAssetMenu(fileName = "New turret", menuName = "Turret")]
public class TurretScriptableObject : ScriptableObject
{
    [Header("Models")]
    public GameObject prefab;
    public GameObject upgradedPrefab;

    [Header("Prices")]
    public int cost;
    public int upgradeCost;
    public float sellPercentage;

    [Header("Effects")]
    public GameObject buildEffect;
    public float buildEffectDuration;
    public GameObject upgradeEffect;
    public float upgradeEffectDuration;
    public GameObject sellEffect;
    public float sellEffectDuration;
}
