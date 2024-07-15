using UnityEngine;

[CreateAssetMenu(fileName = "New wave", menuName = "Wave")]
public class WaveScriptableObject : ScriptableObject
{
    public GameObject enemy;
    public int enemiesAmount;
    public float spawnRate;
}
