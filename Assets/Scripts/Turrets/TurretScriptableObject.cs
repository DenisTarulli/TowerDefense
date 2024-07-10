using UnityEngine;

[CreateAssetMenu(fileName = "New turret", menuName = "Turret")]
public class TurretScriptableObject : ScriptableObject
{
    public GameObject prefab;
    public int cost;
}
